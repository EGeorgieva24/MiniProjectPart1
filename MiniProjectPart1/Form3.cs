using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            if (username.Contains("*") && password.Contains("*"))
            {
                IsAdmin = true;
                DialogResult = DialogResult.OK;
                Form2 steve = new Form2(true);
                steve.Show();
            }
            else
            {
                IsAdmin = false;
                DialogResult = DialogResult.OK;
                Form2 steve = new Form2(false);
                steve.Show();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            string password = passwordTextBox.Text;
            string confirmPassword = passwordAgainTextBox.Text; // Getting the value from passwordAgainTextBox
            string Pattern = "^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[?!@#$%_&*])[A-Za-z0-9?!@#$%_&*]{8,}$";

            if (password != confirmPassword) // Checking if passwords match
            {
                MessageBox.Show("Passwords do not match!");
                return; // Exit the method if passwords do not match
            }

            if (Regex.IsMatch(password, Pattern))
            {
                MessageBox.Show("Valid");
                {
                    var salt = "";
                    salt = DateTime.Now.ToString();
                    hashPassword($"{password}{salt}");
                    MessageBox.Show(hashPassword(password));
                }
            }
            else
            {
                MessageBox.Show("Invalid, password should be 8 chars long with at least one capital, one lowercase letter and one special char");
            }
        }

        string hashPassword(string password)
        {
            SHA256 hashAlgorithm = SHA256.Create();
            var bytes = Encoding.Default.GetBytes(password);
            var hash = hashAlgorithm.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
