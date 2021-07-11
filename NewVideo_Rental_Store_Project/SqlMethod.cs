using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewVideo_Rental_Store_Project
{
    public class SqlMethod
    {
        private static SqlConnection myCon = new SqlConnection("Data Source=DESKTOP-LTQK306;Initial Catalog=VideoAssignTask;Integrated Security=True");
        static SqlCommand myCmd;

        // here all functions are given that is called in main form file

        public static void OutRented(DataGridView a)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from OuttedVideo;", myCon);
            ad.SelectCommand.CommandType = CommandType.Text;
            DataTable dtblbook = new DataTable();
            ad.Fill(dtblbook);
            a.DataSource = dtblbook;
        }

        // fetching customers details from database

        public static void GetCustomer(DataGridView a)
        {
            SqlDataAdapter ad = new SqlDataAdapter("getCustomer", myCon);
            ad.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dtblbook = new DataTable();
            ad.Fill(dtblbook);
            a.DataSource = dtblbook;
        }

        // fetching video from database

        public static void GetVideo(DataGridView a)
        {
            SqlDataAdapter ad = new SqlDataAdapter("getVideo", myCon);
            ad.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dtblbook = new DataTable();
            ad.Fill(dtblbook);
            a.DataSource = dtblbook;
        }

        // get rental data

        public static void GetRental(DataGridView a)
        {
            SqlDataAdapter ad = new SqlDataAdapter("getRental", myCon);
            ad.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dtblbook = new DataTable();
            ad.Fill(dtblbook);
            a.DataSource = dtblbook;
            a.Columns["CID"].Visible = false;
            a.Columns["VID"].Visible = false;
            a.Columns["Cost"].Visible = false;
        }

        // showing videos which are on rent

        public static void GetRented(DataGridView a)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from RentedVideo;", myCon);
            ad.SelectCommand.CommandType = CommandType.Text;
            DataTable dtblbook = new DataTable();
            ad.Fill(dtblbook);
            a.DataSource = dtblbook;
        }
        public static void Delete(TextBox a, TextBox b, TextBox c, string id)
        {
            string query = "delete from Customer where cust_id=" + Convert.ToInt32(id) + "; ";
            try
            {
                myCon.Open();
                myCmd = new SqlCommand(query, myCon);
                myCmd.ExecuteReader();
                myCon.Close();
                MessageBox.Show(a.Text + " Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                a.Text = "";
                b.Text = "";
                c.Text = "";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        public static void Delete(TextBox a, TextBox b, TextBox c, TextBox d, TextBox e, DateTimePicker f,String id)
        {
            string query = "delete from Video where video_id=" + Convert.ToInt32(id) + "; ";
            try
            {
                myCon.Open();
                myCmd = new SqlCommand(query, myCon);
                myCmd.ExecuteReader();
                myCon.Close();
                MessageBox.Show(a.Text + " Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                a.Text = "";
                b.Text = "";
                d.Text = "";
                e.Text = "";
                c.Text = "";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }


        // delete booked videos

        public static void Delete(String id)
        { 
            string query = "delete from Booking where book_id=" + Convert.ToInt32(id) + "; ";
            try
            {
                myCon.Open();
                myCmd = new SqlCommand(query, myCon);
                myCmd.ExecuteReader();
                myCon.Close();
                MessageBox.Show("Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        // insert new videos to database

        public static void Add(TextBox a, TextBox b, TextBox c)
        {
            string query = "insert into Customer(cust_name,cust_phone,cust_address) values('" + a.Text + "','" + b.Text + "','" + c.Text + "');";
            try
            {
                myCon.Open();
                myCmd = new SqlCommand(query, myCon);
                myCmd.ExecuteReader();
                myCon.Close();
                MessageBox.Show(a.Text + " Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                a.Text = "";
                b.Text = "";
                c.Text = "";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                myCon.Close();
            }
        }

        // add new video copies

        public static void Add(Label a, Label b, DateTimePicker c, DateTimePicker d)
        {
            int count = 0;
            string q = "select video_copies from Video where video_id=" + Convert.ToInt32(b.Text) + "";
            SqlDataReader dr;
            try
            {
                myCon.Open();
                myCmd = new SqlCommand(q, myCon);
                dr = myCmd.ExecuteReader();
                dr.Read();
                count = dr.GetInt32(0);
                dr.Close();
                myCon.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                myCon.Close();
            }
            if (count != 0)
            {
                string query = "insert into Booking(book_cID,book_vID,book_start,book_end,book_status) values(" + Convert.ToInt32(a.Text) + "," + Convert.ToInt32(b.Text) + ",'" + c.Value.ToString("dd MMMM yy") + "','" + d.Value.ToString("dd MMMM yy") + "','Issue'); update Video set video_copies=video_copies-1 where video_id=" + Convert.ToInt32(b.Text) + "; ";
                try
                {
                    myCon.Open();
                    myCmd = new SqlCommand(query, myCon);
                    myCmd.ExecuteReader();
                    myCon.Close();
                    MessageBox.Show("Issued Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                    myCon.Close();
                }
            }
            else
            {
                MessageBox.Show("Video Copies Not Available...!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // insert new video to database

        public static void Add(TextBox a, TextBox b, TextBox c, TextBox d, TextBox e, DateTimePicker f)
        {
            string query = "insert into Video(video_title,video_gener,video_price,video_ratting,video_copies,video_year) values('" + a.Text + "','" + b.Text + "','" + Convert.ToInt32(c.Text) + "','" + d.Text + "'," + Convert.ToInt32(e.Text) + ",'" + f.Text + "');";
            try
            {
                myCon.Open();
                myCmd = new SqlCommand(query, myCon);
                myCmd.ExecuteReader();
                myCon.Close();
                MessageBox.Show(a.Text + " Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                a.Text = "";
                b.Text = "";
                c.Text = "";
                d.Text = "";
                e.Text = "";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        // update new customers to database

        public static void Update(TextBox a, TextBox b, TextBox c, string id)
        {
            string query = "update Customer set cust_name='" + a.Text + "', cust_phone='" + b.Text + "', cust_address='" + c.Text + "' where cust_id=" + Convert.ToInt32(id) + "; ";
            try
            {
                myCon.Open();
                myCmd = new SqlCommand(query, myCon);
                myCmd.ExecuteReader();
                myCon.Close();
                MessageBox.Show(a.Text + " Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                a.Text = "";
                b.Text = "";
                c.Text = "";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        //  update videos

        public static void Update(TextBox a, TextBox b, TextBox c, TextBox d, TextBox e, DateTimePicker f, String id)
        {
            string query = "update Video set video_title='" + a.Text + "', video_gener='" + b.Text + "', video_price='" + Convert.ToInt32(c.Text) + "', video_ratting='" + d.Text + "', video_copies=" + Convert.ToInt32(e.Text) + ",video_year='" + f.Text + "'  where video_id=" + Convert.ToInt32(id) + "; ";
            try
            {
                myCon.Open();
                myCmd = new SqlCommand(query, myCon);
                myCmd.ExecuteReader();
                myCon.Close();
                MessageBox.Show(a.Text + " Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                a.Text = "";
                b.Text = "";
                d.Text = "";
                e.Text = "";
                c.Text = "";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        public static void Update(Label a ,Label b, DateTimePicker c ,DateTimePicker d ,String id,int i)
        {
            string query = "update Booking set book_cID=" + Convert.ToInt32(a.Text) + ", book_vID=" + Convert.ToInt32(b.Text) + ", book_start='" + c.Value.ToString("dd MMMM yy") + "',book_end='" + d.Value.ToString("dd MMMM yy") + "',book_status='Return' where book_id=" + Convert.ToInt32(id) + "; update Video set video_copies=video_copies+1 where video_id=" + b.Text + "; ";
            try
            {
                myCon.Open();
                myCmd = new SqlCommand(query, myCon);
                myCmd.ExecuteReader();
                myCon.Close();
                MessageBox.Show("Total Rent Cost is " + i.ToString() + "$", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        public static void LoadLabel(Label a,Label b)
        {
            string query1 = "select Top 1 v.video_title FROM Booking b,Video v where b.book_vID=v.video_id group by b.book_vID,v.video_title;";
            string query2 = "select Top 1 c.cust_name FROM Booking b,Customer c where b.book_cID=c.cust_id group by b.book_cID,c.cust_name;";
            SqlDataReader dr1, dr2;
            try
            {
                myCmd = new SqlCommand(query1, myCon);
                myCon.Open();
                dr1 = myCmd.ExecuteReader();
                if (dr1.HasRows)
                {
                    dr1.Read();
                    a.Text = dr1.GetString(0);
                    dr1.Close();
                }
                myCon.Close();

                myCmd = new SqlCommand(query2, myCon);
                myCon.Open();
                dr2 = myCmd.ExecuteReader();
                if (dr2.HasRows)
                {
                    dr2.Read();
                    b.Text = dr2.GetString(0);
                    dr2.Close();
                }
                myCon.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
    }
}