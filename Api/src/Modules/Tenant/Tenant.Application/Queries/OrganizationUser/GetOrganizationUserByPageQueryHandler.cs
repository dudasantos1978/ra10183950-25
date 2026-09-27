using BuildingBlocks.Application.DTOs;
using BuildingBlocks.Application.Enums;
using BuildingBlocks.Application.Exceptions;
using BuildingBlocks.Application.Interfaces.Query;
using BuildingBlocks.Application.Interfaces.Uow;
using Dapper;
using Tenant.Application.DTOs;

namespace Tenant.Application.Queries.OrganizationUser;

public class GetOrganizationUserByPageQueryHandler : IQueryHandler<GetOrganizationUserByPageQuery, PageDto<OrganizationUserDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetOrganizationUserByPageQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PageDto<OrganizationUserDto>> Handle(GetOrganizationUserByPageQuery request, CancellationToken cancellationToken)
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
        builder.Where("ou.organization_id = @OrganizationId", new { request.OrganizationId });
        builder.Where("ou.inactived_at IS NULL");

        builder.OrderBy("ou.created_at DESC");

        var dataSelector = builder.AddTemplate($@"
SELECT COUNT(*)
FROM {DatabaseSchemaEnum.SdpDpNew.OrganizationUser.FullName} ou
/**leftjoin**/
/**where**/;

SELECT 
/**select**/
FROM {DatabaseSchemaEnum.SdpDpNew.OrganizationUser.FullName} ou
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
        var data = await multi.ReadAsync<OrganizationUserDto>();

        if (data is null || !data.Any())
            throw AppException.NotFound($"Nenhuma relação entre organização e usuário encontrada!");

        return new PageDto<OrganizationUserDto>
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            TotalItemCount = totalItemCount,
            Items = data
        };
    }
}

