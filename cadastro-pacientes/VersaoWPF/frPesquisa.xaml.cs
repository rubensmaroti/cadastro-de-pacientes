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

namespace VersaoWPF
{
    /// <summary>
    /// Lógica interna para frPesquisa.xaml
    /// </summary>
    public partial class frPesquisa : Window
    {
        public frPesquisa()
        {
            InitializeComponent();
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
            
            DataTable data = Metodos.ExecutaSelect("Select * from Paciente ", null);

            foreach (var item in data.Rows)
            {
                datagrid.Items.Add(Metodos.MontaVOPaciente(item as DataRow));
            }

        }

        private void btnSelecionar_Click(object sender, RoutedEventArgs e)
        {
            if(datagrid.SelectedItem != null)
            {
                VariaveisGlobais.pacienteVO = datagrid.SelectedItem as PacienteVO;
                // inserir uma mensagem de confirmação
                this.Close();

            }
        }
    }
}