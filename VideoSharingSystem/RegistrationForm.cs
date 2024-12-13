using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoSharingSystem
{
	public partial class RegistrationForm : Form
	{
		public string url_host;

		public RegistrationForm(string url_host)
		{
			this.url_host = url_host;
			InitializeComponent();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (textBox6.Text != textBox7.Text)
			{
				MessageBox.Show("Паролі повинні співпадати");
				return;
			}
			if (textBox2.Text == "")
			{
				MessageBox.Show("Логін не має буди пустим");
				return;
			}
			if (textBox6.Text == "")
			{
				MessageBox.Show("Пароль не має буди пустим");
				return;
			}
			if (textBox1.Text == "")
			{
				MessageBox.Show("Email не має буди пустим");
				return;
			}
			if (textBox3.Text == "")
			{
				MessageBox.Show("Ім'я не має буди пустим");
				return;
			}
			using (HttpClient client = new HttpClient())
			{
				string url = $"{url_host}/profile/register";
				var registerInfo = new
				{
					email = textBox1.Text,
					loginUser = textBox2.Text,
					nameUser = textBox3.Text,
					surname = textBox4.Text,
					patronymic = textBox5.Text,
					birthday = dateTimePicker1.Value,
					about = richTextBox1.Text,
					password = textBox6.Text,
					passwordAgain = textBox7.Text
				};
				string json = JsonSerializer.Serialize(registerInfo);
				var content = new StringContent(json, Encoding.UTF8, "application/json");


				try
				{
					HttpResponseMessage response = client.PostAsync(url, content).Result;

					string responseBody = response.Content.ReadAsStringAsync().Result;

					var loginResult = JsonSerializer.Deserialize<LoginResult>(responseBody);
					if (response.IsSuccessStatusCode)
					{

						if (loginResult != null || loginResult.message == "success")
						{
							MessageBox.Show("Реєстрація успішна, будь-ласка авторизуйтесь");
							Close();
						}
						else
						{
							MessageBox.Show("При реєстрації виникла помилка, можливо логін вже зайнятий\n" + loginResult.message);
						}

					}
					else
					{
						MessageBox.Show("Ошибка при виконанні запита: " + response.StatusCode +"\n"+ loginResult.message);
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					MessageBox.Show(ex.Message);
				}
			}
		}
	}
}
