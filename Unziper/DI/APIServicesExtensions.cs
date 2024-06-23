using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unziper.Core.Abstractions;
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
