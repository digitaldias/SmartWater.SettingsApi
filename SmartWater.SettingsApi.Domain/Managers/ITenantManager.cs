using System.Threading.Tasks;
using SmartWater.Domain.Core.Entities;

namespace SmartWater.SettingsApi.Domain.Managers
{
    public interface ITenantManager
    {
        Task<Tenant> GetByIdAsync(string tenantId);
    }
}
