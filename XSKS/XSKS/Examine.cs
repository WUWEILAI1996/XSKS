using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XSKS
{
    public partial class Examine : Form
    {
        public Examine()
        {
            InitializeComponent();
        }

        int mint = 10;
        int scss = 59;
        private void Examine_Load(object sender, EventArgs e)
        {
            label2.Text = mint.ToString();
            label4.Text = scss.ToString();
            this.timer1.Interval = 1000; //设置间隔时间，为毫秒；
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            this.timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (mint >= 0)
            {
                scss--;
                if (scss == 0)
                {
                    mint--;
                    label2.Text = mint.ToString();
                    scss = 59;
                }
                label4.Text = scss.ToString();
            }
        }
        
    }
}
