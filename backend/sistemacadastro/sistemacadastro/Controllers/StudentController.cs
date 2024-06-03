using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace sistemacadastro.Controllers
{
    
    [ApiController]
    public class StudentController : ControllerBase
    {

        private IConfiguration _configuration;

        public StudentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("get_tasks")]
        public JsonResult get_tasks()
        {
            string query = "select * from Student";
            DataTable table = new DataTable();
            string SqlDatasource = _configuration.GetConnectionString("cadastro_aluno");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(SqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                }

                return new JsonResult(table);

            }
        }

        [HttpPost("add_task")]
        public JsonResult add_task([FromForm] string matricula, [FromForm] string cpf, [FromForm] string nome, [FromForm] string curso, [FromForm] int idade )
        {
            string query = @"INSERT INTO Student(matricula, cpf,  nome, curso, idade) 
                             VALUES(@matricula, @cpf, @nome, @curso, @idade)";


            DataTable table = new DataTable();

            string SqlDatasource = _configuration.GetConnectionString("cadastro_aluno");
            SqlDataReader myReader;

            using(SqlConnection myCon = new SqlConnection(SqlDatasource))
            {
                myCon.Open();

                using(SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@matricula", matricula);
                    myCommand.Parameters.AddWithValue("@cpf", cpf);
                    myCommand.Parameters.AddWithValue("@nome", nome);
                    myCommand.Parameters.AddWithValue("@curso", curso);
                    myCommand.Parameters.AddWithValue("@idade", idade);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                   
                }
            }

            return new JsonResult("Adicionado com sucesso");
        }

        [HttpPost("delete_task")]
        public JsonResult delete_task([FromForm] string matricula)
        {
            string query = "DELETE FROM Student WHERE matricula = @matricula";
            DataTable table = new DataTable();
            string SqlDatasource = _configuration.GetConnectionString("cadastro_aluno");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(SqlDatasource))
            {
                myCon.Open();
                using(SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@matricula",matricula);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                }
            }

            return new JsonResult("Deletado com Sucesso!");




        }

    }
}
