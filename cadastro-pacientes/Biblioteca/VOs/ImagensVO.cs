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
    public class ImagensVO : Registro
    {
        
        private PacienteVO paciente;
        private FileInfo file;




        public ImagensVO(string caminho, PacienteVO paciente)
        {
            this.Nome = caminho;
            this.paciente = paciente;
            this.CPF = paciente.CPF;
            Tipo = Tipo.Imagem;
        }

        public ImagensVO(PacienteVO paciente, FileInfo file)
        {
            if (string.IsNullOrWhiteSpace(file.Name))
            {
                throw ValidacaoException.ImagemValidaco;
            }
            else if (paciente == null || string.IsNullOrEmpty(paciente.CPF))
            {
                throw new Exception("O diretório da imagem será uma pasta nomeada com o cpf do paciente por favor,informe um paciente ");
            }
            else
            {
                this.CPF = paciente.CPF;
                this.paciente = paciente;
                this.File = file;
                Nome = file.Name;
                Tipo = Tipo.Imagem;

            }


        }

        

        public string Caminho
        {
            get => Nome;
            private set => Nome = value;
        }

        public FileInfo File { get => file; set => file = value; }
        public PacienteVO Paciente { get => paciente; set => paciente = value; }

        public void SalvarImagem()
        {
            if (!string.IsNullOrWhiteSpace(System.IO.Path.GetFileName(File.Name)))
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                string pastaImagens = path + @"..\..\Registros\Imagens\" + paciente.CPF;

                if (Directory.Exists(pastaImagens) == false)
                    Directory.CreateDirectory(pastaImagens);

                System.IO.File.Copy(File.FullName, pastaImagens+@"\"+ Nome, false);

            }
            else
                throw new Exception("Arquivo não encontrado");

        }




        public override string ToString()
        {
            if (paciente == null || string.IsNullOrEmpty(paciente.CPF))
                return base.ToString();
            return paciente.CPF + " - " + Caminho.Replace(paciente.CPF, "").Replace(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Registros\Imagens\", "");
        }




    }
}
