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

namespace VideoSharingSystem
{

	public class link
	{
		public string temporary_link { get; set; }
	}
	partial class Form1
	{
		public int currentVideoId = -1;
		int currentVideoUserId = -1;
		int myRateId = 0;

		int rand_number = 0;

		List<CommentElement> commentElements;

		public void InitVideoPlayer(int id)
		{
			axWindowsMediaPlayer1.URL = "";
			splitContainer1.Enabled = true;

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

						var linkResult = JsonSerializer.Deserialize<link>(responseBody);

						axWindowsMediaPlayer1.URL = linkResult.temporary_link;

						//if (loginResult != null || loginResult.message == "success")
						//{
						//	Visible = false;
						//	var mainFrorm = new Form1(loginResult.token);
						//	mainFrorm.ShowDialog();
						//	mainFrorm.Dispose();
						//	Visible = true;
						//}
						//else
						//{
						//	MessageBox.Show("Невірний логін або пароль: " + loginResult.message);
						//}

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
			//if (currentVideoId == id)
			//{
			//	tabControl1.SelectedIndex = 1;
			//	return;
			//}
			//using (SqlConnection connection = new SqlConnection(connectionString))
			//{
			//	SqlCommand command = new SqlCommand("SELECT IdVideo, NameV, DescriptionV, VideoData, NameUser, Surname, V.IdUser " +
			//		"FROM Videos V JOIN Users U ON V.IdUser = U.IdUser WHERE IdVideo = @IdVideo ", connection);
			//	command.Parameters.AddWithValue("@IdVideo", id);
			//	command.Parameters.AddWithValue("@IdUser", myUserId);

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
			//		LoadsComments();
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

		public void DeInitVideoPlayer()
		{
			label1.Text = "Спочатку оберіть відео в одній з інших вкладок";
			splitContainer1.Enabled = false;
			axWindowsMediaPlayer1.URL = "";
			if (currentVideoId != -1)
				File.Delete(getTempFilePlaceById(currentVideoId));
			currentVideoId = -1;
		}

		string getTempFilePlaceById(int id) => Path.GetTempPath() + "VideoSharingSystem_cache_" +
			rand_number.ToString() + "_" + id.ToString() + ".mp4";
		public void LoadsComments()
		{
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
			if (currentUserId == -1 || currentVideoId == -1)
				return;
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
