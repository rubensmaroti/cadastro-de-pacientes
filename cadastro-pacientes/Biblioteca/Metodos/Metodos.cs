using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca;
using Biblioteca.VOs;
using System.Data;
using System.Data.SqlClient;

namespace Biblioteca.Metodos
{
    public static class Metodos
    {

        public static PacienteVO MontaVOPaciente(DataRow registro)
        {
            PacienteVO paciente = new PacienteVO();
            paciente.PacienteCPF = registro["CPF"].ToString();
            paciente.Nome = registro["Nome"].ToString();
            paciente.Sexo = registro["Sexo"].ToString()[0];
            paciente.Email = registro["Email"].ToString();
            paciente.DataNasc = Convert.ToDateTime(registro["DtNascimento"]);
            paciente.Telefone = registro["Telefone"].ToString();
            return paciente;
        }
        public static DataTable ExecutaSelect(string sql, SqlParameter[] parametros)
        {
            using (SqlConnection conexao = ConexaoBD.GetConexao())
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conexao))
                {
                    if (parametros != null)
                        adapter.SelectCommand.Parameters.AddRange(parametros);
                    DataTable tabelaTemp = new DataTable();
                    adapter.Fill(tabelaTemp);
                    conexao.Close();
                    return tabelaTemp;
                }
            }
        }
    }
}
