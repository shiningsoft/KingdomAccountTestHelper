using System;
using System.Windows.Forms;
using Yushen.Util;

namespace Xml格式化工具
{
    public partial class Main : Form
    {
        xmlFormatter xmlFormatter = new xmlFormatter();

        public Main()
        {
            InitializeComponent();
        }

        private void btnPreProccess_Click(object sender, EventArgs e)
        {
            tbXmlStr.Text = xmlFormatter.preProccess(tbXmlStr.Text);
        }

        private void convert2params_Click(object sender, EventArgs e)
        {
            tbXmlStr.Text = xmlFormatter.getParams();
        }

        private void convert2setAttr_Click(object sender, EventArgs e)
        {
            tbXmlStr.Text = xmlFormatter.getSetAttr();
        }

        private void convert2memo_Click(object sender, EventArgs e)
        {
            tbXmlStr.Text = xmlFormatter.getMemo();
        }

        private void btnSaveXmlStr_Click(object sender, EventArgs e)
        {
            xmlFormatter.xmlstr = tbXmlStr.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tbXmlStr.Text = xmlFormatter.getXml();
            Clipboard.SetText(tbXmlStr.Text);
        }
    }
}
