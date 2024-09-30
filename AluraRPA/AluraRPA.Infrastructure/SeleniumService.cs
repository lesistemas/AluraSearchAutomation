using AluraRPA.Dominio;
using AluraRPA.Infraestrutura;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace AluraRPA.Infraestrutura
{
    public class SeleniumService : ISeleniumService
    {
        public List<Curso> BuscarResultados(string termoDeBusca)
        {
            var cursosEncontrados = new List<Curso>();

            using (IWebDriver driver = InicializarChromeDriver())
            {
                try
                {
                    NavegarParaSite(driver);
                    BuscarTermo(driver, termoDeBusca);
                    AplicarFiltroDeCursos(driver);
                    cursosEncontrados = ExtrairResultadosComPaginacao(driver);
                }
                catch (NoSuchElementException ex)
                {
                    Console.WriteLine($"[ERRO] Elemento não encontrado: {ex.Message}");
                }
                catch (ElementNotInteractableException ex)
                {
                    Console.WriteLine($"[ERRO] Elemento não interativo: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERRO] Erro durante a automação: {ex.Message}");
                }
            }

            return cursosEncontrados;
        }

        private IWebDriver InicializarChromeDriver()
        {
            var chromeOptions = new ChromeOptions();
            return new ChromeDriver(chromeOptions);
        }

        private void NavegarParaSite(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("https://www.alura.com.br/");
            Console.WriteLine("[INFO] Acessando o site da Alura...");
        }

        private void BuscarTermo(IWebDriver driver, string termoDeBusca)
        {
            var searchField = driver.FindElement(By.Id("header-barraBusca-form-campoBusca"));
            searchField.SendKeys(termoDeBusca);
            searchField.Submit();
            Console.WriteLine($"[INFO] Buscando por: {termoDeBusca}");
        }

        private void AplicarFiltroDeCursos(IWebDriver driver)
        {
            var filter = driver.FindElement(By.ClassName("show-filter-options"));
            filter.Click();

            var cursoFilter = driver.FindElement(By.CssSelector("input[value='COURSE']"));
            var cursoLabel = cursoFilter.FindElement(By.XPath("following-sibling::label"));
            cursoLabel.Click();

            driver.FindElement(By.Id("busca--filtrar-resultados")).Submit();
        }

        private List<Curso> ExtrairResultadosComPaginacao(IWebDriver driver)
        {
            List<Curso> cursosEncontrados = new List<Curso>();
            bool hasNextPage = true;

            while (hasNextPage)
            {
                var results = driver.FindElements(By.ClassName("busca-resultado"));
                foreach (var result in results)
                {
                    try
                    {
                        var curso = new Curso()
                        {
                            Titulo = result.FindElement(By.ClassName("busca-resultado-nome")).Text,
                            Descricao = result.FindElement(By.ClassName("busca-resultado-descricao")).Text,
                            CargaHoraria = "",
                            Professor = ""
                        };
                                            
                        curso.Link = result.FindElement(By.TagName("a")).GetAttribute("href");

                        cursosEncontrados.Add(curso);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[ERRO] Falha ao processar o curso: {ex.Message}");
                    }
                }

                hasNextPage = IrParaProximaPagina(driver);
            }

            for (int i = 0; i < cursosEncontrados.Count; i++)
            {

                ProcessarDetalhesDoCurso(driver, cursosEncontrados[i]);
            }

            return cursosEncontrados;
        }

        private bool IrParaProximaPagina(IWebDriver driver)
        {
            try
            {
                var botaoProximaPagina = driver.FindElements(By.ClassName("busca-paginacao-prevNext"))[1];
                botaoProximaPagina.Click();
                Thread.Sleep(2000);
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("[INFO] Não há mais páginas.");
                return false;
            }
        }

        private bool ProcessarDetalhesDoCurso(IWebDriver driver, Curso curso)
        {
            try
            {
                driver.Navigate().GoToUrl(curso.Link);
                var elementosNomeProfessor = driver.FindElements(By.ClassName("instructor-course--flex"));
                curso.Professor  = string.Join(", ", elementosNomeProfessor.Where(e => !string.IsNullOrEmpty(e.Text)).Select(e => e.Text));

                curso.CargaHoraria = driver.FindElement(By.ClassName("courseInfo-card-wrapper-infos")).Text;

                if (string.IsNullOrEmpty(curso.CargaHoraria) || string.IsNullOrEmpty(curso.Professor))
                {
                    Console.WriteLine("[INFO] Curso com dados incompletos. Pulando.");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERRO] Falha ao processar detalhes do curso: {ex.Message}");
                return false;
            }
        }
    }
}