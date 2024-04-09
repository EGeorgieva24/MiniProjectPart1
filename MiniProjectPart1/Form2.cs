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
    public partial class Form2 : Form
    {
        private bool isAdmin;
        public Form2(bool isAdmin)
        {
            InitializeComponent();
            PopulateCountriesComboBox();
            PopulateTownsComboBox();
            if (!isAdmin)
            {
                adminButton.Enabled = false;
                adminButton.Visible = false;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void PopulateCountriesComboBox()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=LAB108PC13\SQLEXPRESS;Initial Catalog=TourismNew;Integrated Security=True"))
                {
                    string query = "SELECT CountryName FROM Country";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            comboBoxCountries.Items.Add(reader["CountryName"].ToString());
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving countries from the database: " + ex.Message);
            }
        }

        private void PopulateTownsComboBox()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=LAB108PC13\SQLEXPRESS;Initial Catalog=TourismNew;Integrated Security=True"))
                {
                    string query = "SELECT TownName FROM Towns";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            comboBoxTowns.Items.Add(reader["TownName"].ToString());
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving towns from the database: " + ex.Message);
            }
        }

        private void adminButton_Click(object sender, EventArgs e)
        {

            Form1 loginForm = new Form1();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                // Open Form1 when login is successful
                Form1 form1 = new Form1();
                form1.Show();

                // Enable admin button if the user is an admin

            }
        }
    }
}
