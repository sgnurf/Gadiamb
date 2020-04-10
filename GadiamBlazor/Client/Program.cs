using Blazored.LocalStorage;
using GadiamBlazor.Client.ApiServices;
using GadiamBlazor.Client.Authentication;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace GadiamBlazor.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            ConfigureServices(builder.Services);
            builder.RootComponents.Add<App>("app");

            await builder.Build().RunAsync();
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddBlazoredLocalStorage();
            services.AddAuthorizationCore();
            services.AddGadiambAuthentication();
        }
    }

    public static class ConfigurationExtensiosn
    {
        public static IServiceCollection AddGadiambAuthentication(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddTransient<IAccountsApi, AccountsApi>()
                .AddTransient<IAuthenticationStateBuilder, AuthenticationStateBuilder>()
                .AddSingleton<ApiAuthenticationStateProvider>()
                .AddSingleton<AuthenticationStateProvider>((s) => s.GetRequiredService<ApiAuthenticationStateProvider>());
        }
    }
}