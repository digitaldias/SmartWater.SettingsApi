using SmartWater.Domain.Core.Contracts.Verifiers;
using SmartWater.Domain.Core.Entities;

namespace SmartWater.Business.Verifiers
{
    public class TenantValidator : ITenantValidator
    {
        private const int MINIMUM_TENANTID_LENGTH = 2;
        private const int MAXIMUM_TENANTID_LENGTH = 25;

        public ValidationResult Validate(string tenantId)
        {
            if (string.IsNullOrEmpty(tenantId))
                return ValidationResult.Fail("TenantId is null or empty");

            if (tenantId.Length < MINIMUM_TENANTID_LENGTH)
                return ValidationResult.Fail("TenantId is too short");

            if (tenantId.Length >= MAXIMUM_TENANTID_LENGTH)
                return ValidationResult.Fail($"TenantId is too long. Maximum length is {MAXIMUM_TENANTID_LENGTH} characters");

            return ValidationResult.Pass;
        }
    }
}
