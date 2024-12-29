using AxWMPLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static VideoSharingSystem.Form1;

namespace VideoSharingSystem
{
	public partial class EditVideo : Form
	{
		Form1 mainForm;
		int idVideo;
		bool preview_changed = false;
		VideoInfoGet oldVideoInfo;
		public EditVideo(Form1 mainForm, int idVideo)
		{
			this.mainForm = mainForm;
			this.idVideo = idVideo;

			InitializeComponent();

			foreach (var item in mainForm.tags.tags)
				TagsCheckedListBox.Items.Add(item);

			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = mainForm.bearer_token;

				string url = $"{mainForm.url_host}/video/{idVideo}/get";

				try
				{
					HttpResponseMessage response = client.GetAsync(url).Result;

					if (response.IsSuccessStatusCode)
					{
						string responseBody = response.Content.ReadAsStringAsync().Result;

						oldVideoInfo = JsonSerializer.Deserialize<VideoInfoGet>(responseBody);

						axWindowsMediaPlayer1.URL = oldVideoInfo.temporary_link;
						nameTextBox.Text = oldVideoInfo.name.Trim();
						descriptionRichTextBox.Text = oldVideoInfo.description.Trim();

						for (int i = 0; i < TagsCheckedListBox.Items.Count; i++)
						{
							int id = ((Tag)TagsCheckedListBox.Items[i]).id;
							if (oldVideoInfo.tags.Exists(x => x.id == id))
								TagsCheckedListBox.SetItemCheckState(i, CheckState.Checked);
							else
								TagsCheckedListBox.SetItemCheckState(i, CheckState.Unchecked);
						}

						axWindowsMediaPlayer1.Ctlcontrols.currentPosition = mainForm.axWindowsMediaPlayer1.Ctlcontrols.currentPosition;

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

				string url = $"{mainForm.url_host}/video/{idVideo}/preview";

				try
				{
					byte[] imageBytes = client.GetByteArrayAsync(url).Result;
					using MemoryStream ms = new MemoryStream(imageBytes);
					Image image = Image.FromStream(ms);
					pictureBox1.Image = image;
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					MessageBox.Show(ex.Message);
				}
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			axWindowsMediaPlayer1.Dispose();
			Close();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = mainForm.bearer_token;
				client.DefaultRequestHeaders.Add("X-idCompany", oldVideoInfo.company_id.ToString());

				List<int> tags = new List<int>();
				foreach (object itemChecked in TagsCheckedListBox.CheckedItems)
				{
					Tag tag = (Tag)itemChecked;
					tags.Add(tag.id);
				}

				var companyInfo = new { name = nameTextBox.Text, description = descriptionRichTextBox.Text, tags = tags };
				string json = JsonSerializer.Serialize(companyInfo);
				string url = $"{mainForm.url_host}/video/{idVideo}";

				try
				{
					HttpContent imageStreamContent = null;
					HttpContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
					MemoryStream imageStream = null;

					if (preview_changed)
					{
						imageStream = new MemoryStream();
						pictureBox1.Image.Save(imageStream, ImageFormat.Jpeg);
						imageStreamContent = new ByteArrayContent(imageStream.ToArray());
					}

					using (var formData = new MultipartFormDataContent())
					{
						if (nameTextBox.Text != oldVideoInfo.name || descriptionRichTextBox.Text != oldVideoInfo.description)
							formData.Add(stringContent);
						if (preview_changed)
							formData.Add(imageStreamContent, "preview", "preview.jpg");

						var response = client.PutAsync(url, formData).Result;

						if (response.IsSuccessStatusCode)
						{
							string responseBody = response.Content.ReadAsStringAsync().Result;

							var loginResult = JsonSerializer.Deserialize<LoginResult>(responseBody);

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

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			saveButton.Enabled = nameTextBox.Text.Length != 0;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var result = MessageBox.Show("Ви впевненні, що хочете видалити це відно?\nВидалиться вся інформація пов'язанна з ним (оцінки, коментарі).\nЦю дію буде неможливо відминити!",
				"Видалення відео", MessageBoxButtons.OKCancel);

			if (result == DialogResult.OK)
			{
				using (HttpClient client = new HttpClient())
				{
					client.DefaultRequestHeaders.Authorization = mainForm.bearer_token;
					client.DefaultRequestHeaders.Add("X-idCompany", oldVideoInfo.company_id.ToString());

					string url = $"{mainForm.url_host}/video/{idVideo}";

					try
					{
						HttpResponseMessage response = client.DeleteAsync(url).Result;

						if (response.IsSuccessStatusCode)
						{
							string responseBody = response.Content.ReadAsStringAsync().Result;

							Close();
							mainForm.DeInitVideoPlayer();
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

		private void stopFrameButton_Click(object sender, EventArgs e)
		{
			var wasPlaying = axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying;
			if (wasPlaying)
				axWindowsMediaPlayer1.Ctlcontrols.pause();

			var old_size = axWindowsMediaPlayer1.Size;
			axWindowsMediaPlayer1.Size = new Size(240, 240);

			var old_mode = axWindowsMediaPlayer1.uiMode;
			axWindowsMediaPlayer1.uiMode = "None";

			Rectangle bounds = axWindowsMediaPlayer1.Bounds;
			Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);

			using (Graphics g = Graphics.FromImage(bitmap))
			{
				g.CopyFromScreen(axWindowsMediaPlayer1.PointToScreen(Point.Empty), Point.Empty, bounds.Size);
			}

			pictureBox1.Image = bitmap;
			preview_changed = true;

			axWindowsMediaPlayer1.Ctlenabled = true;
			axWindowsMediaPlayer1.uiMode = old_mode;
			axWindowsMediaPlayer1.Size = old_size;

			if (wasPlaying)
				axWindowsMediaPlayer1.Ctlcontrols.play();
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
				pictureBox1.Image = resizeImage(image, new Size(240, 240));
				preview_changed = true;
			}
		}

		private void EditVideo_FormClosed(object sender, FormClosedEventArgs e)
		{
			axWindowsMediaPlayer1.close();
		}
	}
}
