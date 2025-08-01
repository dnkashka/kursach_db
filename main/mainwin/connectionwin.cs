using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mainwin
{
    public partial class connectionwin : Form
    {
        public string ConnectionString {  get; private set; }

        private const string PlaceholderHost = "Введите название хоста";
        private const string PlaceholderPort = "Введите порт";
        private const string PlaceholderLogin = "Введите логин";
        private const string PlaceholderPassword = "Введите пароль";

        public connectionwin()
        {
            InitializeComponent();
        }

        private void loginBox_Enter(object sender, EventArgs e) //пользователь нажал на loginBox (элемент ввода логина)
        {
            if (loginBox.Text == PlaceholderLogin)
            {
                loginBox.Text = "";
                loginBox.ForeColor = Color.Black;
            }
        }

        private void loginBox_Leave(object sender, EventArgs e) //пользователь нажал на другой элемент
        {
            if (loginBox.Text == "")
            {
                loginBox.Text = PlaceholderLogin;
                loginBox.ForeColor = Color.Gray;
            }
        }

        private void passwordBox_Enter(object sender, EventArgs e) //пользователь нажал на passwordBox (элемент ввода пароля)
        {
            if (passwordBox.Text == PlaceholderPassword)
            {
                passwordBox.Text = "";
                passwordBox.UseSystemPasswordChar = true;
                passwordBox.ForeColor = Color.Black;
            }
        }

        private void passwordBox_Leave(object sender, EventArgs e) //пользователь нажал на другой элемент
        {
            if (passwordBox.Text == "")
            {
                passwordBox.UseSystemPasswordChar = false;
                passwordBox.Text = PlaceholderPassword;
                passwordBox.ForeColor = Color.Gray;
            }
        }

        private void portBox_Enter(object sender, EventArgs e) //пользователь нажал на portBox (элемент ввода порта)
        {
            if (portBox.Text == PlaceholderPort)
            {
                portBox.Text = "";
                portBox.ForeColor = Color.Black;
            }
        }

        private void portBox_Leave(object sender, EventArgs e) //пользователь нажал на другой элемент
        {
            if (portBox.Text == "")
            {
                portBox.Text = PlaceholderPort;
                portBox.ForeColor = Color.Gray;
            }
        }

        private void hostBox_Enter(object sender, EventArgs e) //пользователь нажал на hostBox (элемент ввода название хоста)
        {
            if (hostBox.Text == PlaceholderHost)
            {
                hostBox.Text = "";
                hostBox.ForeColor = Color.Black;
            }
        }

        private void hostBox_Leave(object sender, EventArgs e) //пользователь нажал на другой элемент
        {
            if (hostBox.Text == "")
            {
                hostBox.Text = PlaceholderHost;
                hostBox.ForeColor = Color.Gray;
            }
        }

        private async void connectionButton_Click(object sender, EventArgs e)
        {
            if (hostBox.Text == PlaceholderHost ||
                portBox.Text == PlaceholderPort ||
                loginBox.Text == PlaceholderLogin ||
                passwordBox.Text == PlaceholderPassword)
            {
                MessageBox.Show("Пожалуйста, заполните обязательные поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string host = hostBox.Text;
            string portText = portBox.Text;
            string login = loginBox.Text;
            string password = passwordBox.Text;

            if (!int.TryParse(portText, out int port))
            {
                MessageBox.Show("Порт должен быть числом!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var csb = new NpgsqlConnectionStringBuilder
            {
                Host = host,
                Port = port,
                Username = login,
                Password = password
            };

            string connString = csb.ToString();

            try
            {
                using (var conn = new NpgsqlConnection(connString))
                {
                    await conn.OpenAsync();
                    MessageBox.Show("Соединение установлено!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ConnectionString = connString;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ошибка: {ex.Message}\n",
                    "Ошибка подключения",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
