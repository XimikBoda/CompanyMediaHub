using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static VideoSharingSystem.EditUser;
using static VideoSharingSystem.Form1;
using UserInfo = VideoSharingSystem.EditUser.UserInfo;

namespace VideoSharingSystem
{
	public partial class ShowUser : Form
	{
		Form1 mainForm;
		int userId;
		public ShowUser(Form1 mainForm, int userId)
		{
			this.mainForm = mainForm;
			this.userId = userId;
			InitializeComponent();

			if (mainForm.is_admin || userId == mainForm.myUserId)
				editButton.Visible = true;

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

						UserInfo userInfo = JsonSerializer.Deserialize<UserInfo>(responseBody);

						emailTextBox.Text = userInfo.email;
						loginTextBox.Text = userInfo.login;
						nameTextBox.Text = userInfo.name;
						surnameTextBox.Text = userInfo.surname;
						patronymicTextBox.Text = userInfo.patromic;
						birthDateTimePicker.Value = userInfo.birthday;
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

		private void closeButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void editButton_Click(object sender, EventArgs e)
		{
			//this.Visible = false;
			new EditUser(mainForm, userId).ShowDialog(mainForm);
			Close();
		}
	}
}
