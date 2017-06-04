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
    public partial class Main : Form
    {
        string admin_name;
        Form1 fm1 = new Form1();
        SqlConnection Mycon;
        public Main(string name)
        {
            InitializeComponent();
            this.ControlBox = false;
            this.admin_name = name;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult reslut = MessageBox.Show("是否选择退出？", "提示", MessageBoxButtons.YesNo);
            if (reslut == DialogResult.Yes)
            {
                fm1.Show();
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

        private void Main_Load(object sender, EventArgs e)
        {
            label1.Text = "管理员" + admin_name + "你好！";

            int i;
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            string connstr = ConfigurationManager.ConnectionStrings["SQLConnString"].ConnectionString;
            Mycon = new SqlConnection(connstr);
            Mycon.Open();

            SqlCommand search = new SqlCommand("select * from Test", Mycon);
            da = new SqlDataAdapter(search);
            da.Fill(ds, "Test");

            for (i = 0; i < ds.Tables["Test"].Rows.Count; i++)
            {
                comboBox2.Items.Add(ds.Tables["Test"].Rows[i]["test_num"].ToString());
            }

            SqlCommand search_time = new SqlCommand("select * from Time where flag = '1'", Mycon);
            da = new SqlDataAdapter(search_time);
            da.Fill(ds, "Time");

            i = int.Parse(ds.Tables["Time"].Rows[0]["time"].ToString());

            textBox2.Text = i.ToString();
            textBox3.Text = i.ToString();
            SqlCommand save = new SqlCommand("update Time set time = '" + textBox2.Text + "'where flag = '0'", Mycon);
            save.ExecuteNonQuery();

            Mycon.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i;
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            string connstr = ConfigurationManager.ConnectionStrings["SQLConnString"].ConnectionString;
            Mycon = new SqlConnection(connstr);
            Mycon.Open();

            listBox1.Items.Clear();

            if (textBox1.Text == "")
            {
                MessageBox.Show("考生姓名不能为空！");
            }
            else
            {
                SqlCommand search_Stu = new SqlCommand("select * from Stu where name = '" + textBox1.Text + "'", Mycon);
                da = new SqlDataAdapter(search_Stu);
                da.Fill(ds, "Stu");
                if(ds.Tables["Stu"].Rows.Count <= 0)
                {
                    MessageBox.Show("不存在该考生!");
                    textBox1.Text = "";
                }
                else
                {
                    SqlCommand search;
                    if (radioButton1.Checked)
                    {
                        search = new SqlCommand("select * from Score where id = '" + ds.Tables["Stu"].Rows[0]["id"] + "'", Mycon);
                        da = new SqlDataAdapter(search);
                        da.Fill(ds, "Score");
                        for (i = 0; i < ds.Tables["Score"].Rows.Count; ++i)
                        {
                            listBox1.Items.Add("成绩： " + ds.Tables["Score"].Rows[i]["sum"].ToString() + "        时间：" + ds.Tables["Score"].Rows[i]["time"].ToString());
                        }

                    }
                    else if (radioButton2.Checked)
                    {
                        search = new SqlCommand("select * from history where stu_id = '" + ds.Tables["Stu"].Rows[0]["id"] + "' and TorF = 'F'", Mycon);
                        da = new SqlDataAdapter(search);
                        da.Fill(ds, "history");
                        for (i = 0; i < ds.Tables["history"].Rows.Count; ++i)
                        {
                            listBox1.Items.Add("错题题号： " + ds.Tables["history"].Rows[i]["test_num"].ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show("请选择要搜索的内容！");
                    }
                }
            }

            Mycon.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int i;
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            string connstr = ConfigurationManager.ConnectionStrings["SQLConnString"].ConnectionString;
            Mycon = new SqlConnection(connstr);
            Mycon.Open();


            if (comboBox2.Text == "")
            {
                MessageBox.Show("请选择题号！");
            }
            else
            {
                SqlCommand search = new SqlCommand("select * from Test where test_num = '" + comboBox2.Text + "'", Mycon);
                da = new SqlDataAdapter(search);
                da.Fill(ds, "Test");

                richTextBox1.ReadOnly = false;
                richTextBox2.ReadOnly = false;
                richTextBox3.ReadOnly = false;
                richTextBox4.ReadOnly = false;
                richTextBox5.ReadOnly = false;

                richTextBox5.Text = ds.Tables["Test"].Rows[0]["title"].ToString();
                richTextBox1.Text = ds.Tables["Test"].Rows[0]["chose_A"].ToString();
                richTextBox2.Text = ds.Tables["Test"].Rows[0]["chose_B"].ToString();
                richTextBox3.Text = ds.Tables["Test"].Rows[0]["chose_C"].ToString();
                richTextBox4.Text = ds.Tables["Test"].Rows[0]["chose_D"].ToString();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            string connstr = ConfigurationManager.ConnectionStrings["SQLConnString"].ConnectionString;
            Mycon = new SqlConnection(connstr);
            Mycon.Open();

            if (comboBox2.Text == "")
            {
                MessageBox.Show("请先选择题号！");
            }
            else
            {
                DialogResult reslut = MessageBox.Show("是否确定该修改？", "提示", MessageBoxButtons.YesNo);
                if (reslut == DialogResult.Yes)
                {
                    SqlCommand update_test = new SqlCommand("update Test set title = '" + richTextBox5.Text + "', chose_A = '" + richTextBox1.Text + "', chose_B = '" + richTextBox2.Text + "', chose_C = '" + richTextBox3.Text + "', chose_D = '" + richTextBox4.Text + "' where test_num = '" + comboBox2.Text + "' ", Mycon);
                    int i = update_test.ExecuteNonQuery();
                    if (i != 0)
                    {
                        MessageBox.Show("修改成功！");
                    }
                    else
                    {
                        MessageBox.Show("修改失败！");
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            string connstr = ConfigurationManager.ConnectionStrings["SQLConnString"].ConnectionString;
            Mycon = new SqlConnection(connstr);
            Mycon.Open();

            int i;
            bool reslut = Int32.TryParse(textBox3.Text, out i);
            if (reslut)
            {
                SqlCommand update_time = new SqlCommand("update Time set time = '" + textBox3.Text + "'where flag = '1'", Mycon);
                DialogResult reslut1 = MessageBox.Show("是否确定该修改？", "提示", MessageBoxButtons.YesNo);
                if (reslut1 == DialogResult.Yes)
                {
                    SqlCommand save = new SqlCommand("update Time set time = '" + textBox2.Text + "'where flag = '0'", Mycon);
                    int a = update_time.ExecuteNonQuery();
                    save.ExecuteNonQuery();
                    if (a != 0)
                    {
                        MessageBox.Show("修改成功！");
                        textBox2.Text = textBox3.Text;
                    }
                    else
                    {
                        MessageBox.Show("修改失败！");
                    }
                }
            }
            else
            {
                MessageBox.Show("请输入正确的时间数字！");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int i;
            bool result = Int32.TryParse(textBox3.Text, out i);
            if (result)
            {
                i++;
                textBox3.Text = i.ToString();
            }
            else
            {
                MessageBox.Show("请输入正确的时间数字！");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            string connstr = ConfigurationManager.ConnectionStrings["SQLConnString"].ConnectionString;
            Mycon = new SqlConnection(connstr);
            Mycon.Open();

            SqlCommand ret = new SqlCommand("select * from Time where flag = '0'", Mycon);
            da = new SqlDataAdapter(ret);
            da.Fill(ds, "Time");

            textBox3.Text = ds.Tables["Time"].Rows[0]["time"].ToString();

            Mycon.Close();
        }

    }
}
