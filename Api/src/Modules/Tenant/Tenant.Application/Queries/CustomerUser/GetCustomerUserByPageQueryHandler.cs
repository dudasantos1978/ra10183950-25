using BuildingBlocks.Application.DTOs;
using BuildingBlocks.Application.Enums;
using BuildingBlocks.Application.Exceptions;
using BuildingBlocks.Application.Interfaces.Query;
using BuildingBlocks.Application.Interfaces.Uow;
using Dapper;
using Tenant.Application.DTOs;

namespace Tenant.Application.Queries.CustomerUser;

public class GetCustomerUserByPageQueryHandler : IQueryHandler<GetCustomerUserByPageQuery, PageDto<CustomerUserDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCustomerUserByPageQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PageDto<CustomerUserDto>> Handle(GetCustomerUserByPageQuery request, CancellationToken cancellationToken)
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
        builder.Where("cu.inactived_at IS NULL");
        builder.Where("cu.organization_id = @OrganizationId", new { request.OrganizationId });
        builder.Where("cu.customer_id = @CustomerId", new { request.CustomerId });

        builder.OrderBy("cu.created_at DESC");

        var dataSelector = builder.AddTemplate($@"
SELECT COUNT(*)
FROM {DatabaseSchemaEnum.SdpDpNew.CustomerUser.FullName} cu
/**leftjoin**/
/**where**/;

SELECT 
/**select**/
FROM {DatabaseSchemaEnum.SdpDpNew.CustomerUser.FullName} cu
/**leftjoin**/
/**where**/
/**orderby**/
{_unitOfWork.SdpDpNew.Offset()}");

        parameters.AddDynamicParams(dataSelector.Parameters);
        parameters.Add("@PageIndex", request.PageIndex - 1);
        parameters.Add("@PageSize", request.PageSize);

        var sql = dataSelector.RawSql;

        using var multi = await _unitOfWork.SdpDpNew.Connection.QueryMultipleAsync(sql, parameters, _unitOfWork.SdpDpNew.Transaction, commandTimeout: 30);
        var totalItemCount = await multi.ReadSingleAsync<int>();
        var data = await multi.ReadAsync<CustomerUserDto>();

        if (data is null || !data.Any())
            throw AppException.NotFound($"Nenhuma relação entre cliente e usuário encontrada!");

        return new PageDto<CustomerUserDto>
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            TotalItemCount = totalItemCount,
            Items = data
        };
    }
}

