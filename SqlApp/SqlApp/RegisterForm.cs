// https://t.me/cobocoin

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlApp
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();

            nameField.Text = "Name";
            nameField.ForeColor = Color.Gray;

            surnameField.Text = "Surname";
            surnameField.ForeColor = Color.Gray;

            loginField.Text = "Login";
            loginField.ForeColor = Color.Gray;

            passwordField.Text = "Password";
            passwordField.ForeColor = Color.Gray;
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            // check if input fields are not empty
            if (nameField.Text== "Name")
            {
                MessageBox.Show("Enter Name!");
                return;
            }
            if (surnameField.Text == "Surname")
            {
                MessageBox.Show("Enter Surname!");
                return;
            }
            if (loginField.Text == "Login")
            {
                MessageBox.Show("Enter Login!");
                return;
            }
            if (passwordField.Text == "Password")
            {
                MessageBox.Show("Enter Password!");
                return;
            }

            // check if user exists
            if(isUserExists())
            {
                return;
            }

            Db db = new Db();
            // using @login, @password,... for security purposes
            MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`login`, `password`, `name`, `surname`) VALUES (@login, @password, @name, @surname)", db.getConnection());
        
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = loginField.Text;
            command.Parameters.Add("@password", MySqlDbType.VarChar).Value = passwordField.Text;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = nameField.Text;
            command.Parameters.Add("@surname", MySqlDbType.VarChar).Value = surnameField.Text;

            db.openConnection();

            // user registration
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("User Registered!");
            }
            else
            {
                MessageBox.Show("Could not Register :(");
            }

            db.closeConnection();
        }

        Point lastPoint;
        private void mainPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;

            }
        }

        private void mainPanel_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void closeButton_MouseEnter(object sender, EventArgs e)
        {
            closeButton.ForeColor = Color.Red;

        }

        private void closeButton_MouseLeave(object sender, EventArgs e)
        {
            closeButton.ForeColor = Color.White;

        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void nameField_Enter(object sender, EventArgs e)
        {
            if (nameField.Text == "Name")
            {
                nameField.Text = "";
                nameField.ForeColor = Color.Black;

            }

        }

        private void nameField_Leave(object sender, EventArgs e)
        {
            if (nameField.Text == "")
            {
                nameField.Text = "Name";
                nameField.ForeColor= Color.Gray;
            }
        }

        private void surnameField_Enter(object sender, EventArgs e)
        {
            if (surnameField.Text == "Surname")
            {
                surnameField.Text = "";
                surnameField.ForeColor = Color.Black;

            }
        }

        private void surnameField_Leave(object sender, EventArgs e)
        {
            if (surnameField.Text == "")
            {
                surnameField.Text = "Surname";
                surnameField.ForeColor = Color.Gray;
            }
        }

        private void loginField_Enter(object sender, EventArgs e)
        {
            if (loginField.Text == "Login")
            {
                loginField.Text = "";
                loginField.ForeColor = Color.Black;
            }
        }

        private void loginField_Leave(object sender, EventArgs e)
        {
            if (loginField.Text == "")
            {
                loginField.Text = "Login";
                loginField.ForeColor = Color.Gray;
            }
        }

        private void passwordField_Enter(object sender, EventArgs e)
        {
            if (passwordField.Text == "Password")
            {
                passwordField.Text = "";
                passwordField.ForeColor = Color.Black;
            }
        }

        private void passwordField_Leave(object sender, EventArgs e)
        {
            if (passwordField.Text == "")
            {
                passwordField.Text = "Password";
                passwordField.ForeColor = Color.Gray;
            }
        }

        // is user exists?
        public Boolean isUserExists()
        {
            Db db = new Db();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            // command to database
            // @uL, @uP - for security purposes
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uL", db.getConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginField.Text;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("This login is already taken. Choose another one!");
                return true;
            }
            else
            {
                return false;
            }
        }

        private void loginLabel_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }
    }
}
