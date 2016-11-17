using SmartWater.Domain.Core.Entities;

namespace SmartWater.Domain.Core.Contracts.Verifiers
{
    public interface ITenantValidator
    {
        ValidationResult Validate(string tenantId);
    }
}
