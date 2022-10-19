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
    public partial class reports : Form
    {
        Conn Conn = new Conn();
        public reports()
        {
            InitializeComponent();
        }



        public void tblSTOCKOUT()
        {
            string selectQuerry = "select top 20 *from tproduct where pqty <='70'";
            SqlCommand command = new SqlCommand(selectQuerry, Conn.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView2.DataSource = table;
        }

        public void GETUSERID()
        {
            string uid;
            string query = "SELECT uid FROM tbllogin ORDER BY uid Desc;";
            Conn.OpenCon();
            SqlCommand command = new SqlCommand(query, Conn.GetCon());
            SqlDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
                int id = int.Parse(dr[0].ToString()) + 1;
               uid = id.ToString("0000");

            }
            else if (Convert.IsDBNull(dr))
            {
                uid = ("0000");
            }
            else
            {
                uid = ("1000");
            }

            textBox8.Text = uid.ToString();
            Conn.CloseCon();

        }

       public void tblcustomers()
        {
            string selectQuerry = "SELECT * FROM tbllogin";
            SqlCommand command = new SqlCommand(selectQuerry, Conn.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }




        private void tblprofit()
        {
            string selectQuerry = "SELECT tproduct.pname,tproduct.bprice,sales.price, sales.pqty,sales.total, sales.pname FROM sales INNER JOIN tproduct ON tproduct.pname=sales.pname;";
            SqlCommand command = new SqlCommand(selectQuerry, Conn.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView3.DataSource = table;
        }
        int grandTotal = 0, n = 0; int Total = 0;


        public void saleprofit()
        {
            for (int i = 0; i < dataGridView3.Rows.Count - 1; i++)
            {

                DataGridViewRow addRow = new DataGridViewRow();
                addRow.CreateCells(dataGridView3);

                Total = Convert.ToInt32(dataGridView3.Rows[i].Cells[2].Value) - Convert.ToInt32(dataGridView3.Rows[i].Cells[1].Value);
                n = Total * Convert.ToInt32(dataGridView3.Rows[i].Cells[3].Value);
                grandTotal += n;
                textBox2.Text = grandTotal + "  ksh ";
            }
        }

        int sTotal; int salle;
        public void totalsale()
        {

            for (int i = 0; i < dataGridView3.Rows.Count - 1; i++)
            {

                DataGridViewRow addRow = new DataGridViewRow();
                addRow.CreateCells(dataGridView3);

                sTotal = Convert.ToInt32(dataGridView3.Rows[i].Cells[4].Value);
                salle += sTotal;
                textBox1.Text = salle + " ksh ";
            }
        }
        int bgrandTotal = 0;
        int bTotal;
        public void buy()
        {
            for (int i = 0; i < dataGridView3.Rows.Count - 1; i++)
            {

                DataGridViewRow addRow = new DataGridViewRow();
                addRow.CreateCells(dataGridView3);

                bTotal = Convert.ToInt32(dataGridView3.Rows[i].Cells[1].Value) * Convert.ToInt32(dataGridView3.Rows[i].Cells[3].Value);

                bgrandTotal += bTotal;
                textBox5.Text = bgrandTotal + "  ksh ";
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            POS pos = new POS();
            pos.Show();
            this.Hide();
        }

        private void reports_Load(object sender, EventArgs e)
        {
            tblprofit();
            GETUSERID();
            label1.Text = login.username;
            tblcustomers();
            tblSTOCKOUT();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            saleprofit();
            totalsale();
            buy();

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

        private void panel4_DoubleClick(object sender, EventArgs e)
        {
            textBox7.Focus();
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                textBox6.Focus();
            }
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                textBox4.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                textBox7.Focus();
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                textBox3.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                textBox6.Focus();
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                textBox9.Focus();
                
            }
            else if(e.KeyCode == Keys.Up)
            {
                textBox4.Focus();
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                button1.Focus();
                button1.PerformClick();
            }
            else if (e.KeyCode == Keys.Up)
            {
                textBox9.Focus();
            }
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                button1.PerformClick();
            }
           
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            saleprofit();
            totalsale();
            buy();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                textBox7.Focus();

            string insertQuery = "INSERT INTO tbllogin VALUES (" + textBox8.Text + ", '" + textBox7.Text + "', '" + textBox4.Text + "', '" + textBox6.Text + "','" + comboBox1.Text + "', '" + textBox9.Text + "','" + textBox3.Text + "')";
            SqlCommand command = new SqlCommand(insertQuery, Conn.GetCon());
            Conn.OpenCon();
            command.ExecuteNonQuery();
            GETUSERID();
            tblcustomers();
            clear();


        }

        private void textBox9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                comboBox1.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                textBox3.Focus();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox8.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox7.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox6.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            textBox9.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (textBox8.Text == "")
            {
                MessageBox.Show("Missing Information", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string deleteQuery = "DELETE FROM tbllogin WHERE uid=" + textBox8.Text + "";
                SqlCommand command = new SqlCommand(deleteQuery, Conn.GetCon());
                Conn.OpenCon();
                command.ExecuteNonQuery();
                MessageBox.Show("customer Deleted Successfully", "Delete Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Conn.CloseCon();
                GETUSERID();
                tblcustomers();
                clear();


            }
        }
        public void clear()
        {
            textBox7.Clear();
            textBox4.Clear();
            textBox6.Clear();
            textBox9.Clear();
            textBox3.Clear();
            
        }
        private void button11_Click(object sender, EventArgs e)
        {

            Form1 inv = new Form1();
            inv.Show();
            this.Hide();
        }
    }
}
