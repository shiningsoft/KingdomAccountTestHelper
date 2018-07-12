using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace 金证统一账户测试账户生成器
{
    partial class frmFramework
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            if (kess != null)
            {
                kess.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFramework));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.tsmiFunction = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.操作员退出toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.自动重新登录toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.自定义数据字典ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.查看当前日志ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开日志目录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelCurrentServer = new System.Windows.Forms.ToolStripStatusLabel();
            this.currentUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.requestQueueCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslExpired = new System.Windows.Forms.ToolStripStatusLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel = new System.Windows.Forms.Panel();
            this.menuStrip.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.AllowMerge = false;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFunction,
            this.toolStripMenuItem1,
            this.关于ToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1010, 25);
            this.menuStrip.TabIndex = 8;
            this.menuStrip.Text = "menuStrip";
            // 
            // tsmiFunction
            // 
            this.tsmiFunction.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator2,
            this.toolStripMenuItem2,
            this.操作员退出toolStripMenuItem,
            this.自动重新登录toolStripMenuItem,
            this.toolStripSeparator3,
            this.自定义数据字典ToolStripMenuItem,
            this.系统设置ToolStripMenuItem,
            this.toolStripSeparator1,
            this.退出ToolStripMenuItem});
            this.tsmiFunction.Name = "tsmiFunction";
            this.tsmiFunction.Size = new System.Drawing.Size(68, 21);
            this.tsmiFunction.Text = "功能列表";
            this.tsmiFunction.MouseHover += new System.EventHandler(this.tsmiFunction_MouseHover);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(157, 6);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(160, 22);
            this.toolStripMenuItem2.Text = "操作员登录";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.tsmiOperatorLogin_Click);
            // 
            // 操作员退出toolStripMenuItem
            // 
            this.操作员退出toolStripMenuItem.Name = "操作员退出toolStripMenuItem";
            this.操作员退出toolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.操作员退出toolStripMenuItem.Text = "操作员退出";
            this.操作员退出toolStripMenuItem.Click += new System.EventHandler(this.操作员退出toolStripMenuItem_Click);
            // 
            // 自动重新登录toolStripMenuItem
            // 
            this.自动重新登录toolStripMenuItem.Checked = true;
            this.自动重新登录toolStripMenuItem.CheckOnClick = true;
            this.自动重新登录toolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.自动重新登录toolStripMenuItem.Name = "自动重新登录toolStripMenuItem";
            this.自动重新登录toolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.自动重新登录toolStripMenuItem.Text = "自动重新登录";
            this.自动重新登录toolStripMenuItem.Click += new System.EventHandler(this.自动重新登录toolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(157, 6);
            // 
            // 自定义数据字典ToolStripMenuItem
            // 
            this.自定义数据字典ToolStripMenuItem.Name = "自定义数据字典ToolStripMenuItem";
            this.自定义数据字典ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.自定义数据字典ToolStripMenuItem.Text = "自定义数据字典";
            this.自定义数据字典ToolStripMenuItem.Click += new System.EventHandler(this.自定义数据字典ToolStripMenuItem_Click);
            // 
            // 系统设置ToolStripMenuItem
            // 
            this.系统设置ToolStripMenuItem.Name = "系统设置ToolStripMenuItem";
            this.系统设置ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.系统设置ToolStripMenuItem.Text = "系统设置";
            this.系统设置ToolStripMenuItem.Click += new System.EventHandler(this.系统设置ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(157, 6);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查看当前日志ToolStripMenuItem,
            this.打开日志目录ToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(44, 21);
            this.toolStripMenuItem1.Text = "日志";
            // 
            // 查看当前日志ToolStripMenuItem
            // 
            this.查看当前日志ToolStripMenuItem.Name = "查看当前日志ToolStripMenuItem";
            this.查看当前日志ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.查看当前日志ToolStripMenuItem.Text = "查看当前日志";
            this.查看当前日志ToolStripMenuItem.Click += new System.EventHandler(this.查看当前日志ToolStripMenuItem_Click);
            // 
            // 打开日志目录ToolStripMenuItem
            // 
            this.打开日志目录ToolStripMenuItem.Name = "打开日志目录ToolStripMenuItem";
            this.打开日志目录ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.打开日志目录ToolStripMenuItem.Text = "打开日志目录";
            this.打开日志目录ToolStripMenuItem.Click += new System.EventHandler(this.打开日志目录ToolStripMenuItem_Click);
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.关于ToolStripMenuItem.Text = "关于";
            this.关于ToolStripMenuItem.Click += new System.EventHandler(this.关于ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelCurrentServer,
            this.currentUser,
            this.requestQueueCount,
            this.tsslVersion,
            this.tsslExpired});
            this.statusStrip1.Location = new System.Drawing.Point(0, 507);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1010, 26);
            this.statusStrip1.TabIndex = 75;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelCurrentServer
            // 
            this.toolStripStatusLabelCurrentServer.Name = "toolStripStatusLabelCurrentServer";
            this.toolStripStatusLabelCurrentServer.Size = new System.Drawing.Size(68, 21);
            this.toolStripStatusLabelCurrentServer.Text = "当前环境：";
            // 
            // currentUser
            // 
            this.currentUser.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.currentUser.Name = "currentUser";
            this.currentUser.Size = new System.Drawing.Size(48, 21);
            this.currentUser.Text = "用户：";
            // 
            // requestQueueCount
            // 
            this.requestQueueCount.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.requestQueueCount.Name = "requestQueueCount";
            this.requestQueueCount.Size = new System.Drawing.Size(182, 21);
            this.requestQueueCount.Text = "请求队列长度：0，当前并发：0";
            // 
            // tsslVersion
            // 
            this.tsslVersion.Name = "tsslVersion";
            this.tsslVersion.Size = new System.Drawing.Size(637, 21);
            this.tsslVersion.Spring = true;
            this.tsslVersion.Text = "当前版本：";
            this.tsslVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tsslExpired
            // 
            this.tsslExpired.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tsslExpired.Name = "tsslExpired";
            this.tsslExpired.Size = new System.Drawing.Size(60, 21);
            this.tsslExpired.Text = "有效期：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            // 
            // panel
            // 
            this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel.Location = new System.Drawing.Point(12, 28);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(986, 476);
            this.panel.TabIndex = 76;
            // 
            // frmFramework
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 533);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "frmFramework";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "金证统一账户测试账户生成器";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmFramework_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiFunction;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelCurrentServer;
        private System.Windows.Forms.ToolStripStatusLabel currentUser;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripStatusLabel requestQueueCount;
        private System.Windows.Forms.ToolStripStatusLabel tsslVersion;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 查看当前日志ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开日志目录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem 操作员退出toolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem 自定义数据字典ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 系统设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel tsslExpired;
        private System.Windows.Forms.ToolStripMenuItem 自动重新登录toolStripMenuItem;
    }
}

