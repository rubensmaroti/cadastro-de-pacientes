using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Biblioteca.VOs
{
    public class PacienteVO
    {
        private string pacienteCPF;
        private string nome;
        private char sexo;
        private DateTime dataNasc;
        private string email;
        private string telefone;

        public string PacienteCPF
        {
            get => pacienteCPF;
            set => pacienteCPF = VerificaCPF(value);
        }

        public string Nome
        {
            get => nome;
            set => nome = (string.IsNullOrWhiteSpace(value) || !value.Contains(' ')) ? throw new Exception("Nome Inválido") : value;
        }

        public char Sexo
        {
            get => sexo;
            set => sexo = (value.ToString().ToUpper() == "F" || value.ToString().ToUpper() == "M") ?  value: throw new Exception("Sexo Inválido");
        }

        public DateTime DataNasc
        {
            get => dataNasc;
            set => dataNasc = (DateTime.Now < value) ? throw new Exception("Data de Nascimento Inválida") : value;
        }

        public string Email
        {
            get => email;
            set => email = (string.IsNullOrEmpty(value) || !value.Contains('@')) ? throw new Exception("e-mail Inválido") : value;
        }

        public string Telefone
        {
            get => telefone;
            set => telefone = value;
        }


        private string VerificaCPF(string cpf)
        {
            if (cpf.Length != 14)
            {
                throw new Exception("CPF inválido");
            }
            string aux = cpf;
            int soma = 0;
            int mult = 10;

            int[] digitos = new int[2];

            cpf = cpf.Replace(".","").Replace(",","").Substring(0,9);

            for (int i = 0; i < 9; i++)
            {
                soma += (Convert.ToInt32(cpf[i].ToString()) * mult);
                mult--;
            }
            soma = soma % 11;

            digitos[0] = (11 - soma == 10) ? 0 : 11 - soma;

            mult = 11;
            for (int i = 0; i < 9; i++)
            {
                soma += (Convert.ToInt32(cpf[i]) * mult);
                mult--;
            }
            soma = soma % 11;

            digitos[1] = (11 - soma == 10) ? 0 : 11 - soma;


            cpf = aux.Replace("-", "").Replace(",", "").Replace(".", "").Remove(0, 9);
            
            if (cpf != (digitos[0].ToString()+digitos[1].ToString()))
                throw new Exception("CPF inválido");

            else
                return aux;

        }

        private string VerificaTelefone(string telefone)
        {
            if (telefone.StartsWith(string.Format("__ ")))
            {
                return telefone;
            }
            throw new Exception("Telefone inválido");
        }

    }









}

