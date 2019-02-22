using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using Biblioteca.VOs;


namespace VersaoWPF
{
    /// <summary>
    /// Lógica interna para frSalvarImagens.xaml
    /// </summary>
    public partial class frSalvarImagens : Window
    {
        public frSalvarImagens()
        {
            InitializeComponent();
        }

        private void btnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();

            file.Filter = "Imagens|*.jpg;*.bmp;*.gif;*.png";
            if (file.ShowDialog() == true)
            {
                
                Imagem.Source = new BitmapImage(new Uri(file.FileName));
            }

            ImagensVO imagens = new ImagensVO();

            imagens.PacienteCPF = "12334566";
            imagens.SalvarImagem(file);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            frPesquisa fr = new frPesquisa();

            fr.ShowDialog();
        }
    }
}
