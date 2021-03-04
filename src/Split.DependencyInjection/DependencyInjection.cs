using Autofac;
using Microsoft.Extensions.Configuration;
using Split.Domain.Interfaces;
using Split.Repository;
using Splitio.Services.Client.Classes;
using Splitio.Services.Client.Interfaces;
using System.IO;
using System.Reflection;

namespace Split.DependencyInjection
{
    public static class DependencyInjection
    {
        private static IConfigurationRoot _configuration;

        public static IConfigurationRoot GetConfiguration() => _configuration;

        public static void ConfigureContainer(this ContainerBuilder builder)
        {
            _configuration = LoadConfiguration();

            builder.RegisterType<DummyRepository.DummyRepository>().As<ICustomerScopeRepository>();
            builder.RegisterType<SplitRepository>().As<ICustomerScopeRepository>();
            
            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(SplitService)))
                .AsImplementedInterfaces();

            var splitioOptions = new SplitOptions();
            _configuration.GetSection("Splitio").Bind(splitioOptions);
            builder.Register(x => new SplitFactory(splitioOptions.ApiKey)).As<ISplitFactory>().SingleInstance();
        }

        private static IConfigurationRoot LoadConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), optional: true, reloadOnChange: true);

            return configurationBuilder.Build();
        }
    }
}