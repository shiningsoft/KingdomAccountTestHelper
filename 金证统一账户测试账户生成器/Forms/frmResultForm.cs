using System;
using System.Windows.Forms;

namespace 金证统一账户测试账户生成器
{
    public partial class frmResultForm : Form
    {
        public frmResultForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        public void Append(string message)
        {
            this.Show();
            this.infoBox.AppendText(message + Environment.NewLine);
        }

        public void Clear()
        {
            this.infoBox.Text = "";
        }

        private void ResultForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Clear();
            e.Cancel = true;
        }
    }
}
