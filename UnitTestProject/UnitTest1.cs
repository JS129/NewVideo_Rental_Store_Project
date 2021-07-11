using System;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        public bool CheckDate(DateTime BookingDate,DateTime ReturnDate)
        {
            return (ReturnDate > BookingDate);
        }

        [TestMethod]
        public void ConnectionTest()
        {
            SqlConnection myCon = new SqlConnection("Data Source=DESKTOP-G2UGPMF\\SQLEXPRESS;Initial Catalog=VideoAssignTask;Integrated Security=True");
            try 
            {
                myCon.Open();
                Assert.IsTrue(true);
                myCon.Close();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false,ex.Message);
            }
        }
     
        [TestMethod]
        public void BookingDateTest1()
        {
            bool a = CheckDate(new DateTime(2021, 7, 1), new DateTime(2021, 7, 5));
            Assert.IsTrue(a, "Invaid Booking Date");
        }
        
        [TestMethod]
        public void BookingDateTest2()
        {
            bool a = CheckDate(new DateTime(2021, 7, 10), new DateTime(2021, 7, 5));
            Assert.IsTrue(a, "Invaid Booking Date");
        }
    }
}
 