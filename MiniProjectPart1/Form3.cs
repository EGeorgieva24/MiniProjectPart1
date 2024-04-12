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

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;
            string pattern = "^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[?!@#$%_&*])[A-Za-z0-9?!@#$%_&*]{8,}$";

            // Check if password meets complexity requirements
            if (!Regex.IsMatch(password, pattern))
            {
                MessageBox.Show("Invalid password. Password should be at least 8 characters long with at least one capital letter, one lowercase letter, one digit, and one special character.");
                return;
            }

            // Generate salt
            byte[] saltBytes = GenerateSalt();
            string salt = Convert.ToBase64String(saltBytes);

            // Hash the password
            string hashedPassword = HashPassword(password, salt);

            // Check if the username and password match in the database
            if (CheckCredentials(username, hashedPassword))
            {
                MessageBox.Show("Login successful!");
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }

        private byte[] GenerateSalt()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[32];
                rng.GetBytes(salt);
                return salt;
            }
        }

        private string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] saltedPassword = Encoding.UTF8.GetBytes(password + salt);
                byte[] hashedPasswordBytes = sha256.ComputeHash(saltedPassword);
                return Convert.ToBase64String(hashedPasswordBytes);
            }
        }

        private bool CheckCredentials(string username, string hashedPassword)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Tourism;Integrated Security=True"))
            {
                string query = "SELECT Password, Salt FROM User_Profiles WHERE Username = @Username";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedPassword = reader.GetString(0);
                            string storedSalt = reader.GetString(1);
                            string hashedPasswordToCheck = HashPassword(passwordTextBox.Text, storedSalt);
                            return hashedPasswordToCheck == storedPassword;
                        }
                        return false; // Username not found
                    }
                }
            }
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
