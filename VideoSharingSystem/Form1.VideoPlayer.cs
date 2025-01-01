using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net.Http;
using System.Text.Json;
using AxWMPLib;
using System.Security.Policy;
using System.Net;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using System.Reflection.Emit;

namespace VideoSharingSystem
{
	partial class Form1
	{
		public class VideoInfoGet
		{
			public string name { get; set; }
			public string description { get; set; }
			public string temporary_link { get; set; }
			public List<Tag> tags { get; set; }


			public int likes { get; set; }
			public int dislikes { get; set; }
			public int user_rating { get; set; }
			public int total_views { get; set; }
			public int unique_viewers { get; set; }
			public int company_id { get; set; }
			public string company_name { get; set; }
		}

		public class CommentInfo
		{
			public int id { get; set; }
			public int user_id { get; set; }
			public string text { get; set; }
			public string user_login { get; set; }
			public bool can_delete { get; set; }
			public bool reported { get; set; }
		}
		public class NewCommentnfo
		{
			public CommentInfo comment { get; set; }
			public string message { get; set; }

		}

		public class RaringInfo
		{
			public int likes { get; set; }
			public int dislikes { get; set; }
			public int user_rating { get; set; }
			public string message { get; set; }

		}

		public class CommentsInfo
		{
			public List<CommentInfo> list { get; set; }
		}


		public int currentVideoId = -1;
		int currentVideoUserId = -1;
		int currentVideoCompanyId = -1;
		int myRateId = 0;

		List<CommentElement> commentElements;
		List<TagElement> tagElements = new List<TagElement>();

