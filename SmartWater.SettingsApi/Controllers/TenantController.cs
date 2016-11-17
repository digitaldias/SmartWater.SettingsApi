using SmartWater.Domain.Core.Entities;
using SmartWater.SettingsApi.Domain.Managers;
using System.Threading.Tasks;
using System.Web.Http;

namespace SmartWater.SettingsApi.Controllers
{
    public class TenantController : ApiController
    {
        private readonly ITenantManager _tenantManager;


        public TenantController()
        {
            _tenantManager = WebApiApplication.IocContainer.GetInstance<ITenantManager>();
        }


        public TenantController(ITenantManager tenantManager)
        {
            _tenantManager = tenantManager;
        }


        [Route("v1/tenants/{tenantId}")]
        public async Task<Tenant> Get(string tenantId)
        {
            return await _tenantManager.GetByIdAsync(tenantId);
        }
    }
}
