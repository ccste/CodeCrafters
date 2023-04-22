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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace zumbaAPP
{
    public partial class Booking : Form
    {
        public Booking()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        #region Functions
        public void AddMemberBooking()
        {


            string connectionString = @"C:\Users\willy\Downloads\FarmInfomation.accdb"; //connection string

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO [zumba connessions] (MemberID, FirstName, LastName, DayOfWeek, Coach, ClassType, RemainingClass) VALUES (@MemberID, @FirstName, @LastName, DayOfWeek, @Coach, @ClassType @RemainingClass)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MemberID", MemberID.Text);
                    command.Parameters.AddWithValue("@FirstName", fnTextbox.Text);
                    command.Parameters.AddWithValue("@LastName", lnTextbox.Text);
                    command.Parameters.AddWithValue("@RemainingClass", int.Parse(classesTextbox.Text));
                    command.Parameters.AddWithValue("@DayOfWeek", dayOfWeekTextbox.Text);
                    command.Parameters.AddWithValue("@Coach", coachTextbox.Text);
                    command.Parameters.AddWithValue("@ClassType", classTypeTextbox.Text);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Members Booking has been added successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Failed to add Members Booking.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }


        #endregion

        private void Booking_Load(object sender, EventArgs e)
        {

        }

        private void classesTextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
