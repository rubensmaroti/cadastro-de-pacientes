using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Biblioteca.Enumeradores;
using Microsoft.Win32;
using Biblioteca.Exceptions;


namespace Biblioteca.VOs
{
    public class ImagensVO
    {
        private string caminho;
        private PacienteVO paciente;
        private OpenFileDialog file = new OpenFileDialog();



        public ImagensVO(string caminho, PacienteVO paciente)
        {
            this.caminho = caminho;
            this.paciente = paciente;
        }

        public ImagensVO(PacienteVO paciente, OpenFileDialog file)
        {
            if (string.IsNullOrWhiteSpace(System.IO.Path.GetFileName(file.FileName)))
            {
                throw ValidacaoException.ImagemValidaco;
            }
            else if (paciente == null || string.IsNullOrEmpty(paciente.PacienteCPF))
            {
                throw new Exception("O diretório da imagem será uma pasta nomeada com o cpf do paciente por favor,informe um paciente ");
            }
            else
            {
                this.paciente = paciente;
                this.File = file;
                caminho = System.IO.Path.GetFileName(file.FileName);
            }


        }


        public string Caminho
        {
            get => caminho;
            private set => caminho = value;
        }
        public OpenFileDialog File { get => file; set => file = value; }
        public PacienteVO Paciente { get => paciente; set => paciente = value; }

        public void SalvarImagem()
        {
            if (!string.IsNullOrWhiteSpace(System.IO.Path.GetFileName(File.FileName)))
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                string pastaImagens = path + @"..\..\Registros\Imagens\" + paciente.PacienteCPF;

                if (Directory.Exists(pastaImagens) == false)
                    Directory.CreateDirectory(pastaImagens);

                System.IO.File.Copy(File.FileName, pastaImagens+@"\"+ caminho, false);

            }
            else
                throw new Exception("Arquivo não encontrado");

        }




        public override string ToString()
        {
            if (paciente == null || string.IsNullOrEmpty(paciente.PacienteCPF))
                return base.ToString();
            return paciente.PacienteCPF + " - " + Caminho.Replace(paciente.PacienteCPF, "").Replace(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Registros\Imagens\", "");
        }




    }
}
