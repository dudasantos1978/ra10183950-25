using BuildingBlocks.Application.Enums;
using BuildingBlocks.Application.Exceptions;
using BuildingBlocks.Application.Interfaces.Query;
using BuildingBlocks.Application.Interfaces.Uow;
using Dapper;
using Tenant.Application.DTOs;

namespace Tenant.Application.Queries.OrganizationUser;

public class GetOrganizationUserByIdQueryHandler : IQueryHandler<GetOrganizationUserByIdQuery, OrganizationUserDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetOrganizationUserByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OrganizationUserDto> Handle(GetOrganizationUserByIdQuery request, CancellationToken cancellationToken)
    {
        var builder = new SqlBuilder();
        var parameters = new DynamicParameters();

        builder.Select($"ou.id AS {nameof(OrganizationUserDto.Id)}");
        builder.Select($"ou.created_at AS {nameof(OrganizationUserDto.CreatedAt)}");
        builder.Select($"ou.updated_at AS {nameof(OrganizationUserDto.UpdatedAt)}");
        builder.Select($"ou.row_version AS {nameof(OrganizationUserDto.RowVersion)}");
        builder.Select($"ou.organization_id AS {nameof(OrganizationUserDto.OrganizationId)}");
        builder.Select($"ou.inactived_at AS {nameof(OrganizationUserDto.InactivedAt)}");
        builder.Select($"ou.user_id AS {nameof(OrganizationUserDto.UserId)}");
        builder.Select($"ou.team_id AS {nameof(OrganizationUserDto.TeamId)}");
        builder.Select($"u.name AS {nameof(OrganizationUserDto.UserName)}");
        builder.Select($"u.email AS {nameof(OrganizationUserDto.UserEmail)}");

        builder.LeftJoin($"{DatabaseSchemaEnum.SdpDpNew.User.FullName} u ON u.id = ou.user_id");

        builder.Where("1 = 1");
        builder.Where("ou.id = @Id", new { request.Id });
        builder.Where("ou.organization_id = @OrganizationId", new { request.OrganizationId });
        builder.Where("ou.inactived_at IS NULL");

        builder.OrderBy("ou.created_at DESC");

        var dataSelector = builder.AddTemplate($@"
SELECT 
/**select**/
FROM {DatabaseSchemaEnum.SdpDpNew.OrganizationUser.FullName} ou
/**leftjoin**/
/**where**/
/**orderby**/");

        parameters.AddDynamicParams(dataSelector.Parameters);

        var sql = dataSelector.RawSql;

        var result = await _unitOfWork.SdpDpNew.Connection.QueryFirstOrDefaultAsync<OrganizationUserDto>(sql, parameters, _unitOfWork.SdpDpNew.Transaction, commandTimeout: 30);
        if (result is null)
            throw AppException.NotFound($"A relação entre organização e usuário não existe!");

        return result;
    }
}

