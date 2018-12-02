using System;
using System.Windows.Forms;

namespace AnimatGeneticos
{
    public partial class Config : Form
    {
        public Config()
        {
            InitializeComponent();
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            int N, M, A;
            try
            {
                N = Int32.Parse(NTextBox.Text);
                M = Int32.Parse(MTextBox.Text);
                A = Int32.Parse(ATextBox.Text);
                Form1 f = new Form1(N, M, A);
                this.Hide();
                f.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Asegurate de ingresar caracteres numericos. "+ex);
                NTextBox.Text = "";
                MTextBox.Text = "";
                ATextBox.Text = "";
            }
        }
    }
}
