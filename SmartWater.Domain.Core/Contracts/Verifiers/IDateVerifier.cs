using System;

namespace SmartWater.Domain.Core.Contracts.Verifiers
{
    public interface IDateVerifier
    {
        bool IsValid(DateTime date, DateTime? validFrom = null, DateTime? validTo = null);

        bool IsValidRange(DateTime? validFrom = null, DateTime? validTo = null);
    }
}
