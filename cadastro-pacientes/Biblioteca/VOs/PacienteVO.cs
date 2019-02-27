﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Biblioteca.Exceptions;
using Biblioteca.Enumeradores;

namespace Biblioteca.VOs
{
    public class PacienteVO : Registro
    {
        
       public PacienteVO()
        {
            Tipo = Tipo.Paciente;
        }

        
        private char sexo;
        private DateTime dataNasc;
        private string email;
        private string telefone;

        public override string CPF { get => base.CPF; set => base.CPF = VerificaCPF(value); }


        public override string Nome
        {
            get => base.Nome;
            set => base.Nome = (string.IsNullOrWhiteSpace(value) || !value.Contains(' ')) ? throw ValidacaoException.NomeValidacao : value;
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
            

            cpf = cpf.Replace(".", "").Replace(",", "").Replace("-", "");
           
            if (cpf.Length != 11)
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

            digitos[0] = (11 - soma == 10 || soma == 0) ? 0 : 11 - soma;

            mult = 11;
            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += (Convert.ToInt32(cpf[i].ToString()) * mult);
                mult--;
            }
            soma = soma % 11;

            digitos[1] = (11 - soma == 10 || soma == 0) ? 0 : 11 - soma;


            string aux = cpf.Remove(0, 9);

            if (aux != (digitos[0].ToString() + digitos[1].ToString()))
                throw ValidacaoException.CpfValidacao;

            else
                return cpf;

        }

        private string VerificaTelefone(string telefone)
        {
            telefone = telefone.Replace("(", "").Replace(")", "").Replace(".", "").Replace(" ", "").Replace("-", "");
           
            if (telefone.Length == 11 || telefone.Length == 10)
            {
               
                    return telefone;
                
               
            }
            throw ValidacaoException.TelefoneValidacao;
        }

    }









}