		public void InitVideoPlayer(int id)
		{
			if (currentVideoId == id)
			{
				tabControl1.SelectedIndex = 2;
				return;
			}

			currentVideoId = id;

			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = bearer_token;
				
				string url = $"{url_host}/video/{id}/get";

				try
				{
					HttpResponseMessage response = client.GetAsync(url).Result;

					if (response.IsSuccessStatusCode)
					{
						string responseBody = response.Content.ReadAsStringAsync().Result;

						var videoInfo = JsonSerializer.Deserialize<VideoInfoGet>(responseBody);

						axWindowsMediaPlayer1.URL = url_host + videoInfo.temporary_link;
						label1.Text = videoInfo.name.Trim();
						richTextBox1.Text = videoInfo.description.Trim();

						{
							foreach (var el in tagElements)
								el.Deatach();
							tagElements.Clear();

							foreach (var item in videoInfo.tags)
								tagElements.Add(new TagElement(flowLayoutPanel7, this, item.name));
						}

						button10.Visible = is_admin || profileInfo.comp_owner.Exists(x => x.company_id == videoInfo.company_id);

						splitContainer1.Enabled = true;

						currentVideoCompanyId = videoInfo.company_id;

						myRateId = videoInfo.user_rating;
						label12.Text = videoInfo.company_name;
						label13.Text = $"Кількість переглядів: {videoInfo.total_views} ({videoInfo.unique_viewers} унікальних)";
						label14.Text = $"Рейтинг:  {videoInfo.likes - videoInfo.dislikes} (+{videoInfo.likes}/-{videoInfo.dislikes})";
						label12.Visible = true;
						label13.Visible = true;
						label14.Visible = true;


						UpdateLikeButtons();

						tabControl1.SelectedIndex = 2;
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
			
			LoadsComments();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();

			saveFileDialog.Filter = "Video file (*.mp4, *.avi)|*.mp4;*.avi|All files (*.*)|*.*";
			saveFileDialog.FilterIndex = 0;
			saveFileDialog.RestoreDirectory = true;
			saveFileDialog.FileName = label1.Text + ".mp4";

			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				using (var client = new WebClient())
				{
					client.DownloadFileCompleted += (sender, e) =>
					{
						if (e.Error != null)
							MessageBox.Show($"Файл {Path.GetFileName(saveFileDialog.FileName)} не вдалося завантажити");
						else
							MessageBox.Show($"Файл {Path.GetFileName(saveFileDialog.FileName)} завантажено");
					};
					client.DownloadFileAsync(new System.Uri(axWindowsMediaPlayer1.URL), saveFileDialog.FileName);
				}
			}
		}

		public void DeInitVideoPlayer()
		{
			label1.Text = "Спочатку оберіть відео в одній з інших вкладок";
			splitContainer1.Enabled = false;
			axWindowsMediaPlayer1.URL = "";
			currentVideoId = -1;
		}

		public void LoadsComments()
		{
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = bearer_token;

				string url = $"{url_host}/video/{currentVideoId}/comments";

				try
				{
					HttpResponseMessage response = client.GetAsync(url).Result;

					if (response.IsSuccessStatusCode)
					{
						string responseBody = response.Content.ReadAsStringAsync().Result;

						var commentsResult = JsonSerializer.Deserialize<List<CommentInfo>>(responseBody);

						{
							foreach (var el in commentElements)
								el.Deatach();
							commentElements.Clear();

							foreach (var item in commentsResult)
								commentElements.Add(new CommentElement(flowLayoutPanel3, this, item.id, item.user_id, item.user_login, item.text, item.can_delete, item.reported));
						}

						splitContainer1.Enabled = true;

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

		public void DeleteComment(int id)
		{
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = bearer_token;

				string url = $"{url_host}/comments/{id}";
				//var loginData = new { message = comment };
				//string json = JsonSerializer.Serialize(loginData);
				//var content = new StringContent(json, Encoding.UTF8, "application/json");

				try
				{
					HttpResponseMessage response = client.DeleteAsync(url).Result;

					if (response.IsSuccessStatusCode)
					{
						string responseBody = response.Content.ReadAsStringAsync().Result;

						var loginResult = JsonSerializer.Deserialize<GeneralResult>(responseBody);

						if (loginResult != null)
						{
							LoadsComments();
						}
						else
						{
							MessageBox.Show(loginResult.message);
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
		public void AddComment(string comment)
		{
			if (currentVideoId == -1)
				return;

			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = bearer_token;

				string url = $"{url_host}/video/{currentVideoId}/comments";
				var loginData = new { message = comment };
				string json = JsonSerializer.Serialize(loginData);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				try
				{
					HttpResponseMessage response = client.PostAsync(url, content).Result;

					if (response.IsSuccessStatusCode)
					{
						string responseBody = response.Content.ReadAsStringAsync().Result;

						var commentResult = JsonSerializer.Deserialize<NewCommentnfo>(responseBody);

						if (commentResult != null)
						{
							var ncomment = commentResult.comment;
							commentElements.Add(new CommentElement(flowLayoutPanel3, this, ncomment.id, ncomment.user_id, ncomment.user_login, ncomment.text, true, false));
							//LoadsComments();
						}
						else
						{
							MessageBox.Show(commentResult.message);
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
		public async void AddRatings(int IdRatingType)
		{
			if (currentCompanyId == -1 || currentVideoId == -1)
				return;
			if (myRateId == IdRatingType)
				IdRatingType = 0;

			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = bearer_token;

				string url = $"{url_host}/video/{currentVideoId}/rating";
				var loginData = new { rating = IdRatingType };
				string json = JsonSerializer.Serialize(loginData);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				button1.Enabled = false;
				button2.Enabled = false;

				try
				{
					HttpResponseMessage response = await client.PostAsync(url, content);

					if (response.IsSuccessStatusCode)
					{
						string responseBody = response.Content.ReadAsStringAsync().Result;

						var ratingResult = JsonSerializer.Deserialize<RaringInfo>(responseBody);

						if (ratingResult != null )
						{
							myRateId = ratingResult.user_rating;
							label14.Text = $"Рейтинг:  {ratingResult.likes - ratingResult.dislikes} (+{ratingResult.likes}/-{ratingResult.dislikes})";
							label14.Visible = true;
							UpdateLikeButtons();
						}
						else
						{
							//MessageBox.Show(ratingResult.message);
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

				button1.Enabled = true;
				button2.Enabled = true;
			}
		}


		public void UpdateLikeButtons() {
			switch (myRateId)
			{
				case -1:
					this.button1.BackColor = System.Drawing.Color.Transparent;
					this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
					break;
				case 0:
					this.button1.BackColor = System.Drawing.Color.Transparent;
					this.button2.BackColor = System.Drawing.Color.Transparent;
					break;
				case 1:
					this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
					this.button2.BackColor = System.Drawing.Color.Transparent;
					break;
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			AddRatings(1);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			AddRatings(-1);
		}

		private void label12_Click(object sender, EventArgs e)
		{
			InitCompanyViewer(currentVideoCompanyId);
		}

		~Form1()
		{
			DeInitVideoPlayer();
		}
	}
}
