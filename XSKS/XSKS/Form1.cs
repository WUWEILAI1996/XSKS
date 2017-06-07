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
    public partial class Form1 : Form
    {
        SqlConnection Mycon;
        Main admin_main;
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Signup signup = new Signup();
            this.Hide();
            signup.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string usrid = textBox1.Text;
            string pwd = textBox2.Text;

            string connstr = ConfigurationManager.ConnectionStrings["SQLConnString"].ConnectionString;
            Mycon = new SqlConnection(connstr);
            Mycon.Open();

            string sql_login;
            SqlCommand cmd;
            SqlDataReader search;
            SqlDataAdapter da;
            DataSet ds = new DataSet();

            if (textBox1.Text == "")
            {
                MessageBox.Show("请输入用户名！");
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("请输入密码！");
            }
            else
            {
                if (radioButton1.Checked)
                {
                    sql_login = "select * from admin where id ='" + usrid + "' and password='" + pwd + "'";
                    cmd = new SqlCommand(sql_login, Mycon);
                    da = new SqlDataAdapter(cmd);
                    da.Fill(ds, "admin");


                    search = cmd.ExecuteReader();
                    try
                    {
                        if (search.Read())
                        {
                            admin_main = new Main(ds.Tables["admin"].Rows[0]["name"].ToString());
                            this.Hide();
                            admin_main.Show();
                        }
                        else
                        {
                            MessageBox.Show("请输入正确的用户名和密码");
                            textBox2.Text = "";
                        }
                    }
                    catch (Exception msg)
                    {
                        throw new Exception(msg.ToString());
                    }
                }
                else if (radioButton2.Checked)
                {
                    sql_login = "select * from Stu where id='" + usrid + "' and password='" + pwd + "'";
                    cmd = new SqlCommand(sql_login, Mycon);
                    SqlDataAdapter data = new SqlDataAdapter(cmd);
                    DataSet dataset = new DataSet();
                    data.Fill(dataset, "Stu");

                    search = cmd.ExecuteReader();
                    try
                    {
                        if (search.Read())
                        {
                            this.Hide();
                            Main2 stu = new Main2(dataset.Tables["Stu"].Rows[0]["name"].ToString(), dataset.Tables["Stu"].Rows[0]["id"].ToString());
                            stu.Show();
                        }
                        else
                        {
                            MessageBox.Show("请输入正确的用户名和密码");
                            textBox2.Text = "";
                        }
                    }
                    catch (Exception msg)
                    {
                        throw new Exception(msg.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("请选择登陆方式！");
                }

                Mycon.Close();
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.button2.Height != 60)
            {
                this.button2.Height = this.button2.Height + 2;
                this.button2.Width = this.button2.Width + 2;
                this.button2.Top -= 1;
            }
            else
            {
                this.button2.Height = 50;
                this.button2.Width = 100;
                this.button2.Top = 387;
            }
        }
    }
}
