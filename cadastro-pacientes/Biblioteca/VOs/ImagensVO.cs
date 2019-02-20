using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Biblioteca.VOs
{
    class ImagensVO
    {
        private string caminho;
        private string pacienteCPF;

        public string Caminho
        {
            get => caminho;
            set => caminho = value;
        }

        protected string PacienteCPF
        {
            get => pacienteCPF;
            set => pacienteCPF = value;
        }

        public void SalvarImagem(OpenFileDialog openFileDialog)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;

            string pastaImagens = path + @"..\..\Assets\imagens\";
            if (Directory.Exists(pastaImagens) == false)
                Directory.CreateDirectory(pastaImagens);



            if (string.IsNullOrEmpty(PacienteCPF))
                throw new Exception("O nome da imagem será igual ao da figurinha, logo este não pode ser vazio ");

            File.Copy(openFileDialog.FileName, (pastaImagens + PacienteCPF + System.IO.Path.GetFileName(openFileDialog.FileName)), false);

            Caminho = PacienteCPF + System.IO.Path.GetFileName(openFileDialog.FileName);

        }

        private void SetImageCPFfromPacienteVO( PacienteVO paciente)
        {
            PacienteCPF =  paciente.PacienteCPF;
        }
    }
}
