using System.Security.Claims;
using BuildingBlocks.Application.Enums;
using Microsoft.AspNetCore.Http;
using Tenant.Application.Interfaces;
using Tenant.Application.Interfaces.Resolvers;
using Tenant.Domain.Entities;

namespace Tenant.Infrastructure.Resolvers;

public class AccessContextResolver : IAccessContextResolver
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITenantResolver _tenantResolver;
    private readonly IUserRepository _userRepository;
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IOrganizationUserRepository _organizationUserRepository;
    private readonly ICustomerUserRepository _customerUserRepository;

    public AccessContextResolver(
        IHttpContextAccessor httpContextAccessor,
        ITenantResolver tenantResolver,
        IUserRepository userRepository,
        IOrganizationRepository organizationRepository,
        IOrganizationUserRepository organizationUserRepository,
        ICustomerUserRepository customerUserRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _tenantResolver = tenantResolver;
        _userRepository = userRepository;
        _organizationRepository = organizationRepository;
        _organizationUserRepository = organizationUserRepository;
        _customerUserRepository = customerUserRepository;
    }

    public async Task<AccessContext> ResolveAsync(User? user = null, CancellationToken cancellationToken = default)
    {
        user ??= await TryLoadUserFromHttpContextAsync();
        if (user is null)
            return AccessContext.Anonymous;

        var isSuperAdmin = user.RoleId == RoleEnum.SuperAdmin.Id;
        var organizationId = await TryResolveOrganizationIdAsync();

        if (isSuperAdmin)
            return new AccessContext(true, true, user.Id, organizationId);

        if (organizationId is null)
            return new AccessContext(true, false, user.Id, null);

        var isMember = await _organizationUserRepository.GetByUserIdAsync(organizationId.Value, user.Id) is not null
            || await _customerUserRepository.GetByUserIdAsync(organizationId.Value, user.Id) is not null;

        return isMember
            ? new AccessContext(true, false, user.Id, organizationId)
            : new AccessContext(true, false, user.Id, null);
    }

    private async Task<Guid?> TryResolveOrganizationIdAsync()
    {
        var slug = _tenantResolver.ResolveWithSource().Slug?.Trim();
        if (string.IsNullOrWhiteSpace(slug))
            return null;

        var organization = await _organizationRepository.GetBySlugAsync(slug);
        return organization?.Id;
    }

    private async Task<User?> TryLoadUserFromHttpContextAsync()
    {
        var userIdRaw = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypeEnum.UserId.Type)
            ?? _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdRaw, out var userId))
            return null;

        return await _userRepository.GetByIdAsync(userId);
    }
}
