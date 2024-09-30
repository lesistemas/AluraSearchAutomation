using AluraRPA.Dominio;
using System.Collections.Generic;

namespace AluraRPA.Infraestrutura
{
    // A interface ISeleniumService define o contrato para o serviço de automação.
    public interface ISeleniumService
    {
        // Método responsável por buscar os resultados de cursos com base no termo de busca
        // Esse método será implementado no SeleniumService, onde a automação via Selenium ocorrerá.
        List<Curso> BuscarResultados(string termoDeBusca);
    }
}
