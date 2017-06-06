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
        int[] flag = { 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0};
        string id;
        SqlConnection Mycon;
        int[] test = new int[10];
        int[] chose = new int[10];
        int now_num = 0;
        TimeSpan ts;
        int h, m;
        DataSet ds = new DataSet();
        public Examine(string _id)
        {
            InitializeComponent();
            this.id = _id;
        }
        private void Examine_Load(object sender, EventArgs e)
        {
            SqlDataAdapter da;
            string connstr = ConfigurationManager.ConnectionStrings["SQLConnString"].ConnectionString;
            Mycon = new SqlConnection(connstr);
            Mycon.Open();
            int test_num;
            int x = 0, y;


            //定时器
            SqlCommand time = new SqlCommand("select * from Time where flag = '1'", Mycon);
            da = new SqlDataAdapter(time);
            da.Fill(ds, "Time");
            h = Int32.Parse(ds.Tables["Time"].Rows[0]["time"].ToString()) / 60;
            m = Int32.Parse(ds.Tables["Time"].Rows[0]["time"].ToString()) - h * 60;
            ts = new TimeSpan(h, m, 0);
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

            //载入试卷
            SqlCommand search = new SqlCommand("select * from Test", Mycon);
            da = new SqlDataAdapter(search);
            da.Fill(ds, "Test");
            test_num = ds.Tables["Test"].Rows.Count;

            while (x < 10)
            {
                Random a = new Random();
                test[x] = a.Next(test_num);
                for (y = 0; y < x; y++)
                {
                    if (test[y] == test[x])
                    {
                        test[x] = a.Next(test_num);
                        y = 0;
                    }
                }
                x++;
            }

            for (y = 0; y < 10; y++)
            {
                chose[y] = 0;
            }

            label1.Text = "题目" + (now_num+1).ToString() + "：";
            label5.Text = ds.Tables["Test"].Rows[test[now_num]]["title"].ToString();
            label6.Text = ds.Tables["Test"].Rows[test[now_num]]["chose_A"].ToString();
            label7.Text = ds.Tables["Test"].Rows[test[now_num]]["chose_B"].ToString();
            label8.Text = ds.Tables["Test"].Rows[test[now_num]]["chose_C"].ToString();
            label9.Text = ds.Tables["Test"].Rows[test[now_num]]["chose_D"].ToString();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            String str =(ts.Hours*60+ts.Minutes).ToString() + " : " + ts.Seconds.ToString();

            label2.Text = str;

            ts = ts.Subtract(new TimeSpan(0, 0, 1));

            if (ts.TotalSeconds < 0.0)
            {

                timer1.Enabled = false;
                MessageBox.Show("考试时间到，系统将强行交卷");
                string time = DateTime.Now.ToString();
                jiaojuan(time);
                this.Close();
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
            if (flag[0] == 0)
                checkBox1.Checked = false;
            else
                checkBox1.Checked = true;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (flag[1] == 0)
                checkBox2.Checked = false;
            else
                checkBox2.Checked = true;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (flag[2] == 0)
                checkBox3.Checked = false;
            else
                checkBox3.Checked = true;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (flag[3] == 0)
                checkBox4.Checked = false;
            else
                checkBox4.Checked = true;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (flag[4] == 0)
                checkBox5.Checked = false;
            else
                checkBox5.Checked = true;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (flag[5] == 0)
                checkBox6.Checked = false;
            else
                checkBox6.Checked = true;
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (flag[6] == 0)
                checkBox7.Checked = false;
            else
                checkBox7.Checked = true;
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (flag[7] == 0)
                checkBox8.Checked = false;
            else
                checkBox8.Checked = true;
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (flag[8] == 0)
                checkBox9.Checked = false;
            else
                checkBox9.Checked = true;
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (flag[9] == 0)
                checkBox10.Checked = false;
            else
                checkBox10.Checked = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            chose[now_num] = 1;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            chose[now_num] = 2;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            chose[now_num] = 3;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            chose[now_num] = 4;
        }

        private void label6_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
        }

        private void label7_Click(object sender, EventArgs e)
        {
            radioButton2.Checked = true;
        }

        private void label8_Click(object sender, EventArgs e)
        {
            radioButton3.Checked = true;
        }

        private void label9_Click(object sender, EventArgs e)
        {
            radioButton4.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (now_num == 0)
            {
                MessageBox.Show("这是第一题！");
            }
            else
            {
                if (chose[now_num] != 0)
                {
                    flag[now_num] = 1;
                    checkBox1_CheckedChanged(sender, e);
                    checkBox2_CheckedChanged(sender, e);
                    checkBox3_CheckedChanged(sender, e);
                    checkBox4_CheckedChanged(sender, e);
                    checkBox5_CheckedChanged(sender, e);
                    checkBox6_CheckedChanged(sender, e);
                    checkBox7_CheckedChanged(sender, e);
                    checkBox8_CheckedChanged(sender, e);
                    checkBox9_CheckedChanged(sender, e);
                    checkBox10_CheckedChanged(sender, e);
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    radioButton3.Checked = false;
                    radioButton4.Checked = false;
                }
                now_num--;
                if (chose[now_num] == 0)
                {
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    radioButton3.Checked = false;
                    radioButton4.Checked = false;
                }
                else if (chose[now_num] == 1)
                {
                    radioButton1.Checked = true;
                    radioButton2.Checked = false;
                    radioButton3.Checked = false;
                    radioButton4.Checked = false;
                }
                else if (chose[now_num] == 2)
                {
                    radioButton1.Checked = false;
                    radioButton2.Checked = true;
                    radioButton3.Checked = false;
                    radioButton4.Checked = false;
                }
                else if (chose[now_num] == 3)
                {
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    radioButton3.Checked = true;
                    radioButton4.Checked = false;
                }
                else if (chose[now_num] == 4)
                {
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    radioButton3.Checked = false;
                    radioButton4.Checked = true;
                }
                label1.Text = "题目" + (now_num + 1).ToString() + "：";
                label5.Text = ds.Tables["Test"].Rows[test[now_num]]["title"].ToString();
                label6.Text = ds.Tables["Test"].Rows[test[now_num]]["chose_A"].ToString();
                label7.Text = ds.Tables["Test"].Rows[test[now_num]]["chose_B"].ToString();
                label8.Text = ds.Tables["Test"].Rows[test[now_num]]["chose_C"].ToString();
                label9.Text = ds.Tables["Test"].Rows[test[now_num]]["chose_D"].ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (now_num == 9)
            {
                MessageBox.Show("这是最后一题！");
            }
            else
            {
                if (chose[now_num] != 0)
                {
                    flag[now_num] = 1;
                    checkBox1_CheckedChanged(sender, e);
                    checkBox2_CheckedChanged(sender, e);
                    checkBox3_CheckedChanged(sender, e);
                    checkBox4_CheckedChanged(sender, e);
                    checkBox5_CheckedChanged(sender, e);
                    checkBox6_CheckedChanged(sender, e);
                    checkBox7_CheckedChanged(sender, e);
                    checkBox8_CheckedChanged(sender, e);
                    checkBox9_CheckedChanged(sender, e);
                    checkBox10_CheckedChanged(sender, e);
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    radioButton3.Checked = false;
                    radioButton4.Checked = false;
                }
                now_num++;
                if (chose[now_num] == 0)
                {
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    radioButton3.Checked = false;
                    radioButton4.Checked = false;
                }
                else if (chose[now_num] == 1)
                {
                    radioButton1.Checked = true;
                    radioButton2.Checked = false;
                    radioButton3.Checked = false;
                    radioButton4.Checked = false;
                }
                else if (chose[now_num] == 2)
                {
                    radioButton1.Checked = false;
                    radioButton2.Checked = true;
                    radioButton3.Checked = false;
                    radioButton4.Checked = false;
                }
                else if (chose[now_num] == 3)
                {
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    radioButton3.Checked = true;
                    radioButton4.Checked = false;
                }
                else if (chose[now_num] == 4)
                {
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    radioButton3.Checked = false;
                    radioButton4.Checked = true;
                }
                label1.Text = "题目" + (now_num + 1).ToString() + "：";
                label5.Text = ds.Tables["Test"].Rows[test[now_num]]["title"].ToString();
                label6.Text = ds.Tables["Test"].Rows[test[now_num]]["chose_A"].ToString();
                label7.Text = ds.Tables["Test"].Rows[test[now_num]]["chose_B"].ToString();
                label8.Text = ds.Tables["Test"].Rows[test[now_num]]["chose_C"].ToString();
                label9.Text = ds.Tables["Test"].Rows[test[now_num]]["chose_D"].ToString();
            }
        }

        private void jiaojuan(string time)
        {
            SqlDataAdapter da;
            string connstr = ConfigurationManager.ConnectionStrings["SQLConnString"].ConnectionString;
            Mycon = new SqlConnection(connstr);
            Mycon.Open();
            SqlCommand insert;
            int score = 100;
            int x;

            for (x = 0; x < 10; x++)
            {
                if (chose[x] == 0)
                {
                    score -= 10;
                    insert = new SqlCommand("insert into history Values('" + ds.Tables["Test"].Rows[test[x]]["test_num"].ToString() + "', 'F', '"+id+"', '1')", Mycon);
                    insert.ExecuteNonQuery();
                }
                else if (chose[x] == 1)
                {
                    if (ds.Tables["Test"].Rows[test[x]]["ans"].ToString() != "A")
                    {
                        score -= 10;
                        insert = new SqlCommand("insert into history Values('" + ds.Tables["Test"].Rows[test[x]]["test_num"].ToString() + "', 'F', '" + id + "', '1')", Mycon);
                        insert.ExecuteNonQuery();
                    }
                    else
                    {
                        insert = new SqlCommand("insert into history Values('" + ds.Tables["Test"].Rows[test[x]]["test_num"].ToString() + "', 'T', '" + id + "', '1')", Mycon);
                        insert.ExecuteNonQuery();
                    }
                }
                else if (chose[x] == 2)
                {
                    if (ds.Tables["Test"].Rows[test[x]]["ans"].ToString() != "B")
                    {
                        score -= 10;
                        insert = new SqlCommand("insert into history Values('" + ds.Tables["Test"].Rows[test[x]]["test_num"].ToString() + "', 'F', '" + id + "', '1')", Mycon);
                        insert.ExecuteNonQuery();
                    }
                    else
                    {
                        insert = new SqlCommand("insert into history Values('" + ds.Tables["Test"].Rows[test[x]]["test_num"].ToString() + "', 'T', '" + id + "', '1')", Mycon);
                        insert.ExecuteNonQuery();
                    }
                }
                else if (chose[x] == 3)
                {
                    if (ds.Tables["Test"].Rows[test[x]]["ans"].ToString() != "C")
                    {
                        score -= 10;
                        insert = new SqlCommand("insert into history Values('" + ds.Tables["Test"].Rows[test[x]]["test_num"].ToString() + "', 'F', '" + id + "', '1')", Mycon);
                        insert.ExecuteNonQuery();
                    }
                    else
                    {
                        insert = new SqlCommand("insert into history Values('" + ds.Tables["Test"].Rows[test[x]]["test_num"].ToString() + "', 'T', '" + id + "', '1')", Mycon);
                        insert.ExecuteNonQuery();
                    }
                }
                else if (chose[x] == 4)
                {
                    if (ds.Tables["Test"].Rows[test[x]]["ans"].ToString() != "D")
                    {
                        score -= 10;
                        insert = new SqlCommand("insert into history Values('" + ds.Tables["Test"].Rows[test[x]]["test_num"].ToString() + "', 'F', '" + id + "', '1')", Mycon);
                        insert.ExecuteNonQuery();
                    }
                    else
                    {
                        insert = new SqlCommand("insert into history Values('" + ds.Tables["Test"].Rows[test[x]]["test_num"].ToString() + "', 'T', '" + id + "', '1')", Mycon);
                        insert.ExecuteNonQuery();
                    }
                }
            }

            SqlCommand insert_score = new SqlCommand("insert into score Values('" + score.ToString() + "', '"+id+"', '"+time+"')", Mycon);
            insert_score.ExecuteNonQuery();

            MessageBox.Show("你的得分是：" + score.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string time = DateTime.Now.ToString();
            DialogResult reslut = MessageBox.Show("是否交卷？", "提示", MessageBoxButtons.YesNo);
            if (reslut == DialogResult.Yes)
            {
                jiaojuan(time);
                this.Close();
            }
        }
    }
}
