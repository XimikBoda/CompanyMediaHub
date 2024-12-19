using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace VideoSharingSystem
{
	partial class Form1
	{
		public class VideoInfo
		{
			public int id { get; set; }
			public string name { get; set; }
			public string description { get; set; }
			public DateTime upload_time { get; set; }
		}
		public class VideosInfo
		{
			public List<VideoInfo> list { get; set; }

			public string message { get; set; }
		}

		List<VideoElement> videoElements;
		List<SubscriberElement> subscriberElement;

		public bool isSubscribedToCurrent = false;
		public int currentUserId = 1;

		public void InitProfileViewer(int id)
		{
			//currentUserId = id;

			using (HttpClient client = new HttpClient())
			{
				string loginUrl = $"http://25.18.114.207:8080/video";

				try
				{
					HttpResponseMessage response = client.GetAsync(loginUrl).Result;

					if (response.IsSuccessStatusCode)
					{
						string responseBody = response.Content.ReadAsStringAsync().Result;

						var videosResult = JsonSerializer.Deserialize<List<VideoInfo>>(responseBody);

						foreach (var el in videoElements)
							el.Deatach();
						videoElements.Clear();

						foreach (var item in videosResult)
							videoElements.Add(new VideoElement(flowLayoutPanel1, this,
								item.id, item.name, item.description, item.upload_time.ToString()));

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
			//		"SELECT IdUser, Email, LoginUser, NameUser, Surname, Patronymic, Birthday, RegisterTime, About FROM Users WHERE IdUser = @id ", connection);
			//	command.Parameters.AddWithValue("@id", id);

			//	SqlCommand command2 = new SqlCommand(
			//		"SELECT IdVideo, NameV, DescriptionV, UploadTime FROM Videos WHERE IdUser = @id ORDER BY UploadTime DESC", connection);
			//	command2.Parameters.AddWithValue("@id", id);


			//	SqlCommand command3 = new SqlCommand(
			//		"SELECT IdSubscriber, IdTargetUser, S.IdUser, NameUser, Surname FROM Subscribers S JOIN Users U ON S.IdTargetUser = U.IdUser WHERE S.IdUser = @id ", connection);
			//	command3.Parameters.AddWithValue("@id", id);


			//	try
			//	{
			//		connection.Open();
			//		{
			//			SqlDataReader reader = command.ExecuteReader();
			//			if (reader.Read())
			//			{
			//				foreach (var el in videoElements)
			//					el.Deatach();
			//				videoElements.Clear();

			//				foreach (var el in subscriberElement)
			//					el.Deatach();
			//				subscriberElement.Clear();

			//				currentUserId = Convert.ToInt32(reader[0].ToString());
			//				label2.Text = reader[3].ToString().Trim() + ' ' + reader[4].ToString().Trim();
			//				label3.Text = reader[1].ToString().Trim();
			//				//label6.Text = reader[6].ToString().Trim();
			//				richTextBox3.Text = reader[8].ToString().Trim();

			//				if (currentUserId == myUserId || isAdmin)
			//					button9.Visible = true;
			//				else
			//					button9.Visible = false;

			//				button8.Visible = currentUserId == myUserId;

			//			}
			//			reader.Close();
			//		}
			//		{
			//			SqlDataReader reader = command2.ExecuteReader();
			//			while (reader.Read())
			//			{

			//				videoElements.Add(new VideoElement(flowLayoutPanel1, this,
			//					Convert.ToInt32(reader[0].ToString()),
			//					reader[1].ToString().Trim(), reader[2].ToString().Trim(), reader[3].ToString().Trim()));
			//			}
			//			reader.Close();
			//		}
			//		{
			//			SqlDataReader reader = command3.ExecuteReader();
			//			while (reader.Read())
			//			{

			//				subscriberElement.Add(new SubscriberElement(flowLayoutPanel2, this,
			//					Convert.ToInt32(reader[1].ToString()),
			//					reader[3].ToString().Trim() + ' ' + reader[4].ToString().Trim()));
			//			}
			//			reader.Close();
			//		}
			//		CheckSubscribe();

			//		tabControl1.SelectedIndex = 0;
			//	}
			//	catch (Exception ex)
			//	{
			//		Console.WriteLine(ex.Message);
			//		MessageBox.Show(ex.Message);
			//	}
			//}
		}

		public void CheckSubscribe()
		{
			//if (myUserId == currentUserId)
			//{
			//	isSubscribedToCurrent = false;
			//	button6.Enabled = false;
			//	button6.BackColor = Color.Transparent;
			//	button6.Text = "Підписатися";
			//}
			//else
			//	using (SqlConnection connection = new SqlConnection(connectionString))
			//	{

			//		SqlCommand command = new SqlCommand(
			//			"SELECT IdUser, IdTargetUser FROM Subscribers WHERE IdUser = @MyIdUser AND IdTargetUser = @IdTargetUser", connection);
			//		command.Parameters.AddWithValue("@MyIdUser", myUserId);
			//		command.Parameters.AddWithValue("@IdTargetUser", currentUserId);

			//		try
			//		{
			//			connection.Open();
			//			{
			//				SqlDataReader reader = command.ExecuteReader();
			//				if (reader.Read())
			//				{
			//					isSubscribedToCurrent = true;
			//					button6.Enabled = true;
			//					button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
			//					button6.Text = "Підписаний";
			//				}
			//				else
			//				{

			//					isSubscribedToCurrent = false;
			//					button6.Enabled = true;
			//					button6.BackColor = Color.Transparent;
			//					button6.Text = "Підписатися";
			//				}
			//				reader.Close();
			//			}
			//		}
			//		catch (Exception ex)
			//		{
			//			Console.WriteLine(ex.Message);
			//			MessageBox.Show(ex.Message);
			//		}
			//	}
		}
		public void SetSubscribe()
		{
			//using (SqlConnection connection = new SqlConnection(connectionString))
			//{
			//	SqlCommand command = new SqlCommand("", connection);
			//	command.Parameters.Add("@MyIdUser", SqlDbType.Int);
			//	command.Parameters.Add("@IdTargetUser", SqlDbType.Int);

			//	command.Parameters["@MyIdUser"].Value = myUserId;
			//	command.Parameters["@IdTargetUser"].Value = currentUserId;
			//	try
			//	{
			//		connection.Open();
			//		if (isSubscribedToCurrent)
			//			command.CommandText = "Delete FROM Subscribers WHERE IdUser = @MyIdUser AND IdTargetUser = @IdTargetUser";
			//		else
			//			command.CommandText = "INSERT INTO Subscribers (IdUser, IdTargetUser) VALUES (@MyIdUser, @IdTargetUser)";
			//		command.ExecuteNonQuery();
			//		CheckSubscribe();
			//	}
			//	catch (Exception ex)
			//	{
			//		Console.WriteLine(ex.Message);
			//		MessageBox.Show(ex.Message);
			//	}
			//}
		}
	}
}
