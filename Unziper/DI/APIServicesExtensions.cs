using Microsoft.Extensions.DependencyInjection;
using Unziper.ViewModels;
using Unziper.Views;

namespace Unziper.DI
{
    public static class APIServicesExtensions
    {
        public static void SetViewModels(this IServiceCollection services)
        {
            services.AddSingleton<MenuViewModel>();
            services.AddSingleton<MenuWindowViewModel>();
        }

        public static void SetViews(this IServiceCollection services)
        {
            services.AddTransient<MenuView>();
            services.AddTransient<MenuWindow>();
        }
    }
}
