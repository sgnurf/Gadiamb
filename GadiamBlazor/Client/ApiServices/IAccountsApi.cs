using GadiamBlazor.Shared.Authentication;
using System.Threading.Tasks;

namespace GadiamBlazor.Client.ApiServices
{
    public interface IAccountsApi
    {
        Task<AccountModel?> GetAccountDetailsAsync();
    }
}