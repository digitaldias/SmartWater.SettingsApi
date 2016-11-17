using Microsoft.Azure.Documents.Client;
using SmartWater.Domain.Core.Contracts.Readers;
using SmartWater.Domain.Core.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWater.Data.DocDb
{
    public class TenantReader : DocumentDbReader, ITenantReader
    {
        public TenantReader(IConfigurationReader settings)
            : base(settings, "Tenants")
        {

        }


        public async Task<Tenant> GetByIdAsync(string tenantId)
        {
            var feedOptions = new FeedOptions { MaxBufferedItemCount = 1 };
            var results = await QueryFor<Tenant>(t => t.Id == tenantId, feedOptions);

            return results.FirstOrDefault();
        }
    }
}
