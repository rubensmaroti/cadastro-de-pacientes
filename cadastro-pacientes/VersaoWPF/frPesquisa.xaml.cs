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
using System.Data.SqlClient;
using System.Data;
using Biblioteca.DAOs;
using Biblioteca;
using Biblioteca.Metodos;
using Biblioteca.VOs;
using System.ComponentModel;

namespace VersaoWPF
{
    /// <summary>
    /// Lógica interna para frPesquisa.xaml
    /// </summary>
    public partial class frPesquisa : Window
    {
        List<PacienteVO> pacientes = new List<PacienteVO>();

        public frPesquisa()
        {
            InitializeComponent();
            VariaveisGlobais.NumerodeJaneas++;
            this.Closing += new CancelEventHandler(Window_Closing);
        }

        void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            VariaveisGlobais.NumerodeJaneas--;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnSair_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMinimizarClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            datagrid.ItemsSource = null;

            DataTable data = Metodos.ExecutaSelect("Select * from Paciente ", null);

            foreach (var item in data.Rows)
            {
                PacienteVO x = new PacienteVO();
                x = Metodos.MontaVOPaciente(item as DataRow);
                pacientes.Add(x);
               
            }
            datagrid.ItemsSource = pacientes;

        }

        private void btnSelecionar_Click(object sender, RoutedEventArgs e)
        {
            if(datagrid.SelectedItem != null)
            {
                VariaveisGlobais.pacienteVO = datagrid.SelectedItem as PacienteVO;
                // inserir uma mensagem de confirmação
                this.Close();

            }
            else
            {
                MessageBox.Show("Por Favor Selecione um Paciente na Tabela Acima");
            }
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnPesquisar_Click(object sender, RoutedEventArgs e)
        {

            datagrid.ItemsSource = null;

            if (!string.IsNullOrEmpty(txtCPF.Text.Replace(".", "").Replace("-", "")) && !string.IsNullOrEmpty(txtNome.Text))
            {

                var k =
                from p in pacientes
                where (p.CPF.ToLower().Contains(txtCPF.Text.ToLower().Replace(".", "").Replace("-", "")) && p.Nome.ToLower().Contains(txtNome.Text.ToLower()))
                select p;

                k = k.OrderBy(p => p.Nome);

                datagrid.ItemsSource = k;
            }
            else if (!string.IsNullOrEmpty(txtCPF.Text.Replace(".", "").Replace("-", "")))
            {

               var k =
               from p in pacientes
                where (p.CPF.ToLower().Contains(txtCPF.Text.ToLower().Replace(".", "").Replace("-", "")))
                select p;

                k = k.OrderBy(p => p.Nome);

                datagrid.ItemsSource = k;
            }
            else if (!string.IsNullOrEmpty(txtNome.Text))
            {

                


                var k =
                    from p in pacientes
                    where (p.Nome.ToLower().Contains(txtNome.Text.ToLower()))
                    select p;

               

                datagrid.ItemsSource = k;
            }
            else
                datagrid.ItemsSource = pacientes;


            /*
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            */
        }

        /*
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            txtNome.IsEnabled = true;
            datagrid.IsEnabled = true;
            txtCPF.IsEnabled = true;
            btnSair.IsEnabled = true;
            btnPesquisar.IsEnabled = true;
            btnSelecionar.IsEnabled = true;
            btnInfo.IsEnabled = true;

            MessageBox.Show("Pesquisa Efetuada com sucesso");
        }
        
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if(!string.IsNullOrEmpty(txtNome.Text) && !string.IsNullOrEmpty(txtCPF.Text))
            {               

                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@CPF", txtCPF.Text);
                parameters[1] = new SqlParameter("@Nome", txtNome.Text);

                DataTable data = Metodos.ExecutaSelect("Select * from Paciente  where CPF = @CPF and Nome=@Nome", parameters);
                


            }
        }
        */
    }
}