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
    public partial class Form1 : Form
    {
        Conn Conn = new Conn();
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        
       

        private void tblcat()
        {

            string selectQuerry = "SELECT * FROM category";
            SqlCommand command = new SqlCommand(selectQuerry, Conn.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView2.DataSource = table;
        }

        public void getcatid()
        {
            string catid;
            string query = "SELECT catid FROM category ORDER BY catid Desc;";
            Conn.OpenCon();
            SqlCommand command = new SqlCommand(query, Conn.GetCon());
            SqlDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
                int id = int.Parse(dr[0].ToString()) + 1;
                catid = id.ToString("00");

            }
            else if (Convert.IsDBNull(dr))
            {
                catid = ("00");
            }
            else
            {
                catid = ("01");
            }

            textBox6.Text = catid.ToString();
            Conn.CloseCon();

        }

        /// </summary>
        /// 
        ////////////////
        public void getpid()
        {
            string pid;
            string query = "SELECT pid FROM tproduct ORDER BY pid Desc;";
            Conn.OpenCon();
            SqlCommand command = new SqlCommand(query, Conn.GetCon());
            SqlDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
                int id = int.Parse(dr[0].ToString()) + 1;
                pid = id.ToString("000");

            }
            else if (Convert.IsDBNull(dr))
            {
                pid = ("000");
            }
            else
            {
                pid = ("100");
            }

            textBox1.Text = pid.ToString();
            Conn.CloseCon();

        }

        private void getCategory()
        {
            string selectQuerry = "SELECT * FROM category";
            SqlCommand command = new SqlCommand(selectQuerry, Conn.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            comboBox1.DataSource = table;
            comboBox1.ValueMember = "categoryname";

        }

        private void tblproduct()
        {
            string selectQuerry = "SELECT * FROM tproduct";
            SqlCommand command = new SqlCommand(selectQuerry, Conn.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }



        //////////////

        private void tblprofit()
        {
            string selectQuerry = "SELECT tproduct.pname,tproduct.bprice,sales.price, sales.pqty,sales.total, sales.pname FROM sales INNER JOIN tproduct ON tproduct.pname=sales.pname;";
            SqlCommand command = new SqlCommand(selectQuerry, Conn.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView3.DataSource = table;
        }




        private void textBox7_KeyDown_1(object sender, KeyEventArgs e)
        {

        }

        private void panel3_DoubleClick(object sender, EventArgs e)
        {
            textBox7.Focus();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           // button12.Enabled = false;
           //////category//////
            tblcat();
            getcatid();
            //////category///////

            /////product///////
            getpid();
            getCategory();
            tblproduct();

            //////product ////
            tblprofit();
            label10.Text = login.username;
            label10.Text = login.username;

        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                button7.PerformClick();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

            string insertQuery = "INSERT INTO category VALUES(" + textBox6.Text + ",'" + textBox7.Text + "')";
            SqlCommand command = new SqlCommand(insertQuery, Conn.GetCon());
            Conn.OpenCon();
            command.ExecuteNonQuery();
            MessageBox.Show("item Added Successfully");
            Conn.CloseCon();
            tblcat();
            getcatid();
            textBox7.Text = "";
            textBox7.Focus();
        }

        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                textBox6.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                textBox7.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                textBox7.Focus();
            }
 
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            textBox6.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
            textBox7.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
            textBox7.Focus();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox6.Text == "" || textBox7.Text == "")
                {
                    MessageBox.Show("Missing Information", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                    string updateQuery = "UPDATE Category SET categoryname='" + textBox7.Text + "' WHERE catid=" + textBox6.Text + " ";
                    SqlCommand command = new SqlCommand(updateQuery, Conn.GetCon());
                    Conn.OpenCon();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Category Updated Successfully", "Update Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Conn.CloseCon();
                   tblcat();
                    textBox7.Text = "";
                  

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox7.Text == "")
                {
                    MessageBox.Show("Missing Information", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string deleteQuery = "DELETE FROM category WHERE catid=" + textBox6.Text + "";
                    SqlCommand command = new SqlCommand(deleteQuery, Conn.GetCon());
                    Conn.OpenCon();
                    command.ExecuteNonQuery();
                    MessageBox.Show("item Deleted Successfully", "Delete Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Conn.CloseCon();
                    tblcat();
                    textBox7.Text = "";
                    getcatid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void panel2_DoubleClick(object sender, EventArgs e)
        {
            textBox2.Focus();

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                textBox3.Focus();
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                textBox4.Focus();
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                textBox5.Focus();
            }
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                comboBox1.Focus();
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.End)
            {
                button1.PerformClick();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                string insertQuery = "INSERT INTO tproduct  VALUES (" + textBox1.Text + ", '" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + comboBox1.Text + "')";
                SqlCommand command = new SqlCommand(insertQuery, Conn.GetCon());
                Conn.OpenCon();
                command.ExecuteNonQuery();
                MessageBox.Show("item Added Successfully", "Add Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                tblproduct();
                getpid();

                textBox2.Focus();
                clear();

                Conn.CloseCon();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || comboBox1.Text == "")
                {
                    MessageBox.Show("Missing Information", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                    string updateQuery = "UPDATE tproduct SET pname='" + textBox2.Text + "', pqty='" + textBox3.Text + "', bprice='" + textBox4.Text + "' ,  sprice='" + textBox5.Text + "' ,category='" + comboBox1.Text + "'  WHERE PId=" + textBox1.Text + " ";
                    SqlCommand command = new SqlCommand(updateQuery, Conn.GetCon());
                    Conn.OpenCon();
                    command.ExecuteNonQuery();
                    tblproduct();
                    MessageBox.Show("Category Updated Successfully");
                    Conn.CloseCon();
                    clear();



                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            comboBox1.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Right)
            {

                textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                textBox5.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                comboBox1.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            }

           
        }
        public    void clear()
        {
          //  textBox2.Text = "";
            //textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
           // comboBox1.Text = "";
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Missing Information", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string deleteQuery = "DELETE FROM tproduct WHERE PId=" + textBox1.Text + "";
                    SqlCommand command = new SqlCommand(deleteQuery, Conn.GetCon());
                    Conn.OpenCon();
                    command.ExecuteNonQuery();
                    MessageBox.Show("item Deleted Successfully");
                    Conn.CloseCon();
                    tblproduct();
                   clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            POS pos = new POS();
            pos.Show();
            this.Hide();
        }

        private void button12_Click(object sender, EventArgs e)
        {
           
            reports r = new reports();
            r.Show();
            this.Hide();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string message = "Do you want to close the session ,you will be automatically log out";
            string title = "log out ";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                login logh = new login();
                logh.Show();
                this.Close();
            }
            else
            {
                this.Refresh();
            }
        }
    }
}
