using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniProjectPart1
{
    public partial class Form3 : Form
    {
        public bool IsAdmin { get; private set; }
        public Form3()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {

            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;

            if (username.Equals("admin") && password.Equals("admin"))
            {
                IsAdmin = true;
            }
            else
            {
                IsAdmin = false;
            }

            DialogResult = DialogResult.OK;
            Form2 steve = new Form2(IsAdmin);
            steve.Show();

        }



        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;
            string confirmPassword = passwordAgainTextBox.Text;
            string pattern = "^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[?!@#$%_&*])[A-Za-z0-9?!@#$%_&*]{8,}$";

            // Check if passwords match
            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match!");
                return;
            }

            // Check if password meets complexity requirements
            if (!Regex.IsMatch(password, pattern))
            {
                MessageBox.Show("Invalid password. Password should be at least 8 characters long with at least one capital letter, one lowercase letter, one digit, and one special character.");
                return;
            }

            // Generate salt (in this case, using DateTime)
            string salt = DateTime.Now.ToString();

            // Hash the password
            string hashedPassword = hashPassword($"{password}{salt}");

            // Establish connection string to your SQL Server database
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Tourism;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Prepare SQL query to insert data into the Users table
                string query = "INSERT INTO Users (Username, DateAndTime, Password) VALUES (@Username, @DateAndTime, @Password)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to prevent SQL injection
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@DateAndTime", salt); // Store salt
                    command.Parameters.AddWithValue("@Password", hashedPassword); // Store hashed password

                    // Execute the query
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("User registered successfully.");
                        ClearTextBoxes(); // Optional: Clear textboxes after successful registration
                    }
                    else
                    {
                        MessageBox.Show("Failed to register user.");
                    }
                }
            }
        }
        private void ClearTextBoxes()
        {
            usernameTextBox.Text = "";
            passwordTextBox.Text = "";
            passwordAgainTextBox.Text = "";
        }
    


        string hashPassword(string password)
        {
            SHA256 hashAlgorithm = SHA256.Create();
            var bytes = Encoding.Default.GetBytes(password);
            var hash = hashAlgorithm.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private void usernameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
