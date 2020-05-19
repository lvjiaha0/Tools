namespace WindowsFormsApp1
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public class Form1 : Form
    {
        private bool _oKRealTime;
        private IContainer components;
        private Label label1;
        private System.Windows.Forms.Timer timer1;

        public Form1()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Form1_Closed(object sender, EventArgs e)
        {
            HotKey.UnregisterHotKey(base.Handle, 0x65);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.timer1.Interval = 0x3e8;
            this.timer1.Start();
            HotKey.RegisterHotKey(base.Handle, 0x65, HotKey.KeyModifiers.Alt, Keys.Add);
        }

        private string GetP(string response)
        {
            int index = response.IndexOf("price");
            int num2 = response.IndexOf("direction");
            char[] separator = new char[] { '.' };
            return Regex.Match(response.Substring(index, num2 - index).Split(separator)[0], @"\d+").Value;
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.label1 = new Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(30, 0x18);
            this.label1.Name = "label1&B";
            this.label1.Size = new Size(0, 13);
            this.label1.TabIndex = 0;
            this.label1.DoubleClick += new EventHandler(this.Label1_DoubleClick);
            this.timer1.Tick += new EventHandler(this.timer1_Tick);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.Gray;
            base.ClientSize = new Size(120, 0x42);
            base.Controls.Add(this.label1);
            this.ForeColor = Color.White;
            base.FormBorderStyle = FormBorderStyle.None;
            base.StartPosition = FormStartPosition.Manual;
            base.Location = new Point(Screen.PrimaryScreen.Bounds.Width - base.Width, Screen.PrimaryScreen.Bounds.Height - base.Height);
            base.Name = "Form1";
            this.Text = "Form1";
            base.TopMost = true;
            base.ShowInTaskbar = false;
            base.TransparencyKey = Color.Gray;
            base.Load += new EventHandler(this.Form1_Load);
            base.FormClosed += new FormClosedEventHandler(this.Form1_Closed);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void Label1_DoubleClick(object sender, EventArgs e)
        {
            this.OKRealTime = !this.OKRealTime;
            this.timer1_Tick(null, null);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.label1.Text = $"{DateTime.Now.AddHours(-1.0):hh:mm tt}";
            if (this.OKRealTime)
            {
                try
                {
                    string response = new WebClient().DownloadString("https://api.huobi.pro/market/trade?symbol=btcusdt");
                    string p = this.GetP(response);
                    this.label1.Text = this.label1.Text + " " + p;
                }
                catch (Exception)
                {
                    this.label1.Text += " ...";
                }
            }
            base.BringToFront();
        }

        protected override void WndProc(ref Message m)
        {
            if ((m.Msg == 0x312) && (m.WParam.ToInt32() == 0x65))
            {
                this.Label1_DoubleClick(null, null);
            }
            base.WndProc(ref m);
        }

        public bool OKRealTime
        {
            get => 
                this._oKRealTime;
            set
            {
                this._oKRealTime = !this._oKRealTime;
                this.timer1.Interval = this._oKRealTime ? 0x2710 : 0x3e8;
            }
        }
    }
}

