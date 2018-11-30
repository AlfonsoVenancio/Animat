using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimatGeneticos
{
    public partial class Config : Form
    {
        public Config()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            int N, M, A;
            N = Int32.Parse(NTextBox.Text);
            M = Int32.Parse(MTextBox.Text);
            A = Int32.Parse(ATextBox.Text);

            Form1 f = new Form1(N,M,A);
            f.Show();
            this.Visible = false;
        }
    }
}
