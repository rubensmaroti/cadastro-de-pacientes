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

            if (paciente.DataNasc != null && paciente.Email != null && paciente.Nome != null && paciente.PacienteCPF != null && paciente.Sexo.ToString() != null && paciente.Telefone != null)
                return paciente;
            else
               throw new Exception("Erro ao carregar Paciente ");
        }
        public static ImagensVO MontaVOImagem(DataRow registro)
        {
           SqlParameter[] sqlParameter =  new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@CPF", registro["CPF"].ToString());
            PacienteVO paciente = MontaVOPaciente(ExecutaSelect("Select * from Pacientes where CPF = @CPF", sqlParameter).Rows[0]);

            ImagensVO imagens = new ImagensVO(registro["Caminho"].ToString(), paciente);
            if (imagens.Caminho != null && imagens.Paciente.PacienteCPF != null)
            {
                return imagens;
            }
            else throw new Exception("Erro ao carregar Imagem ");
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
