using Biblioteca.Metodos;
using Biblioteca.VOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using Biblioteca.Enumeradores;
using Biblioteca.DAOs;
using System.Threading;

namespace VersaoWPF
{
    /// <summary>
    /// Lógica interna para frRegistros.xaml
    /// </summary>
    public partial class frRegistros : Window
    {
        public frRegistros()
        {
            InitializeComponent();
            VariaveisGlobais.NumerodeJaneas++;
            this.Closing += new CancelEventHandler(Window_Closing);


            cbTipo.Items.Add(Tipo.Paciente);
            cbTipo.Items.Add(Tipo.Imagem);

        }

        List<Registro> list = new List<Registro>();

        void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            VariaveisGlobais.NumerodeJaneas--;
        }

        private void btnSair_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMinimizarClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnPesquisar_Click(object sender, RoutedEventArgs e)
        {

            var source = new List<Registro>();
            

            if(cbTipo.SelectedIndex == 0)
            {
               source.AddRange( from p in list where p.Tipo == Tipo.Paciente select p);
            }
            else if(cbTipo.SelectedIndex == 1)
            {
                source.AddRange(from p in list where p.Tipo == Tipo.Imagem select p);
            }
            else
            {
                source.AddRange(list);
            }

            if (!string.IsNullOrEmpty(txtCPF.Text.Replace(".", "").Replace("-", "")) && !string.IsNullOrEmpty(txtNome.Text))
            {

                var k =
                from p in source
                where (p.CPF.ToLower().Contains(txtCPF.Text.ToLower().Replace(".","").Replace("-","")) && p.Nome.ToLower().Contains(txtNome.Text.ToLower()))
                select p;

                k = k.OrderBy(p => p.Nome);

                datagrid.ItemsSource = k;
            }
            else if (!string.IsNullOrEmpty(txtCPF.Text.Replace(".", "").Replace("-", "")))
            {

                var k =
                from p in source
                where (p.CPF.ToLower().Contains(txtCPF.Text.ToLower().Replace(".", "").Replace("-", "")))
                select p;

                k = k.OrderBy(p => p.Nome);

                datagrid.ItemsSource = k;
            }
            else if (!string.IsNullOrEmpty(txtNome.Text))
            {
                var k =
                    from p in source
                    where (p.Nome.ToLower().Contains(txtNome.Text.ToLower()))
                    select p;

                datagrid.ItemsSource = k;
            }
            else
            {
                datagrid.ItemsSource = source;
            }

        }

        private void btnDeletar_Click(object sender, RoutedEventArgs e)
        {
            
            if(datagrid.SelectedItem is PacienteVO)
            {
                if ((MessageBox.Show("Realmente Deseja Remover este Registro ?(ISTO IRA DELETAR TODAS AS IMAGENS RELACIONADAS AO PACIENTE TAMBEM)", "ATENÇÃO !!!!!", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.Yes))
                {                    

                    var k = from p in list where ((p is ImagensVO) && p.CPF == (datagrid.SelectedItem as PacienteVO).CPF) select p;

                    foreach (var item in k)
                    {                        
                        ImagemDAO.Delete(item as ImagensVO);
                        string x = AppDomain.CurrentDomain.BaseDirectory;
                        string y = x + @"..\..\Registros\Imagens\" + (item as ImagensVO).CPF + (item as ImagensVO).Caminho;

                        System.IO.File.Delete(y);

                    }

                    string path = AppDomain.CurrentDomain.BaseDirectory;
                    string caminho = path + @"..\..\Registros\Imagens\" + (datagrid.SelectedItem as PacienteVO).CPF;

                    PacienteDAO.Delete((datagrid.SelectedItem as PacienteVO).CPF);
                    list.RemoveAll(s => s.CPF == (datagrid.SelectedItem as Registro).CPF);

                    datagrid.ItemsSource = null;
                    datagrid.ItemsSource = list;



                    if (System.IO.Directory.Exists(caminho))
                        System.IO.Directory.Delete(caminho, true);
                  

                }

                
            }
            else if (datagrid.SelectedItem is ImagensVO)
            {
                if ((MessageBox.Show("Realmente Deseja Remover este Registro ?", "ATENÇÃO !!!!!", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.Yes))
                {
                    
                    ImagemDAO.Delete(datagrid.SelectedItem as ImagensVO);


                    (datagrid.SelectedItem as ImagensVO).File.Delete();



                    list.Remove(datagrid.SelectedItem as Registro);
                    datagrid.ItemsSource = null;
                    datagrid.ItemsSource = list;


                }
                
            }
            else
            {
                MessageBox.Show("Escolha um arquivo para deletar primeiro");
            }
        }

     
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            // GridConfig();
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            worker.RunWorkerAsync();

            MessageBox.Show("Porvafor aguarde alguns instantes em quanto os dados são carregados");
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbProgress.Value = e.ProgressPercentage;
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            datagrid.ItemsSource = list;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            list = new List<Registro>();
           

            DataTable data = Metodos.ExecutaSelect("Select * from Paciente ", null);

            DataTable data2 = Metodos.ExecutaSelect("Select * from Imagens ", null);

            int k, y;
            k = data.Rows.Count + data2.Rows.Count;
            y = 0;
            var bolean = k > 100 ? false : true;

            foreach (var item in data.Rows)
            {
                PacienteVO x = new PacienteVO();
                x = Metodos.MontaVOPaciente(item as DataRow);
                list.Add(x);
                y++;
                int razao =Convert.ToInt32( y /(double) k * 100);
                (sender as BackgroundWorker).ReportProgress(razao);
                if (bolean)
                    Thread.Sleep(100);
               
            }
            foreach (var item in data2.Rows)
            {
                ImagensVO x = Metodos.MontaVOImagem(item as DataRow);
                list.Add(x);
                y++;
                int razao = Convert.ToInt32(y / (double)k * 100);
                (sender as BackgroundWorker).ReportProgress(razao);
                if (bolean)
                    Thread.Sleep(100);

            }
        }

       
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            //GridConfig();
        }
    }
}
