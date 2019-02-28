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
using System.ComponentModel;
using System.Threading;
using Biblioteca.DAOs;
using System.IO;

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
            VariaveisGlobais.NumerodeJaneas++;
            this.Closing += new CancelEventHandler(YourWindow_Closing);
        }

        void YourWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            VariaveisGlobais.pacienteVO = new PacienteVO();
            VariaveisGlobais.NumerodeJaneas--;
        }

        private void BtnSalvarImagens_Click(object sender, RoutedEventArgs e)
        {
            if (lbImagens.Items.Count == 0)
            {
                MessageBox.Show("Adicione Imagens primeiro");
                return;
            }
            else
            {
                MessageBox.Show("As imagens afim de evitar conflitos são salvas com uma key seguidas de nome atual");

                using (BackgroundWorker worker = new BackgroundWorker())
                {
                    worker.WorkerReportsProgress = true;
                    worker.WorkerSupportsCancellation = true;
                    worker.DoWork += worker_DoWork;
                    worker.ProgressChanged += worker_ProgressChanged;
                    worker.RunWorkerCompleted += worker_RunWorkerCompleted;


                    btnCadastrar.IsEnabled = false;
                    btnRemoverImagem.IsEnabled = false;
                    btnSair.IsEnabled = false;
                    btnSeleImagem.IsEnabled = false;
                    btnSelePaciente.IsEnabled = false;

                    worker.RunWorkerAsync();

                }
            }
        }
        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            btnCadastrar.IsEnabled = true;
            btnRemoverImagem.IsEnabled = true;
            btnSair.IsEnabled = true;
            btnSeleImagem.IsEnabled = true;
            btnSelePaciente.IsEnabled = true;
            pbWork.Value = 0;
            while (contador != 0)
            {
                lbImagens.Items.RemoveAt(0);
                contador--;
            }
            if (lbImagens.Items.Count == 0)
            {
                MessageBox.Show("Imagens Salvas Com sucesso", "Alerta!!!", MessageBoxButton.OK, MessageBoxImage.Asterisk);

            }

        }

        private void BtnSelecinar_Paciente(object sender, RoutedEventArgs e)
        {
            this.Hide();
            frPesquisa fr = new frPesquisa();
            fr.ShowDialog();

            if (VariaveisGlobais.pacienteVO != null && !string.IsNullOrEmpty(VariaveisGlobais.pacienteVO.CPF))
            {

                txtCPF.Text = VariaveisGlobais.pacienteVO.CPF.Substring(0, 3) + "." + VariaveisGlobais.pacienteVO.CPF.Substring(3, 3) + "." +
                    VariaveisGlobais.pacienteVO.CPF.Substring(6, 3) + "-" + VariaveisGlobais.pacienteVO.CPF.Remove(0, 9);

                txtNome.Text = VariaveisGlobais.pacienteVO.Nome;
            }
            this.Activate();
        }

        private void btnSelecionarImagem_Clik(object sender, RoutedEventArgs e)
        {
            try
            {

                if (VariaveisGlobais.pacienteVO.CPF == null)
                {
                    MessageBox.Show("Porfavor Escolha um paciente primeiro.");
                    return;
                }

                System.Windows.Forms.DialogResult result;
                using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
                {
                    result = dialog.ShowDialog();
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        DirectoryInfo directory = new DirectoryInfo(dialog.SelectedPath);

                        var x = (MessageBox.Show("Deseja buscar imagens nas subpastas deste diretório também? \n(ISTO IRA LER TODAS AS IMAGENS RELACIONADAS AS SUBPASTAS DO DIRETÓRIO TAMBÉM)".ToUpper(), "ATENÇÃO !!!!!", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.Yes);
                        BuscaArquivos(directory, x);
                    }
                }

            }
            catch(Exception x)
            {

                MessageBox.Show(x.Message);
            }


        }

        private void BuscaArquivos(DirectoryInfo dir, bool sub)
        {            

            foreach (FileInfo file in dir.GetFiles())
            {
                if (file.Extension == ".png" || file.Extension == ".jpg")
                {
                    lbImagens.Items.Add(new ImagensVO(VariaveisGlobais.pacienteVO, file));
                }
            }


            if (sub)
            {

                foreach (DirectoryInfo subDir in dir.GetDirectories())
                {
                    BuscaArquivos(subDir, sub);
                }
            }
        }


        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        int contador = 0;

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if ((sender as BackgroundWorker).CancellationPending)
                return;

            int x = lbImagens.Items.Count;

            try
            {
                foreach (var item in lbImagens.Items)
                {

                    (item as ImagensVO).SalvarImagem();
                    ImagemDAO.Insert(item as ImagensVO);
                    contador++;
                    int razao = Convert.ToInt32((contador / (double)x) * 100);
                    (sender as BackgroundWorker).ReportProgress(razao);
                    Thread.Sleep(50);
                }
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show(string.Format("Primeira imagem da lista já foi salva anterior mente favor remover"), "Alerta!!!", MessageBoxButton.OK, MessageBoxImage.Warning);
                (sender as BackgroundWorker).CancelAsync();

            }
            catch (Exception y)
            {
                MessageBox.Show(string.Format("Erro Inesperado:{0}", y.Message), "Alerta!!!", MessageBoxButton.OK, MessageBoxImage.Warning);
                (sender as BackgroundWorker).CancelAsync();
            }

        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbWork.Value = e.ProgressPercentage;
        }

        private void lbImagens_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbImagens.SelectedIndex != -1)
                Imagem.Source = new BitmapImage(new Uri((lbImagens.SelectedItem as ImagensVO).File.FullName));

        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnSair_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }


        private void btnRemoverImagem_Click(object sender, RoutedEventArgs e)
        {
            if (lbImagens.SelectedIndex != -1)
            {
                if ((MessageBox.Show("Realmente Deseja Remover esta imagem da seleção ?", "ATENÇÃO !!!!!", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.Yes))
                {
                    lbImagens.Items.RemoveAt(lbImagens.SelectedIndex);
                }

            }
            else
                MessageBox.Show("Selecione uma imagem na lista de imagens, para removela");
        }


    }
}
