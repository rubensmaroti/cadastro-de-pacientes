using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Biblioteca.VOs;

namespace cadastro_pacientes
{
    public partial class frCadastrodePacientes : Form
    {
        public frCadastrodePacientes()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref Message message)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MOVE = 0xF010;

            switch (message.Msg)
            {
                case WM_SYSCOMMAND:
                    int command = message.WParam.ToInt32() & 0xfff0;
                    if (command == SC_MOVE)
                        return;
                    break;
            }

            base.WndProc(ref message);
        }

        private void frCadastrodePacientes_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                PacienteVO paciente = new PacienteVO();

                paciente.PacienteCPF = mskdtxtCPF.Text;
                paciente.Nome = txtNome.Text;
                if (cbSexo.SelectedItem as string == "Masculino")
                    paciente.Sexo = 'M';
                else if (cbSexo.SelectedItem as string == "Feminino")
                    paciente.Sexo = 'F';
                else
                    paciente.Sexo = 'e';

                paciente.DataNasc = Convert.ToDateTime(mskdtxtData.Text);
                paciente.Email = txtEmai.Text;
                paciente.Telefone = mskdtxtTelefone.Text;
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }
    }
}
