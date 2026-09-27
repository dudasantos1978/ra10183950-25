using BuildingBlocks.Application.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Tenant.Application.Interfaces.Resolvers;

namespace Api.Authorization;

public sealed class OrganizationAccessRequirement : IAuthorizationRequirement;

public sealed class OrganizationAccessAuthorizationHandler : AuthorizationHandler<OrganizationAccessRequirement>
{
    private readonly IAccessContextResolver _accessContextResolver;

    public OrganizationAccessAuthorizationHandler(IAccessContextResolver accessContextResolver)
    {
        _accessContextResolver = accessContextResolver;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        OrganizationAccessRequirement requirement)
    {
        var access = await _accessContextResolver.ResolveAsync();
        if (!access.IsAuthenticated)
            throw AppException.Unauthorized();

        if (access.IsSuperAdmin || access.IsMemberOfResolvedOrganization)
        {
            context.Succeed(requirement);
            return;
        }

        throw AppException.Forbidden();
    }
}
