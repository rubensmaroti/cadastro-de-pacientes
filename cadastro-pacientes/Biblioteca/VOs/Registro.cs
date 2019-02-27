using Biblioteca.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.VOs
{
    public abstract class Registro
    {

        private Tipo tipo;
        private string cpf;
        private string nome;

        public virtual string CPF { get => cpf;  set => cpf = value; }
        public virtual string Nome { get => nome; set => nome = value; }
        public Tipo Tipo { get => tipo; protected set => tipo = value; }
    }
}
