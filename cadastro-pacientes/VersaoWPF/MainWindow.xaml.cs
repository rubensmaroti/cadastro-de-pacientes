using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace VersaoWPF
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            VariaveisGlobais.NumerodeJaneas++;
            BackgroundWorker worker = new BackgroundWorker();          

        }
               

        private void BtnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();
                frCadastro frCadastro = new frCadastro();
                frCadastro.ShowDialog();
                while (VariaveisGlobais.NumerodeJaneas > 1)
                {
                    frCadastro.ShowDialog();
                    Thread.Sleep(100);
                }
            }
            finally
            {
                this.Show();
            }
        }

        private void BtnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void BtnSair_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnSalvarImagens_Click(object sender, RoutedEventArgs e)
        {
            try

            {
                this.Hide();
                frSalvarImagens salvarImagem = new frSalvarImagens();
                salvarImagem.ShowDialog();
                while (VariaveisGlobais.NumerodeJaneas > 1)
                {
                    salvarImagem.ShowDialog();
                    Thread.Sleep(100);
                }
            }
            finally
            {
                this.Show();
            }


        }

        private void btnEditarRegistros_Click(object sender, RoutedEventArgs e)
        {
            try

            {
                this.Hide();
                frRegistros registros = new frRegistros();
                registros.ShowDialog();
                while (VariaveisGlobais.NumerodeJaneas > 1)
                {
                    registros.ShowDialog();
                    Thread.Sleep(100);
                }
            }
            finally
            {
                this.Show();
            }
        }
    }
}
