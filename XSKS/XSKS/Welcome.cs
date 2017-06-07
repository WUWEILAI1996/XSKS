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
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Hide();
            f1.Show();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        int t = 400, flag = 0, x1 = 0, y1 = 400;
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if (x1 != t)
            {

                if (flag == 0)
                {

                    x1 += 4;
                }
                else
                {
                    x1 -= 4;
                }
            }
            else
            {
                if (flag == 0)
                {
                    t = 0;
                    flag = 1;
                }
                else
                {
                    t = 400;
                    flag = 0;
                }
            }
            this.pictureBox1.Location = new Point(x1, y1);
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }
    }
}
