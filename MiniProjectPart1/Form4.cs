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

namespace MiniProjectPart1
{
    public partial class Form4 : Form
    {
        private int userId;

        public Form4(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            LoadProfilePhoto(); // Load user's profile photo when the form loads
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            byte[] photoBytes = savePhoto(); // Get bytes of the photo
            if (photoBytes != null && photoBytes.Length > 0)
            {
                SavePhotoToDatabase(photoBytes); // Save the photo to the database
            }
            else
            {
                MessageBox.Show("No photo selected or invalid photo data.");
            }
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(openFileDialog.FileName);
            }
        }

        private byte[] savePhoto()
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("No photo selected.");
                return null;
            }

            using (MemoryStream stream = new MemoryStream())
            {
                pictureBox1.Image.Save(stream, pictureBox1.Image.RawFormat);
                return stream.ToArray();
            }
        }

        private void SavePhotoToDatabase(byte[] photoBytes)
        {
            if (photoBytes == null)
                return;

            // Establish connection string to your SQL Server database
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Tourism;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "UPDATE User_Profiles SET Picture = @Photo WHERE UserId = @UserId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Photo", photoBytes);
                cmd.Parameters.AddWithValue("@UserId", userId);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Photo saved successfully.");
                }
                else
                {
                    MessageBox.Show("Failed to save photo.");
                }
            }
        }

        private void LoadProfilePhoto()
        {
            // Establish connection string to your SQL Server database
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Tourism;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT Picture FROM User_Profiles WHERE UserId = @UserId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserId", userId);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        byte[] photoBytes = (byte[])reader["Picture"];
                        MemoryStream ms = new MemoryStream(photoBytes);
                        pictureBox1.Image = Image.FromStream(ms);
                    }
                }
                reader.Close();
            }
        }
        private void Form4_Load(object sender, EventArgs e)
        {
            // Establish connection string to your SQL Server database
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Tourism;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Prepare SQL query to retrieve user profile information
                string query = @"SELECT UP.Picture, UP.FirstName, UP.LastName, UP.Country, UP.Gender
                         FROM User_Profiles UP
                         JOIN Users U ON UP.UserId = U.Id
                         WHERE U.Id = @UserId";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserId", userId); // Assuming you have the userId stored in this form

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    // Load profile picture
                    if (!reader.IsDBNull(0))
                    {
                        byte[] photoBytes = (byte[])reader["Picture"];
                        MemoryStream ms = new MemoryStream(photoBytes);
                        pictureBox1.Image = Image.FromStream(ms);
                    }

                    // Load first name
                    if (!reader.IsDBNull(1))
                    {
                        firstNameTextBox.Text = reader.GetString(1);
                    }

                    // Load last name
                    if (!reader.IsDBNull(2))
                    {
                        lastnameTextBox.Text = reader.GetString(2);
                    }

                    // Load country
                    if (!reader.IsDBNull(3))
                    {
                        countryTextBox.Text = reader.GetString(3);
                    }

                    // Load gender
                    if (!reader.IsDBNull(4))
                    {
                        string gender = reader.GetString(4);
                        if (gender == "Male")
                        {
                            maleButton.Checked = true;
                        }
                        else if (gender == "Female")
                        {
                            femaleRadio.Checked = true;
                        }
                    }
                }
                reader.Close();
            }
        }
    }
}
