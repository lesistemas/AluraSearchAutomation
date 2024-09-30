using AluraRPA.Dominio;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace AluraRPA.Infraestrutura
{
    public class CursoRepository : ICursoRepository
    {
        private readonly string _connectionString;

        public CursoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Método para adicionar um curso ao banco de dados
        public void AdicionarCurso(Curso curso)
        {
            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                string sqlQuery = "INSERT INTO Cursos (Titulo, Descricao, Professor, CargaHoraria) VALUES (@Titulo, @Descricao, @Professor, @CargaHoraria)";
                dbConnection.Open();
                dbConnection.Execute(sqlQuery, curso);
            }
        }

        // Método para listar todos os cursos
        public IEnumerable<Curso> ObterCursos()
        {
            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                string sqlQuery = "SELECT * FROM Cursos";
                dbConnection.Open();
                return dbConnection.Query<Curso>(sqlQuery);
            }
        }
    }
}
