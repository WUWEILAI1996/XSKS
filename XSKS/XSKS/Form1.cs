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

            if (radioButton1.Checked)
            {
                sql_login = "select * from admin where usrid='" + usrid + "' and pwd='" + pwd + "'";
                cmd = new SqlCommand(sql_login, Mycon);

                search = cmd.ExecuteReader();
                try
                {
                    if (search.Read())
                    {
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("请输入正确的用户名和密码");
                    }
                }
                catch (Exception msg)
                {
                    throw new Exception(msg.ToString()); 
                }
            }
            else if (radioButton2.Checked)
            {
                sql_login = "select * from Stu where usrid='" + usrid + "' and pwd='" + pwd + "'";
                cmd = new SqlCommand(sql_login, Mycon);

                search = cmd.ExecuteReader();
                try
                {
                    if (search.Read())
                    {
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("请输入正确的用户名和密码");
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
    }
}
