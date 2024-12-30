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
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static VideoSharingSystem.Form1;

namespace VideoSharingSystem
{
	public partial class AddVideo : Form
	{
		Form1 _mainForm;
		int idCompany;
		string _selectedFileName;
		CancellationTokenSource cancelToken = new CancellationTokenSource();
		public AddVideo(Form1 mainForm, int idCompany)
		{
			_mainForm = mainForm;
			this.idCompany = idCompany;
			InitializeComponent();

			foreach (var item in _mainForm.tags.tags)
				TagsCheckedListBox.Items.Add(item);
		}

		private async void UploadButton_Click(object sender, EventArgs e)
		{
			if (_mainForm.currentCompanyId == -1)
				return;


			using (var filestream = File.OpenRead(_selectedFileName))
			{
				var progress = new System.Net.Http.Handlers.ProgressMessageHandler();

				progress.HttpSendProgress += (object sender, System.Net.Http.Handlers.HttpProgressEventArgs e) =>
				{
					int progressPercentage = (int)(e.BytesTransferred * 100 / e.TotalBytes);
					if (progressPercentage > 100)
						progressPercentage = 100;
					try
					{
						if (!cancelToken.Token.IsCancellationRequested)
							this.Invoke(new MethodInvoker(delegate ()
							{
								progressBar1.Value = progressPercentage;
							}));
					} catch (Exception ex) { }
				};

				using (HttpClient client = HttpClientFactory.Create(progress))
				{
					client.DefaultRequestHeaders.Authorization = _mainForm.bearer_token;
					client.DefaultRequestHeaders.Add("X-idCompany", idCompany.ToString());

					List<int> tags = new List<int>();
					foreach (object itemChecked in TagsCheckedListBox.CheckedItems)
					{
						Tag tag = (Tag)itemChecked;
						tags.Add(tag.id);
					}

					var videoInfo = new { name = nameTextBox.Text, description = descriptionRichTextBox.Text, idCompany = idCompany, tags = tags };
					string json = JsonSerializer.Serialize(videoInfo);
					string url = $"{_mainForm.url_host}/video/upload";

					try
					{

						HttpContent imageStreamContent = null;
						HttpContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
						HttpContent fileStreamContent = new StreamContent(filestream);
						MemoryStream imageStream = null;

						bool imageExist = pictureBox1.Image != null;
						if (imageExist)
						{
							imageStream = new MemoryStream();
							pictureBox1.Image.Save(imageStream, ImageFormat.Jpeg);
							imageStreamContent = new ByteArrayContent(imageStream.ToArray());
						}

						using (var formData = new MultipartFormDataContent())
						{

							formData.Add(stringContent);
							formData.Add(fileStreamContent, "file", Path.GetFileName(_selectedFileName));
							if (imageExist)
								formData.Add(imageStreamContent, "preview", "preview.jpg");

							nameTextBox.Enabled = false;
							selectButton.Enabled = false;
							UploadButton.Enabled = false;
							descriptionRichTextBox.Enabled = false;
							TagsCheckedListBox.Enabled = false;
							var response = await client.PostAsync(url, formData, cancelToken.Token);

							if (response.IsSuccessStatusCode)
							{
								string responseBody = response.Content.ReadAsStringAsync().Result;

								var loginResult = JsonSerializer.Deserialize<GeneralResult>(responseBody);

								MessageBox.Show(loginResult.message);

								Close();
							}
							else
							{
								MessageBox.Show("Помилка при виконанні запита: " + response.StatusCode);
							}
							nameTextBox.Enabled = true;
							selectButton.Enabled = true;
							UploadButton.Enabled = true;
							descriptionRichTextBox.Enabled = true;
							TagsCheckedListBox.Enabled = true;
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

		private void CancelButton_Click(object sender, EventArgs e)
		{
			cancelToken.Cancel();
			axWindowsMediaPlayer1.close();
			Close();
		}

		private void AddVideo_FormClosed(object sender, FormClosedEventArgs e)
		{
			cancelToken.Cancel();
			axWindowsMediaPlayer1.close();
		}

		private void button1_Click(object sender, EventArgs e)
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

			axWindowsMediaPlayer1.Ctlenabled = true;
			axWindowsMediaPlayer1.uiMode = old_mode;
			axWindowsMediaPlayer1.Size = old_size;

			if (wasPlaying)
				axWindowsMediaPlayer1.Ctlcontrols.play();
		}

		private void selectButton_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();

			openFileDialog.Filter = "Video file|*.mp4;*.avi;*.mkv|Audio|*.mp3|All files (*.*)|*.*";
			openFileDialog.FilterIndex = 0;
			openFileDialog.RestoreDirectory = true;

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				_selectedFileName = openFileDialog.FileName;

				if (nameTextBox.Text.Length == 0)
					nameTextBox.Text = Path.GetFileName(_selectedFileName);

				axWindowsMediaPlayer1.Visible = true;
				axWindowsMediaPlayer1.URL = _selectedFileName;

				UploadButton.Enabled = true;
			}
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
			}
		}
	}
}
