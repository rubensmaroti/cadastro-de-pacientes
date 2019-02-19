using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Exceptions
{
    class ValidacaoException : Exception
    {
        public ValidacaoException(string msg) : base(msg) 
        {            
        }

        public ValidacaoException CpfValidacao = new ValidacaoException("CPF Inválido");
        public ValidacaoException NomeValidacao = new ValidacaoException("Nome Inválido, ele deve consistir de ao menos um nome e um sobremone separados por um espaço");
        public ValidacaoException EmailValidcao = new ValidacaoException($@"E-mail Inválido, ele deve ser no formato 'exemplo@gamil.com'");
        public ValidacaoException SexoValidacao = new ValidacaoException("Sexo Inválido, por favor selecione uma das opções válidas de sexo");

    }




}
