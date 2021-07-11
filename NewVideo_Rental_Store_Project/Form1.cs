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

namespace NewVideo_Rental_Store_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            BookingDate.Value=DateTime.Now;
            ReturnDate.Value = DateTime.Now;
        }
        String data="";
        String cid = "";
        String vid = "";
        String bid = "";
        String cost = "";

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            SqlMethod.OutRented(Database);
        }

        // calling get customer function

        private void btnCustomerPanel_Click(object sender, EventArgs e)
        {
            cid = "";
            data = "Customer";
            SqlMethod.GetCustomer(Database);
        }

        // this button is calling get video function

        private void btnVideoPanel_Click(object sender, EventArgs e)
        {
            vid = "";
            data = "Video";
            SqlMethod.GetVideo(Database);
        }

        private void btnRentalPanel_Click(object sender, EventArgs e)
        {
            bid = "";
            data = "Booking";
            SqlMethod.GetRental(Database);
        }

        // the following method is calling getrented function 

        private void show_all_rented_CheckedChanged(object sender, EventArgs e)
        {
            SqlMethod.GetRented(Database);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlMethod.LoadLabel(mvideoLbl, mcustLbl);
        }

        // when we click on datagrid  

        private void Database_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Database.Columns.Count != 0 && e.RowIndex != -1 && data !="" && e.ColumnIndex!=-1)
            {
                DataGridViewRow row = Database.Rows[e.RowIndex];
                if (data=="Booking")
                {
                    bid = row.Cells["ID"].Value.ToString();
                    cost= row.Cells["Cost"].Value.ToString();
                    vidLbl.Text= row.Cells["VID"].Value.ToString();
                    cidLbl.Text= row.Cells["CID"].Value.ToString();
                    BookingDate.Text= row.Cells["Booking Date"].Value.ToString();
                    ReturnDate.Text= row.Cells["Return Date"].Value.ToString(); 
                }
                else if (data == "Customer")
                {
                    cidLbl.Text = row.Cells["ID"].Value.ToString();
                    cid = row.Cells["ID"].Value.ToString();
                    txtName.Text = row.Cells["Name"].Value.ToString();
                    txtAddress.Text = row.Cells["Address"].Value.ToString();
                    txtContact.Text = row.Cells["Phone"].Value.ToString();
                }
                else if (data == "Video")
                {
                    vid = row.Cells["ID"].Value.ToString();
                    vidLbl.Text = row.Cells["ID"].Value.ToString();
                    titleTxt.Text= row.Cells["Title"].Value.ToString();
                    costTxt.Text = row.Cells["Cost"].Value.ToString().Remove(1,2);
                    yearTxt.Value = new DateTime(Convert.ToInt32(row.Cells["Year"].Value.ToString()), 1,1);
                    copiesTxt.Text = row.Cells["Copies"].Value.ToString();
                    generTxt.Text = row.Cells["Gener"].Value.ToString();
                    rattingTxt.Text = row.Cells["Ratting"].Value.ToString();
                }
            }
        }

        // add new customer to database 

        private void btnCustomerAdd_Click(object sender, EventArgs e)
        {
            cid = "";
            if (txtName.Text != "" && txtAddress.Text != "" && txtContact.Text != "")
            {
                SqlMethod.Add(txtName,txtContact,txtAddress);
                btnCustomerPanel.PerformClick();
            }
        }

        // customer updation click 

        private void btnCustomerUpdate_Click(object sender, EventArgs e)
        {
            if (cid != "" && txtName.Text != "")
            {
                SqlMethod.Update(txtName,txtContact,txtAddress,cid);
                cid = "";
                btnCustomerPanel.PerformClick();
            }
        }

        // sql method to delete customer
        
        private void btnCustomerDelete_Click(object sender, EventArgs e)
        {
            if (cid != "" && txtName.Text != "")
            {
                SqlMethod.Delete(txtName,txtContact,txtAddress,cid);
                btnCustomerPanel.PerformClick();
                cid = "";
            }
        }

        // adding new videos to databse

        private void btnVideoADd_Click(object sender, EventArgs e)
        {
            vid = "";
            if (titleTxt.Text != "" && costTxt.Text != "" && copiesTxt.Text != "" && yearTxt.Text!="")
            {
                SqlMethod.Add(titleTxt,generTxt,costTxt,rattingTxt,copiesTxt,yearTxt);
                btnVideoPanel.PerformClick();
            }
        }

        

        private void btnVideoDelete_Click(object sender, EventArgs e)
        {
            if (vid != "" && titleTxt.Text != "")
            {
                if (titleTxt.Text != "" && costTxt.Text != "" && copiesTxt.Text != "" && yearTxt.Text != "")
                {
                    SqlMethod.Delete(titleTxt, generTxt, costTxt, rattingTxt, copiesTxt, yearTxt, vid);
                    vid = "";
                    btnVideoPanel.PerformClick();
                }
            }
        }

        // updating videos

        private void btnVideoUpdate_Click(object sender, EventArgs e)
        {
            if (vid != "")
            {
                if (titleTxt.Text != "" && costTxt.Text != "" && copiesTxt.Text != "" && yearTxt.Text != "")
                {
                    SqlMethod.Update(titleTxt, generTxt, costTxt, rattingTxt, copiesTxt, yearTxt, vid);
                    btnVideoPanel.PerformClick();
                    vid = "";
                }
            }
        }

        // delting stored videos
        private void btnVideoDel_Click(object sender, EventArgs e)
        {
            if (bid != "" && vidLbl.Text != "" && cidLbl.Text!="")
            {
                SqlMethod.Delete(bid);
                cidLbl.Text = "";
                vidLbl.Text = "";
                titleTxt.Text = "";
                generTxt.Text = "";
                copiesTxt.Text = "";
                rattingTxt.Text = "";
                costTxt.Text = "";
                txtName.Text = "";
                txtAddress.Text = "";
                txtContact.Text = "";
                bid = "";
                btnRentalPanel.PerformClick();
            }

        }

        // it event is issues new movies

        private void btnVideoIssue_Click(object sender, EventArgs e)
        {
            bid = "";
            if (vidLbl.Text != "" && cidLbl.Text!="")
            {
                SqlMethod.Add(cidLbl,vidLbl,BookingDate,ReturnDate);
                btnRentalPanel.PerformClick();
                vidLbl.Text = "";
                cidLbl.Text = "";
                titleTxt.Text = "";
                generTxt.Text = "";
                copiesTxt.Text = "";
                rattingTxt.Text = "";
                costTxt.Text = "";
                txtName.Text = "";
                txtAddress.Text = "";
                txtContact.Text = "";
            }
        }

        // returning a booked video

        private void btnVideoReturn_Click(object sender, EventArgs e)
        {

            if (vidLbl.Text != "" && cidLbl.Text != "" && bid!="")
            {
                int a = Convert.ToInt32(cost) * Convert.ToInt32((ReturnDate.Value - BookingDate.Value).TotalDays);
                if (BookingDate.Value == ReturnDate.Value)
                    a = 5;
                SqlMethod.Update(cidLbl,vidLbl,BookingDate,ReturnDate,bid,a);
                vidLbl.Text = "";
                cidLbl.Text = "";
                bid = "";
                titleTxt.Text = "";
                generTxt.Text = "";
                copiesTxt.Text = "";
                rattingTxt.Text = "";
                costTxt.Text = "";
                txtName.Text = "";
                txtAddress.Text = "";
                txtContact.Text = "";
                btnRentalPanel.PerformClick();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        // according to year cost changes

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            int a = Convert.ToInt32(DateTime.Now.Year-yearTxt.Value.Year);
            if(a>=5)
            {
                costTxt.Text = "2";
            }
            else
            {
                costTxt.Text = "5";
            }
        }

        // cost values change according to  year

        private void titleTxt_TextChanged(object sender, EventArgs e)
        {
            int a = Convert.ToInt32(DateTime.Now.Year - yearTxt.Value.Year);
            if (a >= 5)
            {
                costTxt.Text = "2";
            }
            else
            {
                costTxt.Text = "5";
            }
        }

        // checking verfication of text filed

        private void txtContact_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        // checking verfication of text filed

        private void copiesTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }
    }
}
