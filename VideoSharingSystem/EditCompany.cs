using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static VideoSharingSystem.Form1;

namespace VideoSharingSystem
{

	public partial class EditCompany : Form
	{
		public class Moderator
		{
			public int user_id { get; set; }
			public string Email { get; set; }
			public override string ToString() => Email;
		}

		Form1 mainForm;
		int currentCompanyId;
		CompanyInfo oldCompanyInfo;

		bool logo_changed = false;


		public EditCompany(Form1 mainForm, int company_id)
		{
			this.mainForm = mainForm;
			this.currentCompanyId = company_id;
			InitializeComponent();
			LoadInfo();
			LoadModerators();
		}

		void LoadModerators() {
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = mainForm.bearer_token;
				client.DefaultRequestHeaders.Add("X-idCompany", currentCompanyId.ToString());

				string loginUrl = $"{mainForm.url_host}/company/{currentCompanyId}/moderators";

				try
				{
					HttpResponseMessage response = client.GetAsync(loginUrl).Result;

					if (response.IsSuccessStatusCode)
					{
						string responseBody = response.Content.ReadAsStringAsync().Result;

						var moderators = JsonSerializer.Deserialize<List<Moderator>>(responseBody);

						foreach (var item in mainForm.tags.tags)
							ModeratorsCheckedListBox.Items.Add(item);
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
						if(nameTextBox.Text != oldCompanyInfo.name || descriptionRichTextBox.Text != oldCompanyInfo.about)
							formData.Add(stringContent);
						if (logo_changed)
							formData.Add(imageStreamContent, "logo", "logo.jpg");

						var response = client.PutAsync(url, formData).Result;

						if (response.IsSuccessStatusCode)
						{
							string responseBody = response.Content.ReadAsStringAsync().Result;

							var loginResult = JsonSerializer.Deserialize<LoginResult>(responseBody);

							MessageBox.Show(loginResult.message);

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
	}

}
