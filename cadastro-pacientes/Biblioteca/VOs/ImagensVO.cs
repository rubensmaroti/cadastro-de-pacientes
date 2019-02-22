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
        private string pacienteCPF;

        public string Caminho
        {
            get => caminho;
            set => caminho = value;
        }

        public string PacienteCPF
        {
            get => pacienteCPF;
            set => pacienteCPF = value;
        }

        public void SalvarImagem(OpenFileDialog openFileDialog)
        {
            if (string.IsNullOrWhiteSpace(System.IO.Path.GetFileName(openFileDialog.FileName)))
            {
                throw ValidacaoException.ImagemValidaco;
            }
            else
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;

                string pastaImagens = path + @"..\..\Registros\Imagens\" + PacienteCPF;
                if (Directory.Exists(pastaImagens) == false)
                    Directory.CreateDirectory(pastaImagens);



                if (string.IsNullOrEmpty(PacienteCPF))
                    throw new Exception("O diretório da imagem será uma pasta nomeada com o cpf do paciente por favor o preencha.");

                File.Copy(openFileDialog.FileName, (pastaImagens + @"\" + pacienteCPF + System.IO.Path.GetFileName(openFileDialog.FileName)), false);

                Caminho = PacienteCPF + System.IO.Path.GetFileName(openFileDialog.FileName);
            }


        }

        private void SetImageCPFfromPacienteVO(PacienteVO paciente)
        {
            PacienteCPF = paciente.PacienteCPF;
        }
    }
}
