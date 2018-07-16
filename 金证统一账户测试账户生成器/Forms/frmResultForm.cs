using System;
using System.Windows.Forms;

namespace 金证统一账户测试账户生成器
{
    public partial class frmResultForm : Form
    {
        private int positionX;
        private int positionY;
        frmFramework frmFramework;

        public frmResultForm(frmFramework form)
        {
            frmFramework = form;
            positionX = Location.X - frmFramework.Location.X;
            positionY = Location.Y - frmFramework.Location.Y;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        public void Append(string message)
        {
            this.Show();
            this.Activate();
            this.infoBox.AppendText(message + Environment.NewLine);
        }

        public void Clear()
        {
            this.infoBox.Text = "";
        }

        /// <summary>
        /// 根据相对主窗体的坐标重新定位
        /// </summary>
        public void refreshLocation()
        {
            Console.WriteLine("X:" + positionX + "\tY:" + positionY);
            Location = new System.Drawing.Point(frmFramework.Location.X + positionX, frmFramework.Location.Y + positionY);
        }

        private void ResultForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Clear();
            e.Cancel = true;
        }

        /// <summary>
        /// 移动时计算与主窗体的相对位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmResultForm_LocationChanged(object sender, EventArgs e)
        {
            positionX = Location.X - frmFramework.Location.X;
            positionY = Location.Y - frmFramework.Location.Y;
        }
    }
}
