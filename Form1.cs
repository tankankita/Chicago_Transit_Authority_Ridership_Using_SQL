//// CTA Ridership analysis using C# and SQL Serer.//
// Tank Ankita 
// U. of Illinois, Chicago
// CS341, Fall2016
// Homework 6
//




using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeWork_6_atank2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filename, version, connectionInfo;
            SqlConnection db;
            SqlCommand cmd;
            Object result2;
            Object result;
            string msg;
            string sql;
            version = "MSSQLLocalDB";
            filename = "CTA.mdf";
            connectionInfo = String.Format(@"Data Source=(LocalDB)\{0};AttachDbFilename
            =|DataDirectory|\{1};Integrated Security=True;", version, filename);
            db = new SqlConnection(connectionInfo);
            db.Open();
           
            string item = listBox1.SelectedItem.ToString();
            item = item.Replace("'", "''");

            listBox2.Items.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            listBox3.Items.Clear();

            sql = string.Format(@" SELECT name FROM stops  WHERE  stationID = (SELECT stationID FROM stations WHERE name = '{0}') order by name ;", item);
            cmd = new SqlCommand();  
            cmd.Connection = db;
           
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            cmd.CommandText = sql;
            result2 = adapter.Fill(ds);

            foreach (DataRow row in ds.Tables["TABLE"].Rows)
            {
                msg = String.Format(Convert.ToString(row["Name"]));
                Console.Write(msg);
                this.listBox2.Items.Add(msg);
            }

            //** Show Sunday and Holidays **//
            sql = string.Format(@"select  SUM(DailyTotal) from Riderships where StationID = (select stationID from stations where name = '{0}') AND TypeOfDay = 'U';", item);
            cmd = new SqlCommand();
            cmd.Connection = db;
            cmd.CommandText = sql;
            result2 = cmd.ExecuteScalar();
            msg = Convert.ToString(result2);
            int sumsumday = Convert.ToInt32(msg);
            string sumsumdayForamatted = String.Format("{0:#,##0}", sumsumday);
            textBox6.Text = sumsumdayForamatted;

            //** Show Saturdays **//
            sql = string.Format(@"select  SUM(DailyTotal) from Riderships where StationID = (select stationID from stations where name = '{0}') AND TypeOfDay = 'A';", item);
            cmd = new SqlCommand();
            cmd.Connection = db;
            cmd.CommandText = sql;
            result = cmd.ExecuteScalar();
            msg = Convert.ToString(result);
            int sumsaturday = Convert.ToInt32(msg);
            string sumsaturdayyForamatted = String.Format("{0:#,##0}", sumsaturday);
            textBox5.Text = sumsaturdayyForamatted;

            //** Show Weekdays **//
            sql = string.Format(@"select  SUM(DailyTotal) from Riderships where StationID = (select stationID from stations where name = '{0}') AND TypeOfDay = 'W';", item);
            cmd = new SqlCommand();
            cmd.Connection = db;
            cmd.CommandText = sql;
            result = cmd.ExecuteScalar();
            msg = Convert.ToString(result);
            int sumweekday = Convert.ToInt32(msg);
            string sumweekdayForamatted = String.Format("{0:#,##0}", sumweekday);
            textBox4.Text = sumweekdayForamatted;

            //** Total Ridership **//
            int total = sumsumday + sumsaturday + sumweekday;
            string totalForamatted = String.Format("{0:#,##0}", total);
            textBox1.Text = totalForamatted;

            ////** Avg Riderships **//     

            sql = string.Format(@"select AVG(DailyTotal) from Riderships where StationID = (select stationID from stations where name = '{0}');", item);
            cmd = new SqlCommand();
            cmd.Connection = db;
            cmd.CommandText = sql;
            result = cmd.ExecuteScalar();
            msg = Convert.ToString(result);
            int msgInt = Convert.ToInt32(msg);
            string msgFormat = String.Format("{0:#,##0}", msgInt);

            textBox2.Text = msgFormat + "/day";

            ////** Percentage **//

            sql = string.Format(@" select sum(cast(dailytotal as bigint)) from Riderships;");
            cmd = new SqlCommand();
            cmd.Connection = db;
            cmd.CommandText = sql;
            result = cmd.ExecuteScalar();
            double dailytotal = Convert.ToDouble(result);
            double IDcount = Convert.ToDouble(total);
            double percentage = (IDcount * 100) / dailytotal;
            percentage = Math.Round(percentage, 2);
            string p = Convert.ToString(percentage);
            textBox3.Text = p + "%";
        
            db.Close();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            string filename, version, connectionInfo;
            SqlConnection db;
            SqlCommand cmd;
            Object result;
            string msg;
            string sql;
            version = "MSSQLLocalDB";
            filename = "CTA.mdf";
            connectionInfo = String.Format(@"Data Source=(LocalDB)\{0};AttachDbFilename
            =|DataDirectory|\{1};Integrated Security=True;", version, filename);

          
            db = new SqlConnection(connectionInfo);
            db.Open();
            sql = @"Select name from stations order by name;";
            cmd = new SqlCommand();
            cmd.Connection = db;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            cmd.CommandText = sql;
            result = adapter.Fill(ds);

            foreach (DataRow row in ds.Tables["TABLE"].Rows)
            {    
                msg = String.Format(Convert.ToString(row["Name"]));
                Console.Write(msg);
                this.listBox1.Items.Add(msg);
            }

            db.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string item = listBox2.SelectedItem.ToString();
            item = item.Replace("'", "''");

            string filename, version, connectionInfo;
            SqlConnection db;
            SqlCommand cmd;
            Object result;
            string msg;
            string sql;
            version = "MSSQLLocalDB";
            filename = "CTA.mdf";
            connectionInfo = String.Format(@"Data Source=(LocalDB)\{0};AttachDbFilename
            =|DataDirectory|\{1};Integrated Security=True;", version, filename);
            db = new SqlConnection(connectionInfo);
            db.Open();
            textBox7.Clear();
            textBox8.Clear();
            listBox3.Items.Clear();

            //** Check the Direction **//

            sql = string.Format(@"Select direction from stops where name ='{0}';",item);
            cmd = new SqlCommand();
            cmd.Connection = db;
            cmd.CommandText = sql;
            result = cmd.ExecuteScalar();
            msg = Convert.ToString(result);
            this.textBox8.Text = msg;

            //** Handicap Accessible **//

            sql= string.Format(@"Select ADA from stops where name ='{0}';", item);
            cmd = new SqlCommand();
            cmd.Connection = db;
            cmd.CommandText = sql;
            result = cmd.ExecuteScalar();
            msg = Convert.ToString(result);
            if (msg.Equals("True"))
                this.textBox7.Text = "Yes";
            else
                this.textBox7.Text = "No";

            //** Location **//

            sql= string.Format(@"select latitude,longitude from stops where name ='{0}';", item);
            cmd = new SqlCommand();
            cmd.Connection = db;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            cmd.CommandText = sql;
            result= adapter.Fill(ds);

            foreach(DataRow row in ds.Tables["TABLE"].Rows) 

             {
                 msg = string.Format("({0}  ,  {1})",Math.Round( Convert.ToDouble   (row["latitude"]),4)  , Math.Round( Convert.ToDouble(row["longitude"]),4) );
                textBox9.Text = msg;
             }

            //** Colors of the line **//

            sql = string.Format(@"select color from lines where lineid = any(select lineid from StopDetails where StopID = (select stopid from stops where name ='{0}'));", item);
            cmd = new SqlCommand();
            cmd.Connection = db;
            adapter = new SqlDataAdapter(cmd);
            ds = new DataSet();
            cmd.CommandText = sql;
            result = adapter.Fill(ds);

            foreach (DataRow row in ds.Tables["TABLE"].Rows)
            {
                msg = String.Format(Convert.ToString(row["Color"]));
                Console.Write(msg);
                this.listBox3.Items.Add(msg);
            }

            ////**Start Getting RiderShip details from Tble Rider **// 
    
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string filename, version, connectionInfo;
            SqlConnection db;
            SqlCommand cmd;
            Object result;
            string msg;
            string sql;
            version = "MSSQLLocalDB";
            filename = "CTA.mdf";
            connectionInfo = String.Format(@"Data Source=(LocalDB)\{0};AttachDbFilename
            =|DataDirectory|\{1};Integrated Security=True;", version, filename);

            db = new SqlConnection(connectionInfo);
            db.Open();

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();

            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();

            sql = string.Format(@" select st.name as name,daily_total from stations st,(select top 10 StationID,sum(DailyTotal) as daily_total from Riderships group by StationID order by sum(dailytotal)DESC) as temp where st.stationid = temp.StationID;");
            cmd = new SqlCommand();
            cmd.Connection = db;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            ds = new DataSet();
            cmd.CommandText = sql;
            result = adapter.Fill(ds);

            foreach (DataRow row in ds.Tables["TABLE"].Rows)
            {
                msg = String.Format("{0} ->  {1}",   Convert.ToString(row["Name"]), Convert.ToString(row["daily_total"]));
                this.listBox4.Items.Add(msg);
            }
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
