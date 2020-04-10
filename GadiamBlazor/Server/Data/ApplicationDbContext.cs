using ElCamino.AspNetCore.Identity.AzureTable;
using ElCamino.AspNetCore.Identity.AzureTable.Model;

namespace GadiamBlazor.Server.Data
{
    public class ApplicationDbContext : IdentityCloudContext
    {
        public ApplicationDbContext() : base()
        {
        }

        public ApplicationDbContext(IdentityConfiguration config) : base(config)
        {
        }
    }
}
