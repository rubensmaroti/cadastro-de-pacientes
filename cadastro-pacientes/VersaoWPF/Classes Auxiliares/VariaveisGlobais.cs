using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.VOs;
using Biblioteca.Exceptions;
using Biblioteca.DAOs;

namespace VersaoWPF
{
    public static class VariaveisGlobais
    {
        static public List<PacienteVO> Pacientes = new List<PacienteVO>();
        static public List<ImagensVO> Imagens = new List<ImagensVO>();
        static public PacienteVO pacienteVO = new PacienteVO();

    }
}
