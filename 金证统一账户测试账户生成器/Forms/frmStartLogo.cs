using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 金证统一账户测试账户生成器
{
    public partial class frmStartLogo : Form
    {
        /// <summary>
        /// 用于启动封面淡出效果的计时器
        /// </summary>
        Timer timerStartLogoFade = new Timer();

        public frmStartLogo()
        {
            InitializeComponent();
        }

        private void frmStartLogo_Shown(object sender, EventArgs e)
        {
#if DEBUG
#else
            timerStartLogoFade.Interval = 2000;
            timerStartLogoFade.Tick += TimerStartLogoFade_Tick;
            timerStartLogoFade.Start();
#endif
        }

        private void TimerStartLogoFade_Tick(object sender, EventArgs e)
        {
            timerStartLogoFade.Stop();
            Close();
        }
    }
}
