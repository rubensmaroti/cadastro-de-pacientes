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
using Biblioteca.VOs;
using Biblioteca.Exceptions;
using Biblioteca.DAOs;
using System.ComponentModel;

namespace VersaoWPF
{
    /// <summary>
    /// Lógica interna para frCadastro.xaml
    /// </summary>
    public partial class frCadastro : Window
    {
        public frCadastro()
        {
            InitializeComponent();
            VariaveisGlobais.NumerodeJaneas++;
            this.Closing += new CancelEventHandler(YourWindow_Closing);
        }

        void YourWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            VariaveisGlobais.NumerodeJaneas--;
        }

        private void TxtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        #region Janela
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbSexo.Items.Add("Masculino");
            cbSexo.Items.Add("Feminino");
            txtCPF.MaxLength = 14;
            txtTelefone.MaxLength = 14;
            txtDataDeNascimento.MaxLength = 10;

        }
        #endregion
        #region Botôes
        private void BtnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void BtnSair_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void BtnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PacienteVO paciente = new PacienteVO();

                paciente.PacienteCPF = txtCPF.Text;
                paciente.Nome = txtNome.Text;
                if (cbSexo.SelectedItem as string == "Masculino")
                    paciente.Sexo = 'M';
                else if (cbSexo.SelectedItem as string == "Feminino")
                    paciente.Sexo = 'F';
                else
                    paciente.Sexo = 'e';

                paciente.DataNasc = Convert.ToDateTime(txtDataDeNascimento.Text);
                paciente.Email = txtEmail.Text;
                paciente.Telefone = txtTelefone.Text;

                PacienteDAO.Insert(paciente);
                MessageBox.Show("Cadastro Efetuado");

                txtCPF.Clear();
                txtDataDeNascimento.Clear();
                txtEmail.Clear();
                txtTelefone.Clear();
                txtNome.Clear();
                cbSexo.SelectedIndex = -1;

            }
            catch (ValidacaoException x)
            {
                MessageBox.Show(x.Message);
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        #endregion
        #region Eventos txtCPF

        private string cpfimput = "";

        private void txtCPF_KeyDown(object sender, KeyEventArgs e)
        {

            if (txtCPF.Text.Length == 3 && !(e.Key == Key.AbntC2 || e.Key == Key.OemPeriod))
            {

                cpfimput += ".";
                txtCPF.Text = txtCPF.Text + ".";
                txtCPF.Select(txtCPF.Text.Length, 0);

            }
            else if (txtCPF.Text.Length == 7 && !(e.Key == Key.AbntC2 || e.Key == Key.OemPeriod))
            {
                cpfimput += ".";
                txtCPF.Text = txtCPF.Text + ".";
                txtCPF.Select(txtCPF.Text.Length, 0);
            }
            else if (txtCPF.Text.Length == 11 && !(e.Key == Key.OemMinus || e.Key == Key.Subtract))
            {
                cpfimput += "-";
                txtCPF.Text = txtCPF.Text + "-";
                txtCPF.Select(txtCPF.Text.Length, 0);
            }

        }

        private void txtCPF_PreviewKeyUp(object sender, KeyEventArgs e)
        {

            if (e.Key != Key.Back)
                SupressaoDeKeysCPF();

        }

        private void txtCPF_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key != Key.Back)
                SupressaoDeKeysCPF();
        }


        private void txtCPF_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((e.OriginalSource as TextBox).Text.Length > 14)
            {
                (e.OriginalSource as TextBox).Text = cpfimput;
            }
            else if (txtCPF.Text != cpfimput)
            {
                SupressaoDeKeysCPF();
                int count = 0;
                if (cpfimput.Length > 0 && txtCPF.Text != "")
                {
                    while ((txtCPF.Text[count] == cpfimput[count]))
                    {
                        count++;
                        if (count == txtCPF.Text.Length || count == cpfimput.Length)
                            break;
                    }
                }
                if (txtCPF.Text.Length > cpfimput.Length)
                    count++;


                cpfimput = "";
                string texto = txtCPF.Text;

                texto = texto.Replace(".", "").Replace("-", "");

                char[] s = texto.ToCharArray();

                for (int i = 0; i < s.Length; i++)
                {
                    if (cpfimput.Length == 3)
                        cpfimput += ".";
                    if (cpfimput.Length == 7)
                        cpfimput += ".";
                    if (cpfimput.Length == 11)
                        cpfimput += "-";
                    cpfimput += s[i];

                }



                txtCPF.Text = cpfimput;


                txtCPF.CaretIndex = count;
            }

        }
        #endregion
        #region Eventos txtTelefone

        string telimput = "";
        private void txtTelefone_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((e.OriginalSource as TextBox).Text.Length > 14)
            {
                (e.OriginalSource as TextBox).Text = telimput;
            }
            else if (txtTelefone.Text != telimput)
            {
                SupressaoDeKeyTele();
                int count = 0;
                if (telimput.Length > 0 && txtTelefone.Text != "")
                {
                    while ((txtTelefone.Text[count] == telimput[count]))
                    {
                        count++;
                        if (count == txtTelefone.Text.Length || count == telimput.Length)
                            break;
                    }
                }
                if (txtTelefone.Text.Length > telimput.Length)
                    count++;


                telimput = "";
                string texto = txtTelefone.Text;

                texto = texto.Replace("(", "").Replace(")", "").Replace("-", "");

                char[] s = texto.ToCharArray();

                for (int i = 0; i < s.Length; i++)
                {
                    if (telimput.Length == 0)
                    {
                        telimput += "(";
                        count++;
                    }
                    if (telimput.Length == 3)
                    {
                        telimput += ")";
                        count++;
                    }
                    if ((telimput.Length == 8 && s.Length <= 10))
                    {
                        telimput += "-";
                        count++;
                    }
                    if ((telimput.Length == 9 && s.Length == 11))
                    {
                        telimput += "-";
                        count++;
                    }



                    telimput += s[i];

                }



                txtTelefone.Text = telimput;


                txtTelefone.CaretIndex = count;
            }
        }




        private void txtTelefone_KeyDown(object sender, KeyEventArgs e)
        {


            if (txtTelefone.Text.Length == 3)
            {
                txtTelefone.Text = txtTelefone.Text + ")";
                txtTelefone.Select(txtTelefone.Text.Length, 0);
            }
            if (txtTelefone.Text.Length == 8 && !(e.Key == Key.OemMinus || e.Key == Key.Subtract))
            {
                txtTelefone.Text = txtTelefone.Text + "-";
                txtTelefone.Select(txtTelefone.Text.Length, 0);
            }



        }

        private void txtTelefone_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key != Key.Back)
                SupressaoDeKeyTele();



        }

        private void txtTelefone_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Back)
                SupressaoDeKeyTele();
        }
        #endregion
        #region Metodos de Supressão
        private void SupressaoDeKeysCPF()
        {
            char[] s = txtCPF.Text.ToCharArray();
            string cpf = "";

            for (int i = 0; i <= s.Length - 1; i++)
            {
                bool numeros = (s[i] == '1' || s[i] == '2' || s[i] == '3' || s[i] == '4' || s[i] == '5' || s[i] == '6' || s[i] == '7' || s[i] == '8' || s[i] == '9' || s[i] == '0');
                bool pontuacoes = ((i == 3 && s[i] == '.') || (i == 7 && s[i] == '.') || (i == 11 && s[i] == '-'));

                if (!(numeros || pontuacoes))
                    s[i] = 'k';

                cpf += s[i];

            }

            txtCPF.Text = cpf.Replace("k", "");



        }
        private void SupressaoDeKeysData()
        {
            char[] s = txtCPF.Text.ToCharArray();
            string cpf = "";

            for (int i = 0; i <= s.Length - 1; i++)
            {
                bool numeros = (s[i] == '1' || s[i] == '2' || s[i] == '3' || s[i] == '4' || s[i] == '5' || s[i] == '6' || s[i] == '7' || s[i] == '8' || s[i] == '9' || s[i] == '0');
                bool pontuacoes = ((i == 2 && s[i] == '/') || (i == 5 && s[i] == '/'));

                if (!(numeros || pontuacoes))
                    s[i] = 'k';

                cpf += s[i];

            }

            txtCPF.Text = cpf.Replace("k", "");



        }


        private void SupressaoDeKeyTele()
        {
            char[] s = txtTelefone.Text.ToCharArray();
            string cpf = "";

            for (int i = 0; i <= s.Length - 1; i++)
            {
                bool numeros = (s[i] == '1' || s[i] == '2' || s[i] == '3' || s[i] == '4' || s[i] == '5' || s[i] == '6' || s[i] == '7' || s[i] == '8' || s[i] == '9' || s[i] == '0');
                bool parenteses = (i == 0 && s[i] == '(') || (i == 3 && s[i] == ')');
                bool traco = ((i == 8 && s[i] == '-') ^ (i + 1 < s.Length && i == 8 && s[i + 1] == '-')) || (i == 9 && s[i] == '-');

                if (!(numeros || parenteses || traco))
                    s[i] = 'k';

                cpf += s[i];

            }

            txtTelefone.Text = cpf.Replace("k", "");


        }




        #endregion
        #region Eventos txtData
        string dataimput = "";
        private void txtDataDeNascimento_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((e.OriginalSource as TextBox).Text.Length > 14)
            {
                (e.OriginalSource as TextBox).Text = dataimput;
            }
            else if (txtDataDeNascimento.Text != dataimput)
            {
                SupressaoDeKeysData(); ///sssss 
                int count = 0;
                if (dataimput.Length > 0 && txtDataDeNascimento.Text != "")
                {
                    while ((txtDataDeNascimento.Text[count] == dataimput[count]))
                    {
                        count++;
                        if (count == txtDataDeNascimento.Text.Length || count == dataimput.Length)
                            break;
                    }
                }
                if (txtDataDeNascimento.Text.Length > dataimput.Length)
                    count++;


                dataimput = "";
                string texto = txtDataDeNascimento.Text;

                texto = texto.Replace("/", "").Replace(@"\", "");

                char[] s = texto.ToCharArray();

                for (int i = 0; i < s.Length; i++)
                {
                    if (dataimput.Length == 2)
                    {
                        dataimput += "/";
                        count++;
                    }
                    if (dataimput.Length == 5)
                    {
                        dataimput += "/";
                        count++;
                    }

                    dataimput += s[i];

                }



                txtDataDeNascimento.Text = dataimput;


                txtDataDeNascimento.CaretIndex = count;
            }
        }
        #endregion

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {

        }
    }
}

