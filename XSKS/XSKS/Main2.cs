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
    public partial class Main2 : Form
    {
        SqlConnection Mycon;
        string name;
        string id;
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        int wrong_test = 0;
        int flag = 1;
        public Main2(string _name, string  _id)
        {
            InitializeComponent();
            name = _name;
            id = _id;
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

        private void Main2_Load(object sender, EventArgs e)
        {
            label1.Text = "欢迎， " + name;
            label10.Text = "";
            label11.Text = "";
            label12.Text = "";
            label13.Text = "";
            label14.Text = "";
            label15.Text = "";
            label16.Text = "";

            string connstr = ConfigurationManager.ConnectionStrings["SQLConnString"].ConnectionString;
            Mycon = new SqlConnection(connstr);
            Mycon.Open();
            int i = 0;

            SqlCommand search1 = new SqlCommand("select * from history where stu_id = '" + id + "'and flag = '1' and TorF = 'F'" , Mycon);
            da = new SqlDataAdapter(search1);
            da.Fill(ds, "history");

            if (ds.Tables["history"].Rows.Count > 0)
            {
                for (i = 0; i < ds.Tables["history"].Rows.Count; i++)
                {
                    SqlCommand search2 = new SqlCommand("select * from Test where test_num = '" + ds.Tables["history"].Rows[i]["test_num"] + "'", Mycon);
                    da = new SqlDataAdapter(search2);
                    da.Fill(ds, "Test");

                }
                label10.Text = ds.Tables["Test"].Rows[wrong_test]["test_num"].ToString();
                label12.Text = ds.Tables["Test"].Rows[wrong_test]["title"].ToString();
                label13.Text = ds.Tables["Test"].Rows[wrong_test]["chose_A"].ToString();
                label14.Text = ds.Tables["Test"].Rows[wrong_test]["chose_B"].ToString();
                label15.Text = ds.Tables["Test"].Rows[wrong_test]["chose_C"].ToString();
                label16.Text = ds.Tables["Test"].Rows[wrong_test]["chose_D"].ToString();
                label11.Text = ds.Tables["Test"].Rows[wrong_test]["ans"].ToString();
            }
            else
            {
                flag = 0;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult reslut = MessageBox.Show("是否选择退出？", "提示", MessageBoxButtons.YesNo);
            if (reslut == DialogResult.Yes)
            {
                Form1 fm1 = new Form1();
                fm1.Show();
                this.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Examine exam = new Examine(id);
            this.Close();
            exam.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (wrong_test == 0 || flag == 0)
                MessageBox.Show("没有上一题了！");
            else
            {
                wrong_test--;
                label10.Text = ds.Tables["Test"].Rows[wrong_test]["test_num"].ToString();
                label12.Text = ds.Tables["Test"].Rows[wrong_test]["title"].ToString();
                label13.Text = ds.Tables["Test"].Rows[wrong_test]["chose_A"].ToString();
                label14.Text = ds.Tables["Test"].Rows[wrong_test]["chose_B"].ToString();
                label15.Text = ds.Tables["Test"].Rows[wrong_test]["chose_C"].ToString();
                label16.Text = ds.Tables["Test"].Rows[wrong_test]["chose_D"].ToString();
                label11.Text = ds.Tables["Test"].Rows[wrong_test]["ans"].ToString();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if ( flag == 0)
                MessageBox.Show("没有下一题了！");
            else if(wrong_test == ds.Tables["Test"].Rows.Count - 1)
                MessageBox.Show("没有下一题了！");
            else
            {
                wrong_test++;
                label10.Text = ds.Tables["Test"].Rows[wrong_test]["test_num"].ToString();
                label12.Text = ds.Tables["Test"].Rows[wrong_test]["title"].ToString();
                label13.Text = ds.Tables["Test"].Rows[wrong_test]["chose_A"].ToString();
                label14.Text = ds.Tables["Test"].Rows[wrong_test]["chose_B"].ToString();
                label15.Text = ds.Tables["Test"].Rows[wrong_test]["chose_C"].ToString();
                label16.Text = ds.Tables["Test"].Rows[wrong_test]["chose_D"].ToString();
                label11.Text = ds.Tables["Test"].Rows[wrong_test]["ans"].ToString();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SqlCommand update = new SqlCommand("update history set flag = '0' where test_num = '" + ds.Tables["Test"].Rows[wrong_test]["test_num"].ToString() + "'", Mycon);
            update.ExecuteNonQuery();
            ds.Clear();
            SqlCommand search1 = new SqlCommand("select * from history where stu_id = '" + id + "'and flag = '1' and TorF = 'F'", Mycon);
            da = new SqlDataAdapter(search1);
            da.Fill(ds, "history");
            for (int i = 0; i < ds.Tables["history"].Rows.Count; i++)
            {
                SqlCommand search2 = new SqlCommand("select * from Test where test_num = '" + ds.Tables["history"].Rows[i]["test_num"] + "'", Mycon);
                da = new SqlDataAdapter(search2);
                da.Fill(ds, "Test");
            }
            if (ds.Tables["history"].Rows.Count <= 0)
                MessageBox.Show("你没有错题了！");
            else
            {
                if (wrong_test == 0)
                {
                    wrong_test++;
                    label10.Text = ds.Tables["Test"].Rows[wrong_test]["test_num"].ToString();
                    label12.Text = ds.Tables["Test"].Rows[wrong_test]["title"].ToString();
                    label13.Text = ds.Tables["Test"].Rows[wrong_test]["chose_A"].ToString();
                    label14.Text = ds.Tables["Test"].Rows[wrong_test]["chose_B"].ToString();
                    label15.Text = ds.Tables["Test"].Rows[wrong_test]["chose_C"].ToString();
                    label16.Text = ds.Tables["Test"].Rows[wrong_test]["chose_D"].ToString();
                    label11.Text = ds.Tables["Test"].Rows[wrong_test]["ans"].ToString();
                }
                else
                {

                    wrong_test--;
                    label10.Text = ds.Tables["Test"].Rows[wrong_test]["test_num"].ToString();
                    label12.Text = ds.Tables["Test"].Rows[wrong_test]["title"].ToString();
                    label13.Text = ds.Tables["Test"].Rows[wrong_test]["chose_A"].ToString();
                    label14.Text = ds.Tables["Test"].Rows[wrong_test]["chose_B"].ToString();
                    label15.Text = ds.Tables["Test"].Rows[wrong_test]["chose_C"].ToString();
                    label16.Text = ds.Tables["Test"].Rows[wrong_test]["chose_D"].ToString();
                    label11.Text = ds.Tables["Test"].Rows[wrong_test]["ans"].ToString();
                }
            }
        }

    }
}
