using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Biblioteca.Exceptions;

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
            set => nome = (string.IsNullOrWhiteSpace(value) || !value.Contains(' ')) ? throw ValidacaoException.NomeValidacao : value;
        }

        public char Sexo
        {
            get => sexo;
            set => sexo = (value.ToString().ToUpper() == "F" || value.ToString().ToUpper() == "M") ? value : throw ValidacaoException.SexoValidacao;
        }

        public DateTime DataNasc
        {
            get => dataNasc;
            set => dataNasc = (DateTime.Now < value) ? throw ValidacaoException.DataNascValidacao : value;
        }

        public string Email
        {
            get => email;
            set => email = (string.IsNullOrEmpty(value) || !value.Contains('@') || !value.Contains('.') || value.ElementAt(value.Length - 1) == '.') ? throw ValidacaoException.EmailValidcao : value;
        }

        public string Telefone
        {
            get => telefone;
            set => telefone = VerificaTelefone(value);
        }


        private string VerificaCPF(string cpf)
        {
            int a;

            cpf = cpf.Replace(".", "").Replace(",", "").Replace("-", "");
            if (!int.TryParse(cpf, out a) || cpf.Length !=11)
            {
                throw ValidacaoException.CpfValidacao;
            }

            int soma = 0;
            int mult = 10;

            int[] digitos = new int[2];


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


            string aux = cpf.Remove(0, 9);

            if (aux != (digitos[0].ToString() + digitos[1].ToString()))
                throw ValidacaoException.CpfValidacao;

            else
                return cpf;

        }

        private string VerificaTelefone(string telefone)
        {
            telefone.Replace("(", "").Replace(")", "").Replace(".", "").Replace(" ", "");
            int a = 0;
            if (telefone.Length == 11 || telefone.Length == 10)
            {
                if (int.TryParse(telefone, out a))
                {
                    return telefone;
                }
                else
                {
                    throw ValidacaoException.TelefoneValidacao;
                }
            }
            throw ValidacaoException.TelefoneValidacao;
        }

    }









}

