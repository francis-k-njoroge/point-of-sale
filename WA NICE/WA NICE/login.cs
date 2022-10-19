using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace WA_NICE
{
    public partial class login : Form
    {
        public static string username;
        Conn Conn = new Conn();
        
        public login()
        {
            InitializeComponent();
            comboBox1.Focus();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            comboBox1.Text = "";
            textBox5.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("Please Enter Username and Password");
                textBox1.Focus();
            }
            else
            {
                if (comboBox1.SelectedIndex > -1)
                {
                    if (comboBox1.SelectedItem.ToString() == "ADMIN")
                    {
                        if (textBox1.Text == "Admin" && textBox5.Text == "Admin123")
                        {
                            Form1 inv = new Form1();
                            inv.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("If you are Admin, Please Enter the corret Id and Password", "Miss Id", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        string selectQuery = "SELECT * FROM tbllogin WHERE fname='" + textBox1.Text + "' AND password='" + textBox5.Text + "'";

                        SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, Conn.GetCon());
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        if (table.Rows.Count > 0)
                        {
                            username = textBox1.Text;

                            POS sellform = new POS();
                            sellform.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Wrong Username or Password", "Wrong Information", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }

                    }
                }
                else
                {
                    MessageBox.Show("Please Select Role", "Wrong Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void login_Load(object sender, EventArgs e)
        {
            
            comboBox1.Focus();

        }

        private void login_DoubleClick(object sender, EventArgs e)
        {
            comboBox1.Focus();
        }
    }
    }

