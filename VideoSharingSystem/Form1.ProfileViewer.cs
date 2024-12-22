using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

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
		public class CompanyInfo
		{
			public int id { get; set; }
			public string name { get; set; }
			public string about { get; set; }
			public int subscribers { get; set; }
			public bool is_subscribed { get; set; }
		}

		public class SubscribeResult
		{
			public bool is_subscribed { get; set; }
			public int subscribers { get; set; }
			public string message { get; set; }
		}
		public class SubscribeInfo
		{
			public int company_id { get; set; }
			public string company_name { get; set; }
		}


		List<VideoElement> videoElements;
		List<SubscriberElement> subscriberElement;

		public bool isSubscribedToCurrent = false;
		public int currentCompanyId = 1;

		public async void InitProfileViewer(int id)
		{
			currentCompanyId = id;
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = bearer_token;

				string loginUrl = $"{url_host}/company/{currentCompanyId}";

				try
				{
					HttpResponseMessage response = await client.GetAsync(loginUrl);

					if (response.IsSuccessStatusCode)
					{
						string responseBody = await response.Content.ReadAsStringAsync();

						var companyResult = JsonSerializer.Deserialize<CompanyInfo>(responseBody);

						currentCompanyId = companyResult.id;
						label2.Text = companyResult.name;
						richTextBox3.Text = companyResult.about;

						SetSubscribeState(companyResult.is_subscribed, companyResult.subscribers);

						//if (currentUserId == myUserId || isAdmin)
						//	button9.Visible = true;
						//else
						//	button9.Visible = false;

						//button8.Visible = currentUserId == myUserId;

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
				client.DefaultRequestHeaders.Authorization = bearer_token;

				string loginUrl = $"{url_host}/company/{currentCompanyId}/videos";

				try
				{
					HttpResponseMessage response = await client.GetAsync(loginUrl);

					if (response.IsSuccessStatusCode)
					{
						string responseBody = await response.Content.ReadAsStringAsync();

						var videosResult = JsonSerializer.Deserialize<List<VideoInfo>>(responseBody);

						flowLayoutPanel1.SuspendLayout();

						foreach (var el in videoElements)
							el.Deatach();
						videoElements.Clear();

						foreach (var item in videosResult)
							videoElements.Add(new VideoElement(flowLayoutPanel1, this,
								item.id, item.name, item.description, item.upload_time.ToString()));

						flowLayoutPanel1.ResumeLayout();


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
		public async void GetMySubscriptions() {
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = bearer_token;

				string loginUrl = $"{url_host}/profile/subscriptions";

				try
				{
					HttpResponseMessage response = await client.GetAsync(loginUrl);

					if (response.IsSuccessStatusCode)
					{
						string responseBody = await response.Content.ReadAsStringAsync();

						var subscribesResult = JsonSerializer.Deserialize<List<SubscribeInfo>>(responseBody);

						flowLayoutPanel2.SuspendLayout();

						foreach (var el in subscriberElement)
							el.Deatach();
						subscriberElement.Clear();

						foreach (var item in subscribesResult)
							subscriberElement.Add(new SubscriberElement(flowLayoutPanel2, this,
								item.company_id, item.company_name));

						flowLayoutPanel2.ResumeLayout();
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


		public void SetSubscribeState(bool state, int subscribers) {

			label3.Text = "Кількість підписників: " + subscribers.ToString();
			if (state)
			{
				isSubscribedToCurrent = true;
				button6.Enabled = true;
				button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
				button6.Text = "Підписаний";
			}
			else
			{

				isSubscribedToCurrent = false;
				button6.Enabled = true;
				button6.BackColor = Color.Transparent;
				button6.Text = "Підписатися";
			}
		}

		public async void SetSubscribe()
		{
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = bearer_token;

				string act;
				if (isSubscribedToCurrent)
					act = "unsubscribe";
				else
					act = "subscribe";

				string loginUrl = $"{url_host}/company/{currentCompanyId}/{act}";

				try
				{
					button6.Enabled = false;

					HttpResponseMessage response = await client.PostAsync(loginUrl, null);

					if (response.IsSuccessStatusCode)
					{
						string responseBody = await response.Content.ReadAsStringAsync();

						var subscribeResult = JsonSerializer.Deserialize<SubscribeResult>(responseBody);

						SetSubscribeState(subscribeResult.is_subscribed, subscribeResult.subscribers);

						GetMySubscriptions();
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
				button6.Enabled = true;
			}
		}
	}
}
