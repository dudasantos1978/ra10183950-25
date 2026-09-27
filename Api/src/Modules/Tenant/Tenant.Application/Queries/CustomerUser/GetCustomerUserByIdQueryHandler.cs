using BuildingBlocks.Application.Enums;
using BuildingBlocks.Application.Exceptions;
using BuildingBlocks.Application.Interfaces.Query;
using BuildingBlocks.Application.Interfaces.Uow;
using Dapper;
using Tenant.Application.DTOs;

namespace Tenant.Application.Queries.CustomerUser;

public class GetCustomerUserByIdQueryHandler : IQueryHandler<GetCustomerUserByIdQuery, CustomerUserDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCustomerUserByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CustomerUserDto> Handle(GetCustomerUserByIdQuery request, CancellationToken cancellationToken)
    {
        var builder = new SqlBuilder();
        var parameters = new DynamicParameters();

        builder.Select($"cu.id AS {nameof(CustomerUserDto.Id)}");
        builder.Select($"cu.created_at AS {nameof(CustomerUserDto.CreatedAt)}");
        builder.Select($"cu.updated_at AS {nameof(CustomerUserDto.UpdatedAt)}");
        builder.Select($"cu.row_version AS {nameof(CustomerUserDto.RowVersion)}");
        builder.Select($"cu.organization_id AS {nameof(CustomerUserDto.OrganizationId)}");
        builder.Select($"cu.inactived_at AS {nameof(CustomerUserDto.InactivedAt)}");
        builder.Select($"cu.customer_id AS {nameof(CustomerUserDto.CustomerId)}");
        builder.Select($"cu.user_id AS {nameof(CustomerUserDto.UserId)}");
        builder.Select($"u.name AS {nameof(CustomerUserDto.UserName)}");
        builder.Select($"u.email AS {nameof(CustomerUserDto.UserEmail)}");

        builder.LeftJoin($"{DatabaseSchemaEnum.SdpDpNew.User.FullName} u ON u.id = cu.user_id");

        builder.Where("1 = 1");
        builder.Where("cu.id = @Id", new { request.Id });
        builder.Where("cu.organization_id = @OrganizationId", new { request.OrganizationId });
        builder.Where("cu.inactived_at IS NULL");

        builder.OrderBy("cu.created_at DESC");

        var dataSelector = builder.AddTemplate($@"
SELECT 
/**select**/
FROM {DatabaseSchemaEnum.SdpDpNew.CustomerUser.FullName} cu
/**leftjoin**/
/**where**/
/**orderby**/");

        parameters.AddDynamicParams(dataSelector.Parameters);

        var sql = dataSelector.RawSql;

        var result = await _unitOfWork.SdpDpNew.Connection.QueryFirstOrDefaultAsync<CustomerUserDto>(sql, parameters, _unitOfWork.SdpDpNew.Transaction, commandTimeout: 30);
        if (result is null)
            throw AppException.NotFound($"A relação entre cliente e usuário não existe!");

        return result;
    }
}

