using AluraRPA.Dominio;
using AluraRPA.Infraestrutura;

namespace AluraRPA.RPA
{
    // Serviço de automação que utiliza o SeleniumService para realizar a busca e salva os resultados usando o CursoRepository
    public class RPAService : IRPAService
    {
        private readonly ICursoRepository _cursoRepository;
        private readonly ISeleniumService _seleniumService;

        // Construtor injetando o repositório e o serviço de automação
        public RPAService(ICursoRepository cursoRepository, ISeleniumService seleniumService)
        {
            _cursoRepository = cursoRepository;
            _seleniumService = seleniumService;
        }

        // Executa a busca e salva os resultados
        public void ExecutarBusca(string termoDeBusca)
        {
            Console.WriteLine($"[INFO] Iniciando busca por: {termoDeBusca}");

            // Busca os resultados de cursos
            List<Curso> resultados = _seleniumService.BuscarResultados(termoDeBusca);

            // Itera pelos cursos encontrados e salva no repositório
            foreach (var resultado in resultados)
            {
                _cursoRepository.AdicionarCurso(resultado);

                // Log de detalhes do curso
                Console.WriteLine($"[INFO] Curso salvo: {resultado.Titulo}, Professor: {resultado.Professor}");
            }

            Console.WriteLine("[INFO] Busca e salvamento de cursos concluídos.");
        }
    }
}
