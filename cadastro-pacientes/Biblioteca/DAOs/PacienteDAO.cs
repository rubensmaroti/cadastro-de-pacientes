using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Biblioteca.VOs;

namespace Biblioteca.DAOs
{
    public static class PacienteDAO
    {
        private static SqlParameter[] CriaParametrosDoPaciente(PacienteVO paciente)
        {
            SqlParameter[] parameters = new SqlParameter[6];

            parameters[0] = new SqlParameter("@CPF", paciente.PacienteCPF);
            parameters[1] = new SqlParameter("@Nome", paciente.Nome);
            parameters[2] = new SqlParameter("@Sexo", paciente.Sexo.ToString());
            parameters[3] = new SqlParameter("@DtNascimento", paciente.DataNasc.ToShortDateString());
            parameters[4] = new SqlParameter("@Email", paciente.Email);
            parameters[5] = new SqlParameter("@Telefone", paciente.Telefone);

            return parameters;
        }

        public static void Insert(PacienteVO paciente)
        {
            string sql = "insert into Paciente(CPF,Nome,Sexo,DtNascimento,Email,Telefone)" +
                           "values (@CPF,@Nome,@Sexo,@DtNascimento,@Email,@Telefone)";
            MetodosGenericosDeDAO.ExecutaSQL(sql, CriaParametrosDoPaciente(paciente));
        }
        public static void Update(PacienteVO paciente)
        {
            string sql = "update Paciente set Nome = @Nome,Sexo=@Sexo,DtNascimento=@DtNascimento,Email=@Email,Telefone=@Telefone" +
                           "where CPF = @CPF";
            MetodosGenericosDeDAO.ExecutaSQL(sql, CriaParametrosDoPaciente(paciente));
        }
        public static void Delete(string cpf)
        {
            SqlParameter[] parametros = { new SqlParameter("CPF", cpf) };

            string sql = "delete Paciente where CPF = @CPF";
            MetodosGenericosDeDAO.ExecutaSQL(sql, parametros);
        }
    }
}
