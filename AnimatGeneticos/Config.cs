using System;
using System.Windows.Forms;

namespace AnimatGeneticos
{
    public partial class Config : Form
    {
        public Config()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.animat1;
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            int N, M, A;
            try
            {
                N = Int32.Parse(NTextBox.Text);
                M = Int32.Parse(MTextBox.Text);
                A = Int32.Parse(ATextBox.Text);

                if (N % 2 == 0 || M % 2 == 0)
                {
                    NTextBox.Text = "";
                    MTextBox.Text = "";
                    MessageBox.Show("Las longitudes del tablero deben ser impares");
                    return;
                }
                Form1 f = new Form1(N, M, A);
                this.Hide();
                f.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Asegurate de ingresar caracteres numericos. ");
                NTextBox.Text = "";
                MTextBox.Text = "";
                ATextBox.Text = "";
            }
        }
    }
}
