using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace XSKS
{
    public partial class Examine : Form
    {
        int flag1 = 0;
        int flag2 = 0;
        int flag3 = 0;
        int flag4 = 0;
        int flag5 = 0;
        int flag6 = 0;
        int flag7 = 0;
        int flag8 = 0;
        int flag9 = 0;
        int flag10 = 0;
        string id;
        public Examine(string _id)
        {
            InitializeComponent();
        }

        TimeSpan ts = new TimeSpan(0, 10, 0);
        private void Examine_Load(object sender, EventArgs e)
        {
            label5.Text = "";
            label6.Text = "";
            label7.Text = "";
            label8.Text = "";
            label9.Text = "";

            this.timer1.Interval = 1000; //设置间隔时间，为毫秒；
            //this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            String str = ts.Minutes.ToString() + " : " + ts.Seconds.ToString();
            label2.Text = str;
            this.timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            String str =ts.Minutes.ToString() + " : " + ts.Seconds.ToString();

            label2.Text = str;

            ts = ts.Subtract(new TimeSpan(0, 0, 1));

            if (ts.TotalSeconds < 0.0)
            {

                timer1.Enabled = false;
                MessageBox.Show("考试时间到，系统将强行交卷");

            }
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

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult reslut = MessageBox.Show("是否选择退出？", "提示", MessageBoxButtons.YesNo);
            if (reslut == DialogResult.Yes)
            {
                Form1 fm1 = new Form1();
                fm1.Show();
                this.Close();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (flag1 == 0)
                checkBox1.Checked = false;
            else
                checkBox1.Checked = true;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (flag2 == 0)
                checkBox2.Checked = false;
            else
                checkBox2.Checked = true;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (flag3 == 0)
                checkBox3.Checked = false;
            else
                checkBox3.Checked = true;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (flag4 == 0)
                checkBox4.Checked = false;
            else
                checkBox4.Checked = true;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (flag5 == 0)
                checkBox5.Checked = false;
            else
                checkBox5.Checked = true;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (flag6 == 0)
                checkBox6.Checked = false;
            else
                checkBox6.Checked = true;
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (flag7 == 0)
                checkBox7.Checked = false;
            else
                checkBox7.Checked = true;
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (flag8 == 0)
                checkBox8.Checked = false;
            else
                checkBox8.Checked = true;
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (flag9 == 0)
                checkBox9.Checked = false;
            else
                checkBox9.Checked = true;
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (flag10 == 0)
                checkBox10.Checked = false;
            else
                checkBox10.Checked = true;
        }
    }
}
