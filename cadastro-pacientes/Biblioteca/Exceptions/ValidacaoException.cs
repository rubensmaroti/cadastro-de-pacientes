using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Exceptions
{
    public class ValidacaoException : Exception
    {
        public ValidacaoException(string msg) : base(msg)
        {
        }

        public static ValidacaoException CpfValidacao = new ValidacaoException("CPF Inválido");
        public static ValidacaoException NomeValidacao = new ValidacaoException("Nome Inválido, ele deve consistir de ao menos um nome e um sobremone separados por um espaço");
        public static ValidacaoException EmailValidcao = new ValidacaoException($@"E-mail Inválido, ele deve ser no formato 'exemplo@gmail.com'");
        public static ValidacaoException SexoValidacao = new ValidacaoException("Sexo Inválido, por favor selecione uma das opções válidas de sexo");
        public static ValidacaoException DataNascValidacao = new ValidacaoException("Data Inválida, por favor inserir uma data válida no formato 'dd/mm/yyyy' , que seja ao menos igual ou inferior ao dia de hoje");
        public static ValidacaoException TelefoneValidacao = new ValidacaoException("Telefone Inválido, por favor insira um telefone válido no form");
        public static ValidacaoException ImagemValidaco = new ValidacaoException("Imagem Inválido, por favor escolha uma Imagem valida");

    }




}
