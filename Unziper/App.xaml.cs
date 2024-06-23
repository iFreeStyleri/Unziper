using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;
using System.Windows;
using Unziper.Core.Abstractions;
using Unziper.Core.DI;
using Unziper.DI;
using Unziper.Domain.Models;
using Unziper.Views;

namespace Unziper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider Services { get; private set; }
        public App()
        {
            var services = new ServiceCollection();
            Services = ServiceConfiguring(services);
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Init();
        }

        private IServiceProvider ServiceConfiguring(IServiceCollection services)
        {
            services.AddSingleton<Options>();
            services.SetCoreServices();
            services.SetViewModels();
            services.SetViews();
            return services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Services.GetRequiredService<MenuWindow>().Show();
        }
        private void Init()
        {
            var optionsService = Services.GetRequiredService<IOptionService>();
            optionsService.Create();
            optionsService.Load();
        }
    }
}
