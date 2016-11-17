using System;
using SmartWater.Domain.Core.Contracts.Verifiers;

namespace SmartWater.Business.Verifiers
{
    public class DateVerifier : IDateVerifier
    {
        public bool IsValid(DateTime date, DateTime? validFrom = default(DateTime?), DateTime? validTo = default(DateTime?))
        {
            if (!validFrom.HasValue)
                return false;

            if (!validTo.HasValue)
                return false;

            return date >= validFrom.Value && date <= validTo.Value;
        }


        public bool IsValidRange(DateTime? validFrom = default(DateTime?), DateTime? validTo = default(DateTime?))
        {
            if (!validFrom.HasValue)
                return false;

            if (!validTo.HasValue)
                return false;

            return validFrom.Value <= validTo;
        }
    }
}
