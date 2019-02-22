using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Biblioteca
{
    public static class ConexaoBD
    {
        public static SqlConnection GetConexao()
        {
            string strCon = "Data Source=LOCALHOST;Initial Catalog=CadastroDePacienteDB; integrated security=true";
            //string strCon = "Data Source=LOCALHOST;Initial Catalog=CadastroDePacienteDB; user id=sa; password=123456";
            SqlConnection conexao = new SqlConnection(strCon);
            conexao.Open();
            return conexao;
        }

    }
}
