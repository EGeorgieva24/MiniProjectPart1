using System.Data.SqlClient;
using System.Drawing;

namespace MiniProjectPart1
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Tourism;Integrated Security=True");

        public Form1()
        {
            InitializeComponent();
        }

        private void UpdateTownButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(townTextBox.Text) && !string.IsNullOrWhiteSpace(replacedTownTextBox.Text))
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Tourism;Integrated Security=True"))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE Towns SET TownName = @city WHERE TownName = @replacedCity", con))
                    {
                        cmd.Parameters.AddWithValue("@city", townTextBox.Text);
                        cmd.Parameters.AddWithValue("@replacedCity", replacedTownTextBox.Text);
                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Town updated successfully");
                            townTextBox.Clear();
                            replacedTownTextBox.Clear();
                        }
                        else
                        {
                            MessageBox.Show("Town not found for replacement");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter valid town names");
            }
        }

        private void AddTownButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(townTextBox.Text))
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=LAB108PC13\SQLEXPRESS;Initial Catalog=TourismNew;Integrated Security=True"))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Towns (TownName) VALUES (@city)", con))
                    {
                        cmd.Parameters.AddWithValue("@city", townTextBox.Text);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Town added successfully");
                        townTextBox.Clear();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid town name");
            }
        }

        private void DeleteTownButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(townTextBox.Text))
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=LAB108PC13\SQLEXPRESS;Initial Catalog=TourismNew;Integrated Security=True"))
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Towns WHERE TownName = @city", con))
                    {
                        cmd.Parameters.AddWithValue("@city", townTextBox.Text);
                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Town deleted successfully");
                            townTextBox.Clear();
                        }
                        else
                        {
                            MessageBox.Show("Town not found for deletion");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid town name");
            }
        }

        private void AddCountryButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(countryTextBox.Text))
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=LAB108PC13\SQLEXPRESS;Initial Catalog=TourismNew;Integrated Security=True"))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Country (CountryName) VALUES (@country)", con))
                    {
                        cmd.Parameters.AddWithValue("@country", countryTextBox.Text);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Country added successfully");
                        townTextBox.Clear();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid country name");
            }
        }

        private void UpdateCountryName_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(countryTextBox.Text) && !string.IsNullOrWhiteSpace(replaceCountryTextBox.Text))
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=LAB108PC13\SQLEXPRESS;Initial Catalog=TourismNew;Integrated Security=True"))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE Country SET CountryName = @country WHERE CountryName = @replacedCountry", con))
                    {
                        cmd.Parameters.AddWithValue("@country", countryTextBox.Text);
                        cmd.Parameters.AddWithValue("@replacedCountry", replaceCountryTextBox.Text);
                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Country updated successfully");
                            townTextBox.Clear();
                            replacedTownTextBox.Clear();
                        }
                        else
                        {
                            MessageBox.Show("Country not found for replacement");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid country name");
            }
        }

        private void DeleCountryButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(countryTextBox.Text))
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=LAB108PC13\SQLEXPRESS;Initial Catalog=TourismNew;Integrated Security=True"))
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Country WHERE CountryName = @country", con))
                    {
                        cmd.Parameters.AddWithValue("@country", countryTextBox.Text);
                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Country deleted successfully");
                            countryTextBox.Clear();
                        }
                        else
                        {
                            MessageBox.Show("Country not found for deletion");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid country name");
            }
        }
    }
}
