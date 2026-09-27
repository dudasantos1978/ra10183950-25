namespace Tenant.Application.Interfaces.Resolvers;

public sealed record AccessContext(
    bool IsAuthenticated,
    bool IsSuperAdmin,
    Guid? UserId,
    Guid? OrganizationId)
{
    public static AccessContext Anonymous { get; } = new(false, false, null, null);

    public bool IsMemberOfResolvedOrganization => OrganizationId.HasValue;
}
