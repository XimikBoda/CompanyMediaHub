using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace VideoSharingSystem
{
	partial class Form1
	{
		List<VideoElement> findVideoElements = new();
		List<VideoElement> findAudioElements = new();
		List<CompanyElement> findCompanyElement = new();
		List<UserElement> findUserElement = new();

		public class CompanyShortInfo
		{
			public int company_id { get; set; }
			public string name { get; set; }
			public string about { get; set; }
		}

		public class UserShortInfo
		{
			public int user_id { get; set; }
			public string name { get; set; }
			public string email { get; set; }
		}
		public class FindHistoryInfo
		{
			public int IdSearchHistory { get; set; }
			public string SearchQuery { get; set; }
			public DateTime SearchTime { get; set; }
		}

		public class FindResult
		{
			public List<VideoInfo> video { get; set; }
			public List<VideoInfo> audio { get; set; }
			public List<CompanyShortInfo> company { get; set; }
			public List<UserShortInfo> user { get; set; }
		}

		public void InitHistory()
		{
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = bearer_token;

				string url = $"{url_host}/search/history";

				try
				{
					HttpResponseMessage response = client.GetAsync(url).Result;

					if (response.IsSuccessStatusCode)
					{
						string responseBody = response.Content.ReadAsStringAsync().Result;

						var history = JsonSerializer.Deserialize<List<FindHistoryInfo>>(responseBody);

						comboBox1.Items.Clear();
						foreach (var item in history)
							comboBox1.Items.Add(item.SearchQuery);
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
		public void Find(string findString)
		{
			List<string> type = new();
			if (findVideoCheckBox.Checked)
				type.Add("video");
			if (findAudioCheckBox.Checked)
				type.Add("audio");
			if (findCompanyCheckBox.Checked)
				type.Add("company");
			if (findUserCheckBox.Checked)
				type.Add("user");

			List<int> tags = new List<int>();
			foreach (object itemChecked in TagsCheckedListBox.CheckedItems)
				tags.Add(((Tag)itemChecked).id);

			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = bearer_token;

				string url = $"{url_host}/search";
				var findRequest = new
				{
					type = type,
					tags = tags,
					request = findString
				};
				string json = JsonSerializer.Serialize(findRequest);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				try
				{
					HttpResponseMessage response = client.PostAsync(url, content).Result;

					if (response.IsSuccessStatusCode)
					{
						string responseBody = response.Content.ReadAsStringAsync().Result;

						var findResult = JsonSerializer.Deserialize<FindResult>(responseBody);

						findFlowLayoutPanel.SuspendLayout();

						foreach (var el in findVideoElements)
							el.Deatach();
						findVideoElements.Clear();

						foreach (var el in findAudioElements)
							el.Deatach();
						findAudioElements.Clear();

						foreach (var el in findCompanyElement)
							el.Deatach();
						findCompanyElement.Clear();

						foreach (var el in findUserElement)
							el.Deatach();
						findUserElement.Clear();

						foreach (var item in findResult.video)
							findVideoElements.Add(new VideoElement(findFlowLayoutPanel, this,
								item.id, item.name, item.description, item.upload_time.ToString(),
								item.company_id, item.company_name
							)
						);

						foreach (var item in findResult.audio)
							findAudioElements.Add(new VideoElement(findFlowLayoutPanel, this,
								item.id, item.name, item.description, item.upload_time.ToString(),
								item.company_id, item.company_name
							)
						);

						foreach (var item in findResult.company)
							findCompanyElement.Add(new CompanyElement(findFlowLayoutPanel, this,
								item.company_id, item.name, item.about));

						foreach (var item in findResult.user)
							findUserElement.Add(new UserElement(findFlowLayoutPanel, this,
								item.user_id, item.name, item.email));

						findFlowLayoutPanel.ResumeLayout();

						if (findString != "")
							InitHistory();
						//	comboBox1.Items.Add(findString);
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

		private void button7_Click(object sender, EventArgs e)
		{
			Find(comboBox1.Text);
		}

		private void FindTextBoxEnter(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				Find(comboBox1.Text);
				e.Handled = true;
			}
		}
	}
}
