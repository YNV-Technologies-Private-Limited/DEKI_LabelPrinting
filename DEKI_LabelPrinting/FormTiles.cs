using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DEKI_LabelPrinting
{
    public partial class FormTiles : Form
    {
        public FormTiles()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void pbPacking_Click(object sender, EventArgs e)
        {
            FormPacking packing=new FormPacking();
            this.Hide();
            packing.ShowDialog();
            this.Show();
        }

        private void pbWending_Click(object sender, EventArgs e)
        {
            FormWending wending=new FormWending();
            this.Hide();
            wending.ShowDialog();
            this.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.DialogResult=  DialogResult.OK;
        }
    }
}
