using SmartWater.Domain.Core.Entities;
using System.Threading.Tasks;

namespace SmartWater.Domain.Core.Contracts.Readers
{
    public interface ITenantReader
    {
        Task<Tenant> GetByIdAsync(string tenantId);
    }
}