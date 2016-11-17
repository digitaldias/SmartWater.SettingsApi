using SmartWater.Domain.Core.Contracts.Managers;
using SmartWater.Domain.Core.Contracts.Readers;
using SmartWater.Domain.Core.Contracts.Verifiers;
using SmartWater.Domain.Core.Entities;
using SmartWater.SettingsApi.Domain.Managers;
using System.Threading.Tasks;

namespace SmartWater.SettingsApi.Business.Managers
{
    public class TenantManager : ITenantManager
    {
        private readonly IExceptionHandler _exceptionHandler;
        private readonly ITenantReader     _tenantReader;
        private readonly ITenantValidator  _tenantValidator;

        public TenantManager(ITenantValidator tenantValidator, ITenantReader tenantReader, IExceptionHandler exceptionHandler)
        {
            _tenantValidator  = tenantValidator;
            _tenantReader     = tenantReader;
            _exceptionHandler = exceptionHandler;
        }


        public async Task<Tenant> GetByIdAsync(string tenantId)
        {
            if (!_tenantValidator.Validate(tenantId).Passed)
                return null;

            return await _exceptionHandler.GetAsync(() => _tenantReader.GetByIdAsync(tenantId));
        }
    }
}
