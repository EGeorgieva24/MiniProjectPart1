using Microsoft.VisualBasic.ApplicationServices;
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
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace MiniProjectPart1
{
    public partial class Form4 : Form
    {
        public string username;

        public Form4(string username)
        {
            InitializeComponent();

            this.username = username;
            LoadProfilePhoto(username);

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            int userID = GetUserId(username); // Get the userId
            if (userID != -1) // Check if userId is valid
            {
                // Assuming you have controls for FirstName, LastName, Country, Gender, and PictureBox on your form
                string firstName = firstNameTextBox.Text;
                string lastName = lastnameTextBox.Text;
                string country = countryTextBox.Text;
                string gender = GetSelectedGender();

                // Get the bytes of the selected image
                byte[] photoBytes = savePhoto();

                // Save the profile to the database, including the image
                SaveProfileToDatabase(userID, firstName, lastName, country, gender, photoBytes);
            }
            else
            {
                MessageBox.Show("User not found.");
            }
        }
        private string GetSelectedGender()
        {
            if (maleButton.Checked)
            {
                return "Male";
            }
            else if (femaleRadio.Checked)
            {
                return "Female";
            }
            else
            {
                return " ";
            }
        }

        private int GetUserId(string username)
        {
            int userId = -1; // Initialize userId to a default value

            // Establish connection string to your SQL Server database
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Tourism;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT u.Id FROM Users u WHERE u.Username = @Username";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Username", username);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    userId = Convert.ToInt32(result);
                }
            }

            return userId;
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

        private void SaveProfileToDatabase(int userId, string firstName, string lastName, string country, string gender, byte[] photoBytes)
        {
            // Establish connection string to your SQL Server database
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Tourism;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Check if userId exists in User_Profiles table
                string checkUserIdQuery = "SELECT COUNT(*) FROM User_Profiles WHERE UserId = @UserId";
                SqlCommand checkUserIdCmd = new SqlCommand(checkUserIdQuery, con);
                checkUserIdCmd.Parameters.AddWithValue("@UserId", userId);

                int userCount = (int)checkUserIdCmd.ExecuteScalar();

                if (userCount == 0)
                {
                    MessageBox.Show($"User with UserId {userId} does not exist in User_Profiles table.");
                    return;
                }

                // If userId exists, update the profile
                string query = @"UPDATE User_Profiles 
                 SET FirstName = @FirstName,
                     LastName = @LastName,
                     Country = @Country,
                     Gender = @Gender,
                     Picture = @Photo
                 WHERE UserId = @UserId";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@Country", country);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@UserId", userId);

                // Add the photo parameter
                SqlParameter photoParam = new SqlParameter("@Photo", SqlDbType.VarBinary, -1);
                photoParam.Value = photoBytes;
                cmd.Parameters.Add(photoParam);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Profile saved successfully.");
                }
                else
                {
                    MessageBox.Show("Failed to save profile.");
                }
            }
        

    }
        private void LoadProfilePhoto(string username)
        {
            // Establish connection string to your SQL Server database
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Tourism;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT Picture FROM User_Profiles p JOIN Users u ON p.UserId = u.Id WHERE u.username = @Username";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Username", username);
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
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Tourism;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                // Select the picture corresponding to the provided username
                SqlCommand cmd = new SqlCommand("SELECT Picture FROM User_Profiles p JOIN Users u ON p.UserId = u.Id WHERE u.username = @Username", con);
                cmd.Parameters.AddWithValue("@Username", username);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Check if the 'Picture' column is not null
                    if (!reader.IsDBNull(reader.GetOrdinal("Picture")))
                    {
                        byte[] photoBytes = (byte[])reader["Picture"];
                        pictureBox1.Image = GetImage(photoBytes);
                    }
                    else
                    {
                        MessageBox.Show("No photo found for the given username.");
                    }
                }
                else
                {
                    MessageBox.Show("No photo found for the given username.");
                }

                reader.Close();
            }
        }
            private Image GetImage(byte[] imageData)
            {
                if (imageData == null || imageData.Length == 0)
                {
                    return null;
                }

                using (MemoryStream memoryStream = new MemoryStream(imageData))
                {
                    Image image = Image.FromStream(memoryStream);
                    return image;
                }
            }
        }
    }

