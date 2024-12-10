using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_OS
{
    public partial class TelaDesktop : Form
    {
        public TelaDesktop()
        {
            InitializeComponent();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                var confirmResult = MessageBox.Show(
                    "Tem certeza de que deseja sair?",
                    "Confirmação",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }
        }

        private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is TelaSobre)
                {
                    frm.Focus();
                    return;
                }
            }


            TelaSobre formCliente = new TelaSobre();
            formCliente.MdiParent = this;
            formCliente.Show();
        }

        private void usuáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is TelaCadastro)
                {
                    frm.Focus();
                    return;
                }
            }


            TelaCadastro formCliente = new TelaCadastro();
            formCliente.MdiParent = this;
            formCliente.Show();
        }
    }
}
