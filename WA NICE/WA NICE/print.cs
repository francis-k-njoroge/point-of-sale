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
using CrystalDecisions.CrystalReports.Engine;

namespace WA_NICE
{
    public partial class print : Form
    {
        Conn Conn = new Conn();
        ReportDocument crypt = new ReportDocument();
        public print()
        {
            InitializeComponent();
        }

        private void print_Load(object sender, EventArgs e)
        {

            //POS p = new POS();
           // p.Show();

            double amount;
             label1.Text = POS.lastid;
            amount = double.Parse(label1.Text)-1;
            string sqlstring2 = "SELECT * FROM sales WHERE rid = '" +amount + "' ";
            SqlDataAdapter da2 = new SqlDataAdapter(sqlstring2, Conn.GetCon());
            DataSet datareport = new DataSet();
            da2.Fill(datareport, "sales");


            string sqlstring1 = "SELECT * FROM subt WHERE rid = '" + amount + "' ";
            SqlDataAdapter da1 = new SqlDataAdapter(sqlstring1, Conn.GetCon());

            da1.Fill(datareport, "subt");

            CrystalReport1 myDatareport = new CrystalReport1();

            myDatareport.SetDataSource(datareport);

            crystalReportViewer1.ReportSource = myDatareport;

        }

        
    }
}
