using AxWMPLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
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
	public partial class AddVideo : Form
	{
		Form1 _mainForm;
		string _selectedFileName;
		public AddVideo(Form1 mainForm)
		{
			_mainForm = mainForm;
			InitializeComponent();

			foreach (var item in _mainForm.tags.tags)
				checkedListBox1.Items.Add(item);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();

			openFileDialog.Filter = "Video file (*.mp4, *.avi)|*.mp4;*.avi|All files (*.*)|*.*";
			openFileDialog.FilterIndex = 0;
			openFileDialog.RestoreDirectory = true;

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				_selectedFileName = openFileDialog.FileName;

				if (textBox1.Text.Length == 0)
					textBox1.Text = Path.GetFileName(_selectedFileName);

				axWindowsMediaPlayer1.Visible = true;
				axWindowsMediaPlayer1.URL = _selectedFileName;

				button3.Enabled = true;
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			if (_mainForm.currentUserId == -1)
				return;

			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = _mainForm.bearer_token;

				List<int> tags = new List<int>();

				foreach (object itemChecked in checkedListBox1.CheckedItems)
				{
					Tag tag = (Tag)itemChecked;
					tags.Add(tag.id);
				}

				var videoInfo= new { name = textBox1.Text, description = richTextBox1.Text, idCompany = 1, tags = tags };
				string json = JsonSerializer.Serialize(videoInfo);
				string url = $"http://25.18.114.207:8080/video/upload";

				using (var filestream = File.OpenRead(_selectedFileName))
				{

					HttpContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
					HttpContent fileStreamContent = new StreamContent(filestream);
					using (var formData = new MultipartFormDataContent())
					{
						formData.Add(stringContent);
						formData.Add(fileStreamContent, "file", Path.GetFileName(_selectedFileName));
						var response = client.PostAsync(url, formData).Result;
						if (response.IsSuccessStatusCode)
						{
							string responseBody = response.Content.ReadAsStringAsync().Result;

							var loginResult = JsonSerializer.Deserialize<LoginResult>(responseBody);

							MessageBox.Show(loginResult.message);

						}
						else
						{
							MessageBox.Show("Ошибка при виконанні запита: " + response.StatusCode);
						}
					}
				}
				Close();

				
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			axWindowsMediaPlayer1.close();
			Close();
		}

		private void AddVideo_FormClosed(object sender, FormClosedEventArgs e)
		{
			axWindowsMediaPlayer1.close();
		}
	}
}
