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

        public static PacienteVO MontaVO(DataRow registro)
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

    }
}
