using AluraRPA.Dominio;
using System.Collections.Generic;

namespace AluraRPA.Infraestrutura
{
    public interface ICursoRepository
    {
        void AdicionarCurso(Curso curso);
        IEnumerable<Curso> ObterCursos();
    }
}
