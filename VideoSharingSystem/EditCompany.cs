﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static VideoSharingSystem.Form1;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace VideoSharingSystem
{

	public partial class EditCompany : Form
	{
		public class Moderator
		{
			public int user_id { get; set; }
			public string Email { get; set; }
			public bool owner { get; set; }
			public override string ToString() => (owner ? "(Власник) " : "") + Email;
		}

		Form1 mainForm;
		int currentCompanyId;
		CompanyInfo oldCompanyInfo;
		List<Moderator> moderators;

		bool logo_changed = false;
		int selectedIdUser = -1;


		public EditCompany(Form1 mainForm, int company_id)
		{
			this.mainForm = mainForm;
			this.currentCompanyId = company_id;
			InitializeComponent();
			addModeratorButton.Enabled = false;
			deleteModeratorButton.Enabled = false;
			addOwnerButton.Visible = mainForm.is_admin;
			deleteCompanyButton.Visible = mainForm.is_admin;
			if (mainForm.is_admin)
				label3.Text = "Модератори та власники:";
			LoadInfo();
			LoadModerators();
		}

		void LoadModerators()
		{
			//if (mainForm.is_admin)
			LoadOwners();
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = mainForm.bearer_token;
				client.DefaultRequestHeaders.Add("X-idCompany", currentCompanyId.ToString());

				string url = $"{mainForm.url_host}/company/{currentCompanyId}/moderators";

				try
				{
					HttpResponseMessage response = client.GetAsync(url).Result;

					if (response.IsSuccessStatusCode)
					{
						string responseBody = response.Content.ReadAsStringAsync().Result;

						var moderators = JsonSerializer.Deserialize<List<Moderator>>(responseBody);

						//if (!mainForm.is_admin)
						//ModeratorsCheckedListBox.Items.Clear();
						foreach (var item in moderators)
						{
							item.owner = false;
							ModeratorsCheckedListBox.Items.Add(item);
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
				}
			}
		}

		void LoadOwners()
		{
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = mainForm.bearer_token;
				client.DefaultRequestHeaders.Add("X-idCompany", currentCompanyId.ToString());

				string url = $"{mainForm.url_host}/company/{currentCompanyId}/owners";

				try
				{
					HttpResponseMessage response = client.GetAsync(url).Result;

					if (response.IsSuccessStatusCode)
					{
						string responseBody = response.Content.ReadAsStringAsync().Result;

						var owners = JsonSerializer.Deserialize<List<Moderator>>(responseBody);

						ModeratorsCheckedListBox.Items.Clear();
						foreach (var item in owners)
						{
							item.owner = true;
							ModeratorsCheckedListBox.Items.Add(item);
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
				}
			}
		}

		void LoadInfo()
		{
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = mainForm.bearer_token;

				string url = $"{mainForm.url_host}/company/{currentCompanyId}";

				try
				{
					HttpResponseMessage response = client.GetAsync(url).Result;

					if (response.IsSuccessStatusCode)
					{
						string responseBody = response.Content.ReadAsStringAsync().Result;

						oldCompanyInfo = JsonSerializer.Deserialize<CompanyInfo>(responseBody);

						currentCompanyId = oldCompanyInfo.id;
						nameTextBox.Text = oldCompanyInfo.name;
						descriptionRichTextBox.Text = oldCompanyInfo.about;

						CheckChanges();
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

			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = mainForm.bearer_token;

				string url = $"{mainForm.url_host}/company/{currentCompanyId}/logo";

				try
				{
					byte[] imageBytes = client.GetByteArrayAsync(url).Result;
					using MemoryStream ms = new MemoryStream(imageBytes);
					Image image = Image.FromStream(ms);
					logoPictureBox.Image = image;
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					MessageBox.Show(ex.Message);
				}
			}
		}

		void CheckChanges()
		{
			bool changed = logo_changed || nameTextBox.Text != oldCompanyInfo.name
				|| descriptionRichTextBox.Text != oldCompanyInfo.about;

			saveButton.Enabled = changed;
		}

		private void nameTextBox_TextChanged(object sender, EventArgs e)
		{
			CheckChanges();
		}

		private void descriptionRichTextBox_TextChanged(object sender, EventArgs e)
		{
			CheckChanges();
		}

		public static Image resizeImage(Image imgToResize, Size size)
		{
			return (Image)(new Bitmap(imgToResize, size));
		}

		private void SelectImageButton_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();

			openFileDialog.Filter = "Image|*.png;*.jpg|All files (*.*)|*.*";
			openFileDialog.FilterIndex = 0;
			openFileDialog.RestoreDirectory = true;

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				logo_changed = true;
				Image image = Image.FromFile(openFileDialog.FileName);
				logoPictureBox.Image = resizeImage(image, new Size(240, 240));
				CheckChanges();
			}
		}

		private void saveButton_Click(object sender, EventArgs e)
		{
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = mainForm.bearer_token;
				client.DefaultRequestHeaders.Add("X-idCompany", currentCompanyId.ToString());

				var companyInfo = new { name = nameTextBox.Text, about = descriptionRichTextBox.Text };
				string json = JsonSerializer.Serialize(companyInfo);
				string url = $"{mainForm.url_host}/company/{currentCompanyId}";

				try
				{
					HttpContent imageStreamContent = null;
					HttpContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
					MemoryStream imageStream = null;

					if (logo_changed)
					{
						imageStream = new MemoryStream();
						logoPictureBox.Image.Save(imageStream, ImageFormat.Jpeg);
						imageStreamContent = new ByteArrayContent(imageStream.ToArray());
					}

					using (var formData = new MultipartFormDataContent())
					{
						if (nameTextBox.Text != oldCompanyInfo.name || descriptionRichTextBox.Text != oldCompanyInfo.about)
							formData.Add(stringContent);
						if (logo_changed)
							formData.Add(imageStreamContent, "logo", "logo.jpg");

						var response = client.PutAsync(url, formData).Result;

						if (response.IsSuccessStatusCode)
						{
							string responseBody = response.Content.ReadAsStringAsync().Result;

							var loginResult = JsonSerializer.Deserialize<GeneralResult>(responseBody);

							Close();
						}
						else
						{
							MessageBox.Show("Помилка при виконанні запита: " + response.StatusCode);
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					MessageBox.Show(ex.Message);
				}
			}
		}

		void Find()
		{
			if (findComboBox.Text.Length == 0)
				return;
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = mainForm.bearer_token;
				client.DefaultRequestHeaders.Add("X-idCompany", currentCompanyId.ToString());

				string url = $"{mainForm.url_host}/users/search?s={findComboBox.Text}";

				try
				{
					HttpResponseMessage response = client.GetAsync(url).Result;

					if (response.IsSuccessStatusCode)
					{
						string responseBody = response.Content.ReadAsStringAsync().Result;

						moderators = JsonSerializer.Deserialize<List<Moderator>>(responseBody);

						findComboBox.Items.Clear();
						foreach (var item in moderators)
							findComboBox.Items.Add(item);

						findComboBox.DroppedDown = true;
						findComboBox.SelectionStart = findComboBox.Text.Length;
						findComboBox.SelectionLength = 0;
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

		void findSelectedId()
		{
			if (moderators.Exists(x => x.Email == findComboBox.Text))
				selectedIdUser = moderators.Find(x => x.Email == findComboBox.Text).user_id;
			else
				selectedIdUser = -1;
		}

		private void findComboBox_TextChanged(object sender, EventArgs e)
		{
			Find();
			findSelectedId();
			addModeratorButton.Enabled = selectedIdUser != -1;
		}

		private void addModeratorButton_Click(object sender, EventArgs e)
		{
			if (selectedIdUser == -1)
				return;
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = mainForm.bearer_token;
				client.DefaultRequestHeaders.Add("X-idCompany", currentCompanyId.ToString());

				string url = $"{mainForm.url_host}/company/{currentCompanyId}/moderators";

				var idData = new { id = selectedIdUser };
				string json = JsonSerializer.Serialize(idData);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				try
				{
					HttpResponseMessage response = client.PostAsync(url, content).Result;

					if (response.IsSuccessStatusCode)
					{
						string responseBody = response.Content.ReadAsStringAsync().Result;

						LoadModerators();
					}
					else
					{
						MessageBox.Show("Ошибка при виконанні запита: " + response.StatusCode);
					}
				}
				catch (Exception ex)
				{
					//Console.WriteLine(ex.Message);
					//MessageBox.Show(ex.Message);
				}
			}
		}

		private void addOwnerButton_Click(object sender, EventArgs e)
		{
			if (selectedIdUser == -1)
				return;
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = mainForm.bearer_token;
				client.DefaultRequestHeaders.Add("X-idCompany", currentCompanyId.ToString());

				string url = $"{mainForm.url_host}/company/{currentCompanyId}/owners";

				var idData = new { id = selectedIdUser };
				string json = JsonSerializer.Serialize(idData);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				try
				{
					HttpResponseMessage response = client.PostAsync(url, content).Result;

					if (response.IsSuccessStatusCode)
					{
						string responseBody = response.Content.ReadAsStringAsync().Result;

						LoadModerators();
					}
					else
					{
						MessageBox.Show("Ошибка при виконанні запита: " + response.StatusCode);
					}
				}
				catch (Exception ex)
				{
					//Console.WriteLine(ex.Message);
					//MessageBox.Show(ex.Message);
				}
			}
		}

		private void findComboBox_SelectedValueChanged(object sender, EventArgs e)
		{
			findSelectedId();
			addModeratorButton.Enabled = selectedIdUser != -1;

		}

		private void findComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			findSelectedId();
			addModeratorButton.Enabled = selectedIdUser != -1;

		}

		private void ModeratorsCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			this.BeginInvoke((MethodInvoker)(() =>
				{
					deleteModeratorButton.Enabled = (ModeratorsCheckedListBox.CheckedItems.Count > 0);
					if(!mainForm.is_admin)
						for (int i = 0; i < ModeratorsCheckedListBox.Items.Count; i++)
						{
							bool owner = ((Moderator)ModeratorsCheckedListBox.Items[i]).owner;
							if (owner)
								ModeratorsCheckedListBox.SetItemCheckState(i, CheckState.Unchecked);
						}
				}
			));

		}

		private void deleteModeratorButton_Click(object sender, EventArgs e)
		{
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = mainForm.bearer_token;
				client.DefaultRequestHeaders.Add("X-idCompany", currentCompanyId.ToString());

				string url = $"{mainForm.url_host}/company/{currentCompanyId}/moderators";
				List<int> ids = new();
				foreach (var item in ModeratorsCheckedListBox.CheckedItems)
					if (!((Moderator)item).owner)
						ids.Add(((Moderator)item).user_id);
				string json = JsonSerializer.Serialize(ids);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				var request = new HttpRequestMessage
				{
					Method = HttpMethod.Delete,
					RequestUri = new Uri(url),
					Content = content
				};

				try
				{
					HttpResponseMessage response = client.SendAsync(request).Result;

					if (response.IsSuccessStatusCode)
					{
						string responseBody = response.Content.ReadAsStringAsync().Result;

						if (!mainForm.is_admin)
							LoadModerators();
						deleteModeratorButton.Enabled = (ModeratorsCheckedListBox.CheckedItems.Count > 0);
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

			if (mainForm.is_admin)
				using (HttpClient client = new HttpClient())
				{
					client.DefaultRequestHeaders.Authorization = mainForm.bearer_token;
					client.DefaultRequestHeaders.Add("X-idCompany", currentCompanyId.ToString());

					string url = $"{mainForm.url_host}/company/{currentCompanyId}/owners";
					List<int> ids = new();
					foreach (var item in ModeratorsCheckedListBox.CheckedItems)
						if (((Moderator)item).owner)
							ids.Add(((Moderator)item).user_id);
					string json = JsonSerializer.Serialize(ids);
					var content = new StringContent(json, Encoding.UTF8, "application/json");
					var request = new HttpRequestMessage
					{
						Method = HttpMethod.Delete,
						RequestUri = new Uri(url),
						Content = content
					};

					try
					{
						HttpResponseMessage response = client.SendAsync(request).Result;

						if (response.IsSuccessStatusCode)
						{
							string responseBody = response.Content.ReadAsStringAsync().Result;

							LoadModerators();
							deleteModeratorButton.Enabled = (ModeratorsCheckedListBox.CheckedItems.Count > 0);
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

		private void findComboBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				addModeratorButton_Click(null, null);
				e.Handled = true;
			}
		}

		private void deleteCompanyButton_Click(object sender, EventArgs e)
		{
			var result = MessageBox.Show("Ви впевненні, що хочете видалити цю компаныю?\nВидалиться вся інформація пов'язанна з профілем (завантаженні медіа, коментарі).\nЦю дію буде неможливо відминити!",
				"Видалення профілю", MessageBoxButtons.OKCancel);

			if (result == DialogResult.OK)
			{
				using (HttpClient client = new HttpClient())
				{
					client.DefaultRequestHeaders.Authorization = mainForm.bearer_token;

					string url = $"{mainForm.url_host}/company/{currentCompanyId}";

					try
					{
						HttpResponseMessage response = client.DeleteAsync(url).Result;

						if (response.IsSuccessStatusCode)
						{
							string responseBody = response.Content.ReadAsStringAsync().Result;

							MessageBox.Show("Компаныю видаленно");
							Close();
							//if (userId == mainForm.myUserId)
							//	mainForm.Close();
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

		private void button2_Click(object sender, EventArgs e)
		{
			Close();
		}
	}

}
