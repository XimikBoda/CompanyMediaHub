using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static VideoSharingSystem.Form1;

namespace VideoSharingSystem
{
	public partial class EditUser : Form
	{
		public class UserInfo
		{
			public int user_id { get; set; }
			public string email { get; set; }
			public string login { get; set; }
			public string name { get; set; }
			public string surname { get; set; }
			public string patromic { get; set; }
			public DateTime birthday { get; set; }
		}

		Form1 mainForm;
		int userId;
		UserInfo oldUserInfo;
		public EditUser(Form1 mainForm, int userId)
		{
			this.mainForm = mainForm;
			this.userId = userId;
			InitializeComponent();

			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = mainForm.bearer_token;

				string url = $"{mainForm.url_host}/user/{userId}";

				try
				{
					HttpResponseMessage response = client.GetAsync(url).Result;

					if (response.IsSuccessStatusCode)
					{
						string responseBody = response.Content.ReadAsStringAsync().Result;

						oldUserInfo = JsonSerializer.Deserialize<UserInfo>(responseBody);

						emailTextBox.Text = oldUserInfo.email;
						loginTextBox.Text = oldUserInfo.login;
						nameTextBox.Text = oldUserInfo.name;
						surnameTextBox.Text = oldUserInfo.surname;
						patronymicTextBox.Text = oldUserInfo.patromic;
						birthDateTimePicker.Value = oldUserInfo.birthday;

						oldPassTextBox.Text = "";
						newPassTextBox.Text = "";
					}
					else
					{
						MessageBox.Show("Ошибка при виконанні запита: " + response.StatusCode);
						Close();
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					MessageBox.Show(ex.Message);
					Close();
				}
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Close();
		}

		bool IsPassChanged() => oldPassTextBox.Text != "" || newPassTextBox.Text != "";
		bool IsInfoChanged() =>
			emailTextBox.Text != oldUserInfo.email ||
			loginTextBox.Text != oldUserInfo.login ||
			nameTextBox.Text != oldUserInfo.name ||
			surnameTextBox.Text != oldUserInfo.surname ||
			patronymicTextBox.Text != oldUserInfo.patromic ||
			birthDateTimePicker.Value != oldUserInfo.birthday;

		bool IsSomeChanged() => IsPassChanged() || IsInfoChanged();
		void updateSaveButton()
		{
			button1.Enabled = IsSomeChanged();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (oldPassTextBox.Text == "" && newPassTextBox.Text != "")
			{
				MessageBox.Show("Введіть старий пароль");
				return;
			}
			if (oldPassTextBox.Text != "" && newPassTextBox.Text == "")
			{
				MessageBox.Show("Введіть новий пароль");
				return;
			}
			if (loginTextBox.Text == "")
			{
				MessageBox.Show("Логін не має буди пустим");
				return;
			}
			if (emailTextBox.Text == "")
			{
				MessageBox.Show("Email не має буди пустим");
				return;
			}
			if (nameTextBox.Text == "")
			{
				MessageBox.Show("Ім'я не має буди пустим");
				return;
			}
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = mainForm.bearer_token;

				string url = $"{mainForm.url_host}/user/{userId}";
				var registerInfo = new
				{
					email = emailTextBox.Text,
					loginUser = loginTextBox.Text,
					nameUser = nameTextBox.Text,
					surname = surnameTextBox.Text,
					patronymic = patronymicTextBox.Text,
					birthday = birthDateTimePicker.Value,
					oldPassword = oldPassTextBox.Text,
					newPassword = newPassTextBox.Text
				};
				string json = JsonSerializer.Serialize(registerInfo);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				try
				{
					HttpResponseMessage response = client.PutAsync(url, content).Result;

					string responseBody = response.Content.ReadAsStringAsync().Result;

					var loginResult = JsonSerializer.Deserialize<GeneralResult>(responseBody);
					if (response.IsSuccessStatusCode)
					{
						Close();
					}
					else
					{
						MessageBox.Show("Ошибка при виконанні запита: " + response.StatusCode + "\n" + loginResult.message);
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					MessageBox.Show(ex.Message);
				}
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			var result = MessageBox.Show("Ви впевненні, що хочете видалити цей профіль?\nВидалиться вся інформація пов'язанна з профілем (завантаженні відео, коментарі).\nЦю дію буде неможливо відминити!",
				"Видалення профілю", MessageBoxButtons.OKCancel);

			if (result == DialogResult.OK)
			{
				using (HttpClient client = new HttpClient())
				{
					client.DefaultRequestHeaders.Authorization = mainForm.bearer_token;

					string url = $"{mainForm.url_host}/user/{userId}";

					try
					{
						HttpResponseMessage response = client.DeleteAsync(url).Result;

						if (response.IsSuccessStatusCode)
						{
							string responseBody = response.Content.ReadAsStringAsync().Result;

							MessageBox.Show("Профіль видаленно");
							Close();
							if (userId == mainForm.myUserId)
								mainForm.Close();
						}
						else
						{
							MessageBox.Show("Ошибка при виконанні запита: " + response.StatusCode);
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

		private void emailTextBox_TextChanged(object sender, EventArgs e)
		{
			updateSaveButton();
		}

		private void birthDateTimePicker_ValueChanged(object sender, EventArgs e)
		{
			updateSaveButton();
		}
	}
}
