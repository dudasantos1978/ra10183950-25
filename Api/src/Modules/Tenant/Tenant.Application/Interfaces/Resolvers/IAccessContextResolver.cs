using Tenant.Domain.Entities;

namespace Tenant.Application.Interfaces.Resolvers;

public interface IAccessContextResolver
{
    Task<AccessContext> ResolveAsync(User? user = null, CancellationToken cancellationToken = default);
}
