﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.Design;

namespace VideoSharingSystem
{
	public class profile
	{
		public string authorized { get; set; }
		public string user { get; set; }
	}

	public class LoginResult
	{
		public string message { get; set; }
		public string token { get; set; }
		public int user_id { get; set; }
		public bool is_admin { get; set; }
		public bool is_comp_owner { get; set; }
		public bool is_mod{ get; set; }
	}

	public class GeneralResult
	{
		public string message { get; set; }
	}

	public partial class LoginForm : Form
	{
		public string url_host = "http://25.18.114.207:8080";
		public LoginForm()
		{
			InitializeComponent();
		}

		void UpdateLoginButton()
		{
			if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0)
				button1.Enabled = true;
			else
				button1.Enabled = false;
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			UpdateLoginButton();
		}

		private void textBox2_TextChanged(object sender, EventArgs e)
		{
			UpdateLoginButton();
		}

		void logout(string token) {
			using (HttpClient client = new HttpClient())
			{
				var bearer_token = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
				client.DefaultRequestHeaders.Authorization = bearer_token;

				string url = $"{url_host}/profile/logout";

				try
				{
					HttpResponseMessage response = client.PostAsync(url, null).Result;

					if (response.IsSuccessStatusCode)
					{
						string responseBody = response.Content.ReadAsStringAsync().Result;

						var result = JsonSerializer.Deserialize<GeneralResult>(responseBody);
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

		void login()
		{
			using (HttpClient client = new HttpClient())
			{
				string loginUrl = $"{url_host}/profile/login";
				var loginData = new { username = textBox1.Text, password = textBox2.Text };
				string json = JsonSerializer.Serialize(loginData);
				var content = new StringContent(json, Encoding.UTF8, "application/json");


				try
				{
					HttpResponseMessage response = client.PostAsync(loginUrl, content).Result;

					if (response.IsSuccessStatusCode)
					{
						string responseBody = response.Content.ReadAsStringAsync().Result;

						var loginResult = JsonSerializer.Deserialize<LoginResult>(responseBody);

						if (loginResult != null || loginResult.message == "success")
						{
							Visible = false;
							var mainFrorm = new Form1(loginResult.token, url_host, loginResult.user_id, 
								loginResult.is_admin, loginResult.is_comp_owner, loginResult.is_mod);
							mainFrorm.ShowDialog();
							mainFrorm.Dispose();

							logout(loginResult.token);

							Visible = true;
						}
						else
						{
							MessageBox.Show("Невірний логін або пароль: " + loginResult.message);
						}

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

					//Visible = false;
					//var mainFrorm = new Form1("loginResult.token", url_host, -1);
					//mainFrorm.ShowDialog();
					//mainFrorm.Dispose();
					//Visible = true;
				}
			}
		}

		private void LoginTextBoxEnter(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				login();
				e.Handled = true;
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			login();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Visible = false;
			new RegistrationForm(url_host).ShowDialog();
			Visible = true;
		}
	}
}
