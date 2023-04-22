using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.OleDb;
using MySql.Data.MySqlClient;


namespace zumbaAPP
{
    public partial class ClientInfo : Form
    {
        Booking Book = new Booking();
        public ClientInfo()
        {
            InitializeComponent();
        }
        #region Textboxes
        private void clientTextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void pnTextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void fnTextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void lnTextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void emailTextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void birthdayTextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void addressTextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void classesTextbox_TextChanged(object sender, EventArgs e)
        {

        }

        #endregion

        #region Buttons
        private void createButton_Click(object sender, EventArgs e)
        {
            AddMember();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            int MemberID;
            if (int.TryParse(this.MemberID.Text, out MemberID))
            {
                GetMemberInfo(MemberID);
            }
            else
            {
                MessageBox.Show("Please enter a valid Member ID.");
            }
        }

        private void saveChangesButton_Click(object sender, EventArgs e)
        {

        }

        private void bookCourseButton_Click(object sender, EventArgs e)
        {
            Booking bookingForm = new Booking();
            bookingForm.Show();
            ClientInfo Close = new ClientInfo();
            Close.Hide();

                

        }

        private void clientInfoButton_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Functions

        private void GetMemberInfo(int MemberID)
        {
            string connectionString = "server=sql.c-pass.xyz;port=3306;database=zumbadb;uid=zomba;password=FFGGtygh;"; //updated connection string

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM Customer WHERE UID = @UID";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UID", MemberID);

                    try
                    {
                        connection.Open();
                        MySqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            this.MemberID.Text = reader["UID"].ToString();
                            pnTextbox.Text = reader["Contact"].ToString();
                            fnTextbox.Text = reader["Name"].ToString();
                            lnTextbox.Text = reader["Last"].ToString();
                            emailTextbox.Text = reader["Email"].ToString();
                            birthdayTextbox.Text = reader["Birthday"].ToString();
                            addressTextbox.Text = reader["Address"].ToString();
                            classesTextbox.Text = reader["Concession"].ToString();
                            // Set the values of other textboxes based on the column names in your database
                        }
                        else
                        {
                            MessageBox.Show("No Member found with the given ID.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }


        private void AddMember()
        {
            string connectionString = "server=sql.c-pass.xyz;port=3306;database=zumbaDB;uid=zomba;pwd=FFGGtygh;"; //updated connection string

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "INSERT INTO [zumba connessions] (MemberID, PhoneNumber, FirstName, LastName, Email, Birthday, Address, RemainingClass) VALUES (@MemberID, @PhoneNumber, @FirstName, @LastName, @Email, @Birthday, @Address, @RemainingClass)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MemberID", MemberID.Text);
                    command.Parameters.AddWithValue("@PhoneNumber", pnTextbox.Text);
                    command.Parameters.AddWithValue("@FirstName", fnTextbox.Text);
                    command.Parameters.AddWithValue("@LastName", lnTextbox.Text);
                    command.Parameters.AddWithValue("@Email", emailTextbox.Text);
                    command.Parameters.AddWithValue("@Birthday", DateTime.Parse(birthdayTextbox.Text));
                    command.Parameters.AddWithValue("@Address", addressTextbox.Text);
                    command.Parameters.AddWithValue("@RemainingClass", int.Parse(classesTextbox.Text));

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Member added successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Failed to add the Member.");
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

        #region Test Database
        private void button1_Click(object sender, EventArgs e)
        {

            String ConnStr = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source =
                              C:\Users\willy\Downloads\FarmInfomation (2).accdb;
                              Persist Security Info = False"; //connection string
            String query = "";
            OleDbConnection conn = null;
            try
            {
                conn = new OleDbConnection(ConnStr);
                conn.Open(); //opens database connection
                query = MemberID.Text.ToLower(); //converts querys to lowercase
                OleDbCommand cmd = new OleDbCommand(query, conn);
                String str = "";//initialises string
                if (query.Contains("sheep")) //checks query for keyword in database
                {
                    str = "ID\tAmount of water\tDaily cost\tWeight\tAge\tColor\tAmount of Wool\n"; //Names of Tables
                }
                else if (query.Contains("[commodity prices]"))//checks query for keyword in database
                {
                    str = "Commodity\tPrice\n\t";
                }
                else
                {
                    str = "ID\tAmount of water\tDaily cost\tWeight\tAge\tColor\tAmount of Milk\n";
                }

                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())//reads data from database
                    {
                        if (query.Contains("sheep"))//checks query for keyword in database
                        {
                            str += "\t" + reader["ID"].ToString() + "\t\t" +//reads contents of tables
                            reader["Amount of water"].ToString() + "\t" +
                            reader["Daily cost"].ToString() + "\t" +
                            reader["Weight"].ToString() + "\t" +
                            reader["Age"].ToString() + "\t" +
                            reader["color"].ToString() + "\t\t" +
                            reader["Amount of Wool"].ToString() + "\n";

                        }
                        else if (query.Contains("[commodity prices]"))
                        {
                            str += "\t" + reader["Commodity"].ToString() + "\t" +
                            reader["Price"].ToString() + "\n";
                        }
                        else
                        {
                            str += "\t" + reader["ID"].ToString() + "\t\t" +
                            reader["Amount of water"].ToString() + "\t" +
                            reader["Daily cost"].ToString() + "\t" +
                            reader["Weight"].ToString() + "\t" +
                            reader["Age"].ToString() + "\t" +
                            reader["color"].ToString() + "\t\t" +
                            reader["Amount of Milk"].ToString() + "\n";
                        }
                    }
                    addressTextbox.Text = str;//display query output

                }
                conn.Close();//closes database connection
            }
            catch (Exception ex)//execption handling
            {
                MessageBox.Show(ex.Message);
            }
        }


        #endregion

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }

}
