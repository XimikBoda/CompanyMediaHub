using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static VideoSharingSystem.Form1;

namespace VideoSharingSystem
{
	public partial class Form1 : Form
	{
		public class ProfileInfo
		{
			public int user_id { get; set; }
			public string login { get; set; }
			public string name { get; set; }
			public string surname { get; set; }
			public bool is_admin { get; set; }
			public List<SubscribeInfo> mod { get; set; }
			public List<SubscribeInfo> comp_owner { get; set; }
		}
		public class ReportInfo
		{
			public int user_id { get; set; }
			public string user_name { get; set; }
			public int video_id { get; set; }
			public string video_name { get; set; }
			public int comment_id { get; set; }
			public string comment { get; set; }
			public int report_id { get; set; }
			public string report_reason { get; set; }
			public DateTime report_time { get; set; }
		}

		public class Tag
		{
			public int id { get; set; }
			public string name { get; set; }

			public override string ToString() => name;
		}
		public class Tags
		{
			public List<Tag> tags { get; set; }
		}

		public int myUserId = -1;
		public bool is_admin = false;
		public bool is_comp_owner = false;
		public bool is_mod = false;

		public AuthenticationHeaderValue bearer_token;
		public string url_host;

		public ProfileInfo profileInfo;

		public Tags tags = new Tags();

		List<VideoElement> recomedationVideoElements = new();


		List<ReportElement> reportElements = new();

		public Form1(string token, string url_host, int user_id, bool is_admin, bool is_comp_owner, bool is_mod)
		{
			bearer_token = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
			this.url_host = url_host;

			myUserId = user_id;
			this.is_admin = is_admin;
			this.is_comp_owner = is_comp_owner;
			this.is_mod = is_mod;

			commentElements = new();
			videoElements = new();
			subscriberElement = new();

			InitializeComponent();

			button6.Visible = is_admin;

			GetProfileInfo();
			if (is_comp_owner)
			{
				cs_label.ForeColor = System.Drawing.SystemColors.HotTrack;
				SetCS_viewer(true);
			}

			InitHistory();
			GetTags();

			foreach (var item in tags.tags)
				TagsCheckedListBox.Items.Add(item);

			GetRecomedation();

			InitCompanyViewer(1);
		}

