using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unziper.Core.Abstractions;
using Unziper.Core.Implementations;
using Unziper.Domain.Models;

namespace Unziper.Core.DI
{
    public static class CoreServicesExtension
    {
        public static void SetCoreServices(this IServiceCollection services)
        {
            services.AddTransient<IUnzipService, UnzipService>();
            services.AddTransient<IAsyncUnzipService, AsyncUnzipService>();
            services.AddTransient<IOptionService, OptionService>(services 
                => new OptionService(services.GetRequiredService<Options>(),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"Unziper"),
                "appsettings.json"));
        }
    }
}
