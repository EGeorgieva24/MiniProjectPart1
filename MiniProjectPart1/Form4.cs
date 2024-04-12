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
                SqlCommand cmd = new SqlCommand("SELECT photo from Picture where id = @id", con);
                cmd.Parameters.AddWithValue("@id", int.Parse(index));
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataTable resultTable = new DataTable();
                adapter.Fill(resultTable);
                pictureBox1.Image = GetImage((byte[])resultTable.Rows[0][photo]);
            }
        }
    }
}