		void GetRecomedation()
		{
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = bearer_token;

				string url = $"{url_host}/video/recommendations";

				try
				{
					HttpResponseMessage response = client.GetAsync(url).Result;

					if (response.IsSuccessStatusCode)
					{
						string responseBody = response.Content.ReadAsStringAsync().Result;

						var recomendations = JsonSerializer.Deserialize<List<VideoInfo>>(responseBody);

						foreach (var el in recomedationVideoElements)
							el.Deatach();
						recomedationVideoElements.Clear();

						foreach (var item in recomendations)
							recomedationVideoElements.Add(new VideoElement(recomendationVideoLayoutPanel, this,
								item.id, item.name, item.description, item.upload_time.ToString(),
								item.company_id, item.company_name
							)
						);
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

		private void GetProfileInfo()
		{
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = bearer_token;

				string url = $"{url_host}/profile";

				try
				{
					HttpResponseMessage response = client.GetAsync(url).Result;

					if (response.IsSuccessStatusCode)
					{
						string responseBody = response.Content.ReadAsStringAsync().Result;

						profileInfo = JsonSerializer.Deserialize<ProfileInfo>(responseBody);
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

		private void GetTags()
		{
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = bearer_token;

				string url = $"{url_host}/video/tags";

				try
				{
					HttpResponseMessage response = client.GetAsync(url).Result;

					if (response.IsSuccessStatusCode)
					{
						string responseBody = response.Content.ReadAsStringAsync().Result;

						tags = JsonSerializer.Deserialize<Tags>(responseBody);
					}
					else
					{
						tags = new Tags();
						MessageBox.Show("Ошибка при виконанні запита: " + response.StatusCode);
					}
				}
				catch (Exception ex)
				{
					tags = new Tags();
					Console.WriteLine(ex.Message);
					MessageBox.Show(ex.Message);
				}
			}
		}



		private void button5_Click(object sender, EventArgs e)
		{
			new EditUser(this, myUserId).ShowDialog(this);
		}

		private void label9_Click(object sender, EventArgs e)
		{

		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		void UpdateSomeElementPositionOnProfile()
		{
			cs_label.Location = new Point(splitContainer6.SplitterDistance + 541 - 527, cs_label.Location.Y);
			uploadButton.Location = new Point(splitContainer6.SplitterDistance + 381 - 527, uploadButton.Location.Y);
		}

		private void Form1_Resized(object sender, EventArgs e)
		{
			UpdateSomeElementPositionOnProfile();
		}

		private void splitContainer6_SplitterMoved(object sender, SplitterEventArgs e)
		{
			UpdateSomeElementPositionOnProfile();
		}

		private void flowLayoutPanel5_Paint(object sender, PaintEventArgs e)
		{

		}

		private void button4_Click(object sender, EventArgs e)
		{
			AddComment(richTextBox2.Text);
			richTextBox2.Text = "";
		}
		private void button4_Enter(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				AddComment(richTextBox2.Text);
				richTextBox2.Text = "";
				e.Handled = true;
			}
		}

		private void button8_Click(object sender, EventArgs e)
		{
			AddVideo f = new(this, currentCompanyId);
			f.ShowDialog(this);
			int ccurrentCompanyId = currentCompanyId;
			currentCompanyId = -1;
			InitCompanyViewer(ccurrentCompanyId);
		}

		private void button9_Click(object sender, EventArgs e)
		{
			new EditCompany(this, currentCompanyId).ShowDialog(this);
			int companyId = currentCompanyId;
			currentCompanyId = -1;
			InitCompanyViewer(companyId);
		}

		private void button6_Click(object sender, EventArgs e)
		{
			SetSubscribe();
		}

		private void button10_Click(object sender, EventArgs e)
		{
			int ccurrentVideoId = currentVideoId;

			var wasPlaying = axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying;
			if (wasPlaying)
				axWindowsMediaPlayer1.Ctlcontrols.pause();

			new EditVideo(this, currentVideoId).ShowDialog(this);

			if (wasPlaying)
				axWindowsMediaPlayer1.Ctlcontrols.play();

			if (currentVideoId != -1)
			{
				DeInitVideoPlayer();
				//currentVideoId = -1;
				InitVideoPlayer(ccurrentVideoId);
			}
		}

		private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
		{

		}

		private void label2_Click(object sender, EventArgs e)
		{

		}

		private void pictureBox1_Paint(object sender, PaintEventArgs e)
		{

		}

		private void button6_Click_1(object sender, EventArgs e)
		{
			new AddCompany(this).ShowDialog(this);
		}

		private void tabControl1_TabIndexChanged(object sender, EventArgs e)
		{

		}

		public void deleteReportsByCommetId(int id) {
			for (int i = 0; i < reportElements.Count; ++i)
				if (reportElements[i].commentId == id) {
					reportElements[i].Deatach();
					reportElements.RemoveAt(i);
					--i;
				}
		}

		private void tabControl1_Selected(object sender, TabControlEventArgs e)
		{
			if (tabControl1.SelectedIndex == 3)
			{
				using (HttpClient client = new HttpClient())
				{
					client.DefaultRequestHeaders.Authorization = bearer_token;

					string url = $"{url_host}/reports";

					try
					{
						HttpResponseMessage response = client.GetAsync(url).Result;

						if (response.IsSuccessStatusCode)
						{
							string responseBody = response.Content.ReadAsStringAsync().Result;

							var videosResult = JsonSerializer.Deserialize<List<ReportInfo>>(responseBody);

							foreach (var el in reportElements)
								el.Deatach();
							reportElements.Clear();

							int c = 0;

							foreach (var item in videosResult)
							{

								reportElements.Add(new ReportElement(flowLayoutPanel6, this,
									item.video_id, item.user_id, item.comment_id, item.report_id, item.video_name, item.user_name, item.comment, item.report_reason, item.report_time.ToString()));

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
		}
	}
}
