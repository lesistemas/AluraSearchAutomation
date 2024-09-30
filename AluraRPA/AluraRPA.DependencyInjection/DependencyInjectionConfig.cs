using AluraRPA.Dominio;
using AluraRPA.Infraestrutura;
using AluraRPA.RPA;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace AluraRPA.InjecaoDeDependencia
{
    public static class DependencyInjectionConfig
    {
        public static IServiceProvider ConfigureServices()
        {
            // Carregar a configuração do appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var services = new ServiceCollection();

            // Registrar IConfiguration para uso com DI
            services.AddSingleton<IConfiguration>(configuration);

            // Registrar o CursoRepository, injetando a connection string a partir da configuração
            services.AddTransient<ICursoRepository>(provider =>
            {
                var config = provider.GetService<IConfiguration>();
                var connectionString = config.GetConnectionString("MySqlConnection");
                return new CursoRepository(connectionString);
            });

            // Registrar outros serviços
            services.AddTransient<IRPAService, RPAService>();
            services.AddTransient<ISeleniumService, SeleniumService>();

            return services.BuildServiceProvider();
        }
    }
}
