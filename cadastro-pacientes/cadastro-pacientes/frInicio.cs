using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cadastro_pacientes
{
    public partial class frInicio : Form
    {
        public frInicio()
        {
            InitializeComponent();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            this.Hide();
            frCadastrodePacientes frCadastrodePacientes = new frCadastrodePacientes();
            frCadastrodePacientes.ShowDialog();
            this.Show();

        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            this.Hide();
            frUploadDeImagens frImagens = new frUploadDeImagens();
            frImagens.ShowDialog();
            this.Show();
        }
    }
}
