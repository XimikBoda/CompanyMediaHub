using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using static VideoSharingSystem.Form1;

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
			public int company_id { get; set; }
			public string company_name { get; set; }
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
		public int currentCompanyId = -1;
		bool cs_label_company = false;

		public async Task InitCompanyViewer(int id)
		{
			if (currentCompanyId == id) {
				tabControl1.SelectedIndex = 1;
				return;
			}
			currentCompanyId = id;

			subscribeButton.Enabled = true;

			if (profileInfo.comp_owner.Exists(x => x.company_id == currentCompanyId) || is_admin)
				companyEditButton.Visible = uploadButton.Visible = true;
			else
				companyEditButton.Visible = uploadButton.Visible = false;

			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = bearer_token;

				string url = $"{url_host}/company/{currentCompanyId}";

				try
				{
					HttpResponseMessage response = await client.GetAsync(url);

					if (response.IsSuccessStatusCode)
					{
						string responseBody = await response.Content.ReadAsStringAsync();

						var companyResult = JsonSerializer.Deserialize<CompanyInfo>(responseBody);

						currentCompanyId = companyResult.id;
						companyNameLabel.Text = companyResult.name;
						richTextBox3.Text = companyResult.about;

						SetSubscribeState(companyResult.is_subscribed, companyResult.subscribers);

						tabControl1.SelectedIndex = 1;
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

				string url = $"{url_host}/company/{currentCompanyId}/logo";

				try
				{
					byte[] imageBytes = await client.GetByteArrayAsync(url);
					using MemoryStream ms = new MemoryStream(imageBytes);
					Image image = Image.FromStream(ms);
					companyLogoPictureBox.Image = image;
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					MessageBox.Show(ex.Message);
				}
			}

			new Task(() => GetMediaList()).Start();

			//new GetMediaList();

		}

		public async Task GetMediaList() {
			int curCompId = currentCompanyId;

			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = bearer_token;

				string url = $"{url_host}/company/{currentCompanyId}/videos";

				try
				{
					HttpResponseMessage response = client.GetAsync(url).Result;

					if (response.IsSuccessStatusCode)
					{
						string responseBody = response.Content.ReadAsStringAsync().Result;

						var videosResult = JsonSerializer.Deserialize<List<VideoInfo>>(responseBody);

						if (curCompId != currentCompanyId)
							return;

						this.Invoke(new MethodInvoker(delegate ()
						{
							flowLayoutPanel1.SuspendLayout();

							foreach (var el in videoElements)
								el.Deatach();
							videoElements.Clear();

							
							flowLayoutPanel1.ResumeLayout();
						}));

						int c = 0;

						foreach (var item in videosResult)
						{
							this.Invoke(new MethodInvoker(delegate ()
							{
								videoElements.Add(new VideoElement(flowLayoutPanel1, this,
									item.id, item.name, item.description, item.upload_time.ToString()));
							}));
							if (curCompId != currentCompanyId)
								return;
							//if (c>4)
								Thread.Sleep(10);
							++c;
						}

					}
					else
					{
						MessageBox.Show("Ошибка при виконанні запита: " + response.StatusCode);
					}
				}
				catch (Exception ex)
				{
				//	Console.WriteLine(ex.Message);
				//	MessageBox.Show(ex.Message);
				}
			}
		}

		public async void GetMySubscriptions() {
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = bearer_token;

				string url = $"{url_host}/profile/subscriptions";

				try
				{
					HttpResponseMessage response = await client.GetAsync(url);

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
				subscribeButton.Enabled = true;
				subscribeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
				subscribeButton.Text = "Підписаний";
			}
			else
			{

				isSubscribedToCurrent = false;
				subscribeButton.Enabled = true;
				subscribeButton.BackColor = Color.Transparent;
				subscribeButton.Text = "Підписатися";
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

				string url = $"{url_host}/company/{currentCompanyId}/{act}";

				try
				{
					subscribeButton.Enabled = false;

					HttpResponseMessage response = await client.PostAsync(url, null);

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
				subscribeButton.Enabled = true;
			}
		}

		void SetCS_viewer(bool company) {
			if (company == cs_label_company)
				return;
			cs_label_company = company;

			if (!cs_label_company)
			{
				cs_label.Text = "Мої підписки:";
				GetMySubscriptions();
			}
			else {
				cs_label.Text = "Мої компанії:";
				{
					flowLayoutPanel2.SuspendLayout();

					foreach (var el in subscriberElement)
						el.Deatach();
					subscriberElement.Clear();

					foreach (var item in profileInfo.comp_owner)
						subscriberElement.Add(new SubscriberElement(flowLayoutPanel2, this,
							item.company_id, item.company_name));

					flowLayoutPanel2.ResumeLayout();
				}
			}

		}

		private void cs_label_Click(object sender, EventArgs e)
		{
			if (is_comp_owner)
				SetCS_viewer(!cs_label_company);
		}

	}
}
