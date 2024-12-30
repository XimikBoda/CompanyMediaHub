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

namespace VideoSharingSystem
{
	public partial class AddCompany : Form
	{
		public class AddCompanyResult
		{
			public string message { get; set; }
			public int id { get; set; }
		}

		Form1 mainForm;
		public AddCompany(Form1 mainForm)
		{
			this.mainForm = mainForm;
			InitializeComponent();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Close();
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
				Image image = Image.FromFile(openFileDialog.FileName);
				logoPictureBox.Image = resizeImage(image, new Size(240, 240));
			}
		}

		private void saveButton_Click(object sender, EventArgs e)
		{
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = mainForm.bearer_token;

				var companyInfo = new { name = nameTextBox.Text, about = descriptionRichTextBox.Text };
				string json = JsonSerializer.Serialize(companyInfo);
				string url = $"{mainForm.url_host}/company";

				try
				{
					HttpContent imageStreamContent = null;
					HttpContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
					MemoryStream imageStream = null;

					if (logoPictureBox.Image != null)
					{
						imageStream = new MemoryStream();
						logoPictureBox.Image.Save(imageStream, ImageFormat.Jpeg);
						imageStreamContent = new ByteArrayContent(imageStream.ToArray());
					}

					using (var formData = new MultipartFormDataContent())
					{
						formData.Add(stringContent);
						if (logoPictureBox.Image != null)
							formData.Add(imageStreamContent, "logo", "logo.jpg");

						var response = client.PostAsync(url, formData).Result;

						if (response.IsSuccessStatusCode)
						{
							string responseBody = response.Content.ReadAsStringAsync().Result;

							var result = JsonSerializer.Deserialize<AddCompanyResult>(responseBody);

							mainForm.InitCompanyViewer(result.id);
							new EditCompany(mainForm, result.id).ShowDialog(this);

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
