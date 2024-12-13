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
		}

		public class CommentInfo
		{
			public int id { get; set; }
			public int user_id { get; set; }
			public string text { get; set; }
			public string user_login { get; set; }
		}

		public class CommentsInfo
		{
			public List<CommentInfo> list { get; set; }
		}


		public int currentVideoId = -1;
		int currentVideoUserId = -1;
		int myRateId = 0;

		int rand_number = 0;

		List<CommentElement> commentElements;
		List<TagElement> tagElements = new List<TagElement>();

		public void InitVideoPlayer(int id)
		{
			if (currentVideoId == id)
			{
				tabControl1.SelectedIndex = 1;
				return;
			}

			currentVideoId = id;

			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = bearer_token;
				
				string loginUrl = $"{url_host}/video/get/{id}";

				try
				{
					HttpResponseMessage response = client.GetAsync(loginUrl).Result;

					if (response.IsSuccessStatusCode)
					{
						string responseBody = response.Content.ReadAsStringAsync().Result;

						var linkResult = JsonSerializer.Deserialize<VideoInfoGet>(responseBody);

						axWindowsMediaPlayer1.URL = linkResult.temporary_link;
						label1.Text = linkResult.name.Trim();
						richTextBox1.Text = linkResult.description.Trim();

						{
							foreach (var el in tagElements)
								el.Deatach();
							tagElements.Clear();

							foreach (var item in linkResult.tags)
								tagElements.Add(new TagElement(flowLayoutPanel7, this, item.name));
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
			
			LoadsComments();

			//	try
			//	{
			//		connection.Open();
			//		{
			//			SqlDataReader reader = command.ExecuteReader();
			//			if (reader.Read())
			//			{
			//				int lastCurrentVideoId = currentVideoId;

			//				currentVideoId = Convert.ToInt32(reader[0].ToString());
			//				label1.Text = reader[1].ToString().Trim();
			//				richTextBox1.Text = reader[2].ToString().Trim();
			//				label12.Text = reader[4].ToString().Trim() + ' ' + reader[5].ToString().Trim();
			//				currentVideoUserId = Convert.ToInt32(reader[6].ToString());

			//				if (currentVideoUserId == myUserId || isAdmin)
			//					button10.Visible = true;
			//				else
			//					button10.Visible = false;

			//				{
			//					byte[] data = (byte[])reader[3];
			//					var file = File.Create(getTempFilePlaceById(currentVideoId));
			//					file.Write(data, 0, data.Length);
			//					file.Close();
			//				}
			//				axWindowsMediaPlayer1.URL = getTempFilePlaceById(currentVideoId);
			//				//axWindowsMediaPlayer1.Ctlcontrols.play();

			//				if (lastCurrentVideoId != -1)
			//					File.Delete(getTempFilePlaceById(lastCurrentVideoId));

			//				splitContainer1.Enabled = true;
			//				label12.Visible = true;
			//			}
			//			reader.Close();
			//			{
			//				command.CommandText = "INSERT INTO ViewHistory (IdUser, IdVideo, ViewTime)" +
			//					"VALUES (@IdUser, @IdVideo, GETDATE())";
			//				command.ExecuteNonQuery();
			//			}
			//		}
			//		
			//		LoadsRatingAndVievers();

			//		tabControl1.SelectedIndex = 1;
			//	}
			//	catch (Exception ex)
			//	{
			//		Console.WriteLine(ex.Message);
			//		MessageBox.Show(ex.Message);
			//	}
			//}
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

				string loginUrl = $"{url_host}/video/comments/{currentVideoId}";

				try
				{
					HttpResponseMessage response = client.GetAsync(loginUrl).Result;

					if (response.IsSuccessStatusCode)
					{
						string responseBody = response.Content.ReadAsStringAsync().Result;

						var commentsResult = JsonSerializer.Deserialize<List<CommentInfo>>(responseBody);

						{
							foreach (var el in commentElements)
								el.Deatach();
							commentElements.Clear();

							foreach (var item in commentsResult)
								commentElements.Add(new CommentElement(flowLayoutPanel3, this, item.id, item.user_id, item.user_login, item.text));
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

			//using (SqlConnection connection = new SqlConnection(connectionString))
			//{
			//	SqlCommand command = new SqlCommand(
			//		"SELECT IdComment, U.IdUser, NameUser, Surname, TextComment " +
			//		"FROM Comments C JOIN Users U ON C.IdUser = U.IdUser WHERE IdVideo = @id ORDER BY IdComment DESC", connection);
			//	command.Parameters.AddWithValue("@id", currentVideoId);

			//	try
			//	{
			//		connection.Open();
			//		{
			//			SqlDataReader reader = command.ExecuteReader();
			//			foreach (var el in commentElements)
			//				el.Deatach();
			//			commentElements.Clear();
			//			while (reader.Read())
			//			{

			//				commentElements.Add(new CommentElement(flowLayoutPanel3, this,
			//					Convert.ToInt32(reader[0].ToString()), Convert.ToInt32(reader[1].ToString()),
			//					reader[2].ToString().Trim() + ' ' + reader[3].ToString().Trim(), reader[4].ToString().Trim()));
			//			}
			//			reader.Close();
			//		}
			//	}
			//	catch (Exception ex)
			//	{
			//		Console.WriteLine(ex.Message);
			//		MessageBox.Show(ex.Message);
			//	}
			//}
		}

		public void DeleteComment(int id)
		{
			//using (SqlConnection connection = new SqlConnection(connectionString))
			//{
			//	SqlCommand command = new SqlCommand("Delete FROM Comments WHERE IdComment = @IdComment", connection);
			//	command.Parameters.Add("@IdComment", SqlDbType.Int);

			//	command.Parameters["@IdComment"].Value = id;

			//	try
			//	{
			//		connection.Open();
			//		command.ExecuteNonQuery();
			//		LoadsComments();
			//		MessageBox.Show("Коментар видаленно");
			//	}
			//	catch (Exception ex)
			//	{
			//		Console.WriteLine(ex.Message);
			//		MessageBox.Show("Коментар не вдалося видалити" + ex.Message);
			//	}
			//}
		}
		public void AddComment(string comment)
		{
			if (currentVideoId == -1)
				return;

			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = bearer_token;

				string loginUrl = $"{url_host}/video/comments/{currentVideoId}";
				var loginData = new { message = comment };
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
							LoadsComments();
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
				}
			}
			//using (SqlConnection connection = new SqlConnection(connectionString))
			//{
			//	SqlCommand command = new SqlCommand("INSERT INTO Comments (IdUser, IdVideo, TextComment, Date) VALUES(@IdUser, @IdVideo, @TextComment, GETDATE())", connection);
			//	command.Parameters.Add("@IdUser", SqlDbType.Int);
			//	command.Parameters.Add("@IdVideo", SqlDbType.Int);
			//	command.Parameters.Add("@TextComment", SqlDbType.VarChar, 500);

			//	command.Parameters["@IdUser"].Value = myUserId;
			//	command.Parameters["@IdVideo"].Value = currentVideoId;
			//	command.Parameters["@TextComment"].Value = comment;
			//	try
			//	{
			//		connection.Open();
			//		command.ExecuteNonQuery();
			//		LoadsComments();
			//	}
			//	catch (Exception ex)
			//	{
			//		Console.WriteLine(ex.Message);
			//		MessageBox.Show(ex.Message);
			//	}
			//}
		}
		public void AddRatings(int IdRatingType)
		{
			if (currentUserId == -1 || currentVideoId == -1)
				return;
			if (myRateId == IdRatingType)
				IdRatingType = 0;
			//using (SqlConnection connection = new SqlConnection(connectionString))
			//{
			//	SqlCommand command = new SqlCommand("Delete FROM Ratings WHERE IdUser = @IdUser AND IdVideo = @IdVideo", connection);
			//	command.Parameters.Add("@IdUser", SqlDbType.Int);
			//	command.Parameters.Add("@IdVideo", SqlDbType.Int);
			//	command.Parameters.Add("@IdRatingType", SqlDbType.Int);

			//	command.Parameters["@IdUser"].Value = myUserId;
			//	command.Parameters["@IdVideo"].Value = currentVideoId;
			//	command.Parameters["@IdRatingType"].Value = IdRatingType;
			//	try
			//	{
			//		connection.Open();
			//		command.ExecuteNonQuery();
			//		command.CommandText = "INSERT INTO Ratings (IdUser, IdVideo, IdRatingType, RatingTime) VALUES (@IdUser, @IdVideo, @IdRatingType, GETDATE())";
			//		if (IdRatingType != 0)
			//			command.ExecuteNonQuery();
			//	}
			//	catch (Exception ex)
			//	{
			//		Console.WriteLine(ex.Message);
			//		MessageBox.Show(ex.Message);
			//	}
			//}
			LoadsRatingAndVievers();
		}
		public void LoadsRatingAndVievers()
		{
			//using (SqlConnection connection = new SqlConnection(connectionString))
			//{
			//	SqlCommand command = new SqlCommand(
			//		"SELECT * FROM GetRatingAndViewes WHERE IdVideo = @IdVideo", connection);
			//	command.Parameters.AddWithValue("@IdVideo", currentVideoId);
			//	command.Parameters.AddWithValue("@IdUser", myUserId);

			//	try
			//	{
			//		connection.Open();
			//		{
			//			SqlDataReader reader = command.ExecuteReader();
			//			if (reader.Read())
			//			{
			//				int views = Convert.ToInt32(reader[3].ToString());
			//				int uniqueviews = Convert.ToInt32(reader[4].ToString());
			//				int likes = Convert.ToInt32(reader[1].ToString());
			//				int dislikes = Convert.ToInt32(reader[2].ToString());

			//				label13.Text = $"Кількість переглядів: {views} ({uniqueviews} унікальних)";
			//				label14.Text = $"Рейтинг:  {likes - dislikes} (+{likes}/-{dislikes})";
			//				label13.Visible = true;
			//				label14.Visible = true;
			//			}
			//			reader.Close();
			//		}
			//		{
			//			command.CommandText = "SELECT IdRatingType FROM Ratings WHERE IdVideo = @IdVideo AND IdUser = @IdUser";
			//			SqlDataReader reader = command.ExecuteReader();
			//			if (reader.Read())
			//			{
			//				myRateId = Convert.ToInt32(reader[0].ToString());
			//			}
			//			else
			//				myRateId = 0;
			//		}
			//		{
			//			switch (myRateId)
			//			{
			//				case 0:
			//					this.button1.BackColor = System.Drawing.Color.Transparent;
			//					this.button2.BackColor = System.Drawing.Color.Transparent;
			//					break;
			//				case 1:
			//					this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
			//					this.button2.BackColor = System.Drawing.Color.Transparent;
			//					break;
			//				case 2:
			//					this.button1.BackColor = System.Drawing.Color.Transparent;
			//					this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			//					break;
			//			}
			//		}
			//	}
			//	catch (Exception ex)
			//	{
			//		Console.WriteLine(ex.Message);
			//		MessageBox.Show(ex.Message);
			//	}
			//}
		}

		~Form1()
		{
			DeInitVideoPlayer();
		}
	}
}
