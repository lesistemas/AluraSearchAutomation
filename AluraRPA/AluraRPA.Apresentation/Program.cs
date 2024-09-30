using AluraRPA.Dominio;
using AluraRPA.InjecaoDeDependencia;
using AluraRPA.RPA;
using Microsoft.Extensions.DependencyInjection;

namespace AluraRPA
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = DependencyInjectionConfig.ConfigureServices();
            var rpaService = serviceProvider.GetService<IRPAService>();

            try
            {
                // Log antes de iniciar a automação
                Console.WriteLine("[INFO] Iniciando automação de busca...");

                // Executa a automação
                rpaService.ExecutarBusca("RPA");

                // Log após a conclusão da automação
                Console.WriteLine("[INFO] Automação concluída com sucesso.");
            }
            catch (Exception ex)
            {
                // Captura erros gerais
                Console.WriteLine($"[ERRO] Ocorreu um erro durante a execução: {ex.Message}");
            }
        }
    }
}
