using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.VOs;
using Biblioteca.Exceptions;
using Biblioteca.DAOs;
using System.Windows.Media.Imaging;

namespace VersaoWPF
{
    public static class VariaveisGlobais
    {        
        
        static public PacienteVO pacienteVO = new PacienteVO();
        static public int NumerodeJaneas = 0;
        static public BitmapImage image = new BitmapImage(new Uri((AppDomain.CurrentDomain.BaseDirectory + @"..\..\Assets\hospital.png")));

    }
}
