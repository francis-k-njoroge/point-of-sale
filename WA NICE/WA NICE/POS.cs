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
    public partial class POS : Form
    {
        public static string lastid;
        Conn Conn = new Conn();
        public POS()
        {
            InitializeComponent();
        }

        private void tblproduct()
        {

            if (textBox5.Text == "")
            {
                string selectQuerry = "SELECT PID,PNAME,PQTY,SPRICE FROM TPRODUCT ";
                SqlCommand command = new SqlCommand(selectQuerry, Conn.GetCon());
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;

            }
            else
            {
                tblpsearch();
            }
        }

        private void tblpsearch()
        {

            string selectQuerry = "SELECT PID,PNAME,PQTY,SPRICE FROM TPRODUCT WHERE pname LIKE '%" + textBox5.Text + "%'";
            SqlCommand command = new SqlCommand(selectQuerry, Conn.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();

            adapter.Fill(table);
            dataGridView1.DataSource = table;

        }


        public void sellid()
        {
            string rid;
            string query = "SELECT RID FROM SUBT ORDER BY Rid Desc;";
            Conn.OpenCon();
            SqlCommand command = new SqlCommand(query, Conn.GetCon());
            SqlDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
                int id = int.Parse(dr[0].ToString()) + 1;
                rid = id.ToString("00000");

            }
            else if (Convert.IsDBNull(dr))
            {
                rid = ("00000");
            }
            else
            {
                rid = ("10000");
            }
            
            textBox8.Text = rid.ToString();
           string lastid = rid.ToString();
            Conn.CloseCon();



        }




        private void button11_Click(object sender, EventArgs e)
        {
            Form1 inv = new Form1();
            inv.Show();
            this.Hide();

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_DoubleClick(object sender, EventArgs e)
        {
            textBox5.Focus();
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
                button1.PerformClick();
            }
        }
        int grandTotal = 0, n = 0; int Total =0;
        private void button1_Click(object sender, EventArgs e)
        {

            double qtydb = 0;
            double qtysale = 0;
            double result = 0;
            qtydb = double.Parse(textBox9.Text);
            qtysale = double.Parse(textBox3.Text);
            result = qtydb - qtysale;


            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Missing Information", "Information Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (result >= 0)
            {

                Conn.OpenCon();
                string updateQuery = "UPDATE tproduct SET pqty='" + result + "'  WHERE PId=" + textBox1.Text + " ";
                SqlCommand command = new SqlCommand(updateQuery, Conn.GetCon());
                Conn.OpenCon();
                command.ExecuteNonQuery();




                Total = Convert.ToInt32(textBox4.Text) * Convert.ToInt32(textBox3.Text);
                DataGridViewRow addRow = new DataGridViewRow();
                addRow.CreateCells(dataGridView2);
                addRow.Cells[0].Value = ++n;
                addRow.Cells[1].Value = textBox2.Text;
                addRow.Cells[2].Value = textBox4.Text;
                addRow.Cells[4].Value = Total;
                addRow.Cells[3].Value = textBox3.Text;

                dataGridView2.Rows.Add(addRow);
               grandTotal += Total;
               textBox6.Text = grandTotal + " ";
                tblproduct();
                textBox3.Text = "1";
                textBox5.Focus();
               Total =0;

            }
            else
            {
                MessageBox.Show(" out of stock or the sell excess the qountity available in the stock  ");
            }

           
        }

        private void POS_Load(object sender, EventArgs e)
        {
            tblproduct();
            sellid();
            label2.Text = DateTime.Today.ToShortDateString();
                 label18.Text = DateTime.Today.ToShortDateString();
            label1.Text = login.username;
            label20.Text = login.username;

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            tblpsearch();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {

                textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox9.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();

                textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                textBox3.Focus();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel3_DoubleClick(object sender, EventArgs e)
        {
            textBox10.Focus();
            //grandTotal += Total;
           // textBox6.Text = grandTotal + " ";
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_KeyDown(object sender, KeyEventArgs e)
        {
            

            if (e.KeyCode == Keys.End)
            {
                double amount = 0;
                double pay = 0;
                double bal = 0;
                amount = double.Parse(textBox6.Text);
                pay = double.Parse(textBox10.Text);
                bal = pay - amount;

                if (bal<0)
                {

                    MessageBox.Show("can't pay less than total in cash sell, try invoice sell");
                    textBox10.Focus();
                    textBox10.Clear();
                    
                }

                else 
                {
                    textBox7.Text = bal.ToString();
                   textBox7.Focus();

                }
            }

           



        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                string insertQuery = "INSERT INTO subt  VALUES (" + textBox8.Text + ", '" + textBox10.Text + "', '" + textBox6.Text + "', '" + textBox7.Text + "','" + label20.Text + "', '" + comboBox1.Text + "','" + label18.Text + "')";
                SqlCommand command = new SqlCommand(insertQuery, Conn.GetCon());
                Conn.OpenCon();
                command.ExecuteNonQuery();
                MessageBox.Show("good process proceed to printing");

                dataGridView2.Rows.Clear();
                dataGridView2.Refresh();
                tblproduct();
                sellid();
              
                Conn.CloseCon();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            print p = new print();
            lastid = textBox8.Text;
            
            p.Show();



        }

        public void clear()
        {
            
            textBox10.Clear();
            textBox6.Clear();
            textBox7.Clear();

        }
        public void clear1()
        {

            textBox2.Clear();
            textBox5.Clear();
            textBox1.Clear();
            textBox4.Clear();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox10.Text == "" || textBox7.Text == "" || textBox6.Text == "")
            {
                MessageBox.Show("Missing Information ");
                textBox10.Focus();
            }
            else
            {
                try
                {
                    string insertQuery = "INSERT INTO subt  VALUES (" + textBox8.Text + ", '" + textBox10.Text + "', '" + textBox6.Text + "', '" + textBox7.Text + "','" + label20.Text + "', '" + comboBox1.Text + "','" + label18.Text + "')";
                    SqlCommand command = new SqlCommand(insertQuery, Conn.GetCon());
                    Conn.OpenCon();
                    command.ExecuteNonQuery();



                    for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
                    {

                        string insertQuery1 = "insert into sales  (rid,pname,price,pqty,total) valueS ('" + textBox8.Text + "','" + dataGridView2.Rows[i].Cells[1].Value + "','" + dataGridView2.Rows[i].Cells[2].Value + "','" + dataGridView2.Rows[i].Cells[3].Value + "','" + dataGridView2.Rows[i].Cells[4].Value + "')";
                        SqlCommand command1 = new SqlCommand(insertQuery1, Conn.GetCon());
                        Conn.OpenCon();
                        command1.ExecuteNonQuery();

                    }


                    // MessageBox.Show("good process proceed to printing");

                    dataGridView2.Rows.Clear();
                    tblproduct();
                    sellid();
                    clear();
                    lastid = textBox8.Text;
                    Conn.CloseCon();


                    print p = new print();
                    p.Show();
                    //this.Close();
                    Total = 0;
                    grandTotal = 0;
                    n = 0;
                    textBox5.Focus();
                    clear1();




                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            



        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            

        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.End)
            {
                textBox10.Focus();
            }

            else if (e.KeyCode == Keys.Down)
            {
                dataGridView1.Focus();
            }
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                button2.PerformClick();
            }
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox9.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();

            textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textBox3.Focus();
        }
    }
}
