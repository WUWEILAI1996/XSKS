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
  
    public partial class Signup : Form
    {
        SqlConnection Mycon;
        public Signup()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult reslut = MessageBox.Show("是否选择退出？", "提示", MessageBoxButtons.YesNo);
            if (reslut == DialogResult.Yes)
            {
                Form1 fm1 = new Form1();
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            string connstr = ConfigurationManager.ConnectionStrings["SQLConnString"].ConnectionString;
            Mycon = new SqlConnection(connstr);
            Mycon.Open();

            SqlCommand search = new SqlCommand("select * from Stu where id = '"+textBox1.Text+"'", Mycon);
            da = new SqlDataAdapter(search);
            da.Fill(ds, "Stu");
            if (textBox1.Text != "")
            {
                if (ds.Tables["Stu"].Rows.Count > 0)
                {
                    label6.Text = "此用户名已经存在！";
                    label6.ForeColor = Color.Red;
                }
                else
                {
                    label6.Text = "此用户名可以使用！";
                    label6.ForeColor = Color.Green;
                }
            }
            else
                label6.Text = "";
        }

        private void Signup_Load(object sender, EventArgs e)
        {
            label6.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            string connstr = ConfigurationManager.ConnectionStrings["SQLConnString"].ConnectionString;
            Mycon = new SqlConnection(connstr);
            Mycon.Open();

            if (textBox4.Text == "")
            {
                MessageBox.Show("姓名不能为空！");
            }
            else if (textBox1.Text == "")
            {
                MessageBox.Show("用户名不能为空！");
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("密码不能为空！");
            }
            else if (textBox3.Text == "")
            {
                MessageBox.Show("请确认密码！");
            }
            else if (textBox2.Text != textBox3.Text)
            {
                MessageBox.Show("两处密码不同");
            }
            else
            {
                SqlCommand insert = new SqlCommand("INSERT INTO Stu VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox4.Text + "')", Mycon);
                int a = insert.ExecuteNonQuery();
                if (a == 0)
                {
                    MessageBox.Show("注册失败！");
                }
                else
                {
                     MessageBox.Show("注册成功！");
                     Form1 login = new Form1();
                     this.Close();
                     login.Show();
                }
            }
        }

    }
}
