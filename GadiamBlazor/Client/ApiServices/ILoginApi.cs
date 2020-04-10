using GadiamBlazor.Shared.Authentication;
using System.Threading.Tasks;

namespace GadiamBlazor.Client.ApiServices
{
    public interface ILoginApi
    {
        Task ExternalLoginRegister(ExternalLoginConfirmationModel externalLoginConfirmationModel);
        Task<ExternalLoginConfirmationModel> GetExternalLoginDetails();
    }
}