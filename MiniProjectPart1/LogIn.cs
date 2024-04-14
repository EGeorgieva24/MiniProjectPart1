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
    public partial class LogIn : Form
    {
        public bool IsAdmin { get; private set; }
        public LogIn()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;
            string pattern = "^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[?!@#$%_&*])[A-Za-z0-9?!@#$%_&*]{8,}$";

            // Check if username is empty or null
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Please enter a username.");
                return;
            }

            // Check if password meets complexity requirements
            if (!Regex.IsMatch(password, pattern))
            {
                MessageBox.Show("Invalid password. Password should be at least 8 characters long with at least one capital letter, one lowercase letter, one digit, and one special character.");
                return;
            }

            

            // Hash the password
            //"M6qjPlXpr0F5Y7YPoTM+tUqiv4N0kGXmWNCZderYhwQ="
            

            // Check if the username and password match in the database
            if (CheckCredentials(username, password))
            {
                MessageBox.Show("Login successful!");

                // Open Form4 if login is successful
                Form4 form4 = new Form4(username);
                form4.Show();
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
        private bool CheckCredentials(string username, string enteredPassword)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Tourism;Integrated Security=True"))
            {
                string query = "SELECT Password, DateAndTime FROM Users WHERE Username = @Username";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedHashedPassword = reader.GetString(0);

                            // Extract salt from the stored hashed password
                            string storedSalt = reader.GetString(1);

                            // Hash the entered password with the same salt
                            string enteredHashedPassword = hashPassword($"{enteredPassword}{storedSalt}");

                            // Compare the hashed passwords
                            return enteredHashedPassword == storedHashedPassword;
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

                // Check if the username already exists
                string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@Username", username);
                    int existingUserCount = (int)checkCommand.ExecuteScalar();

                    if (existingUserCount > 0)
                    {
                        MessageBox.Show("This username is already taken. Please choose a different username.");
                        return;
                    }
                }

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

                        // Automatically create a user profile with default values
                        int userId = GetUserId(username);
                        if (userId != -1)
                        {
                            CreateDefaultUserProfile(userId);
                        }
                        else
                        {
                            MessageBox.Show("Failed to create user profile.");
                        }

                        ClearTextBoxes(); // Optional: Clear textboxes after successful registration
                    }
                    else
                    {
                        MessageBox.Show("Failed to register user.");
                    }
                }
                 int GetUserId(string username)
                {
                    int userId = -1; // Initialize userId to a default value

                    // Establish connection string to your SQL Server database
                    string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Tourism;Integrated Security=True";

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        string query = "SELECT Id FROM Users WHERE Username = @Username";
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
            }
        }

        // Helper method to create a default user profile for a given userId
        private void CreateDefaultUserProfile(int userId)
        {
            // Establish connection string to your SQL Server database
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Tourism;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Prepare SQL query to insert data into the User_Profiles table
                string query = "INSERT INTO User_Profiles (UserId, FirstName, LastName, Country, Gender) VALUES (@UserId, NULL, NULL, NULL, NULL)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters
                    command.Parameters.AddWithValue("@UserId", userId);

                    // Execute the query
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Default user profile created successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Failed to create default user profile.");
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
        public string GetUsername()
        {
            return usernameTextBox.Text;
        }

    }
}
