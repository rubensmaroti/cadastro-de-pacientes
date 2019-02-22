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
            DataTable data = ExecutaSelect("Select * from Paciente ", null);

            foreach (var item in data.Rows)
            {
                datagrid.Items.Add(Metodos.MontaVO(item as DataRow));
                
                

            }

            
        }
        public static DataTable ExecutaSelect(string sql, SqlParameter[] parametros)
        {
            using (SqlConnection conexao = ConexaoBD.GetConexao())
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conexao))
                {
                    if (parametros != null)
                        adapter.SelectCommand.Parameters.AddRange(parametros);
                    DataTable tabelaTemp = new DataTable();
                    adapter.Fill(tabelaTemp);
                    conexao.Close();
                    return tabelaTemp;
                }
            }
        }
}
}