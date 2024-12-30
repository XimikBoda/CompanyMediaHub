using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static VideoSharingSystem.Form1;
using System.Xml.Linq;
using System.Text.Json;

namespace VideoSharingSystem
{
	public partial class CommentReport : Form
	{
		Form1 main;
		int commentId;
		public CommentReport(Form1 main, int commentId, int userId, string name, string comment)
		{
			this.main = main;
			this.commentId = commentId;

			InitializeComponent();

			nameTextBox.Text = name;
			textRichTextBox.Text = comment;
		}

		private void canselButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void sendButton_Click(object sender, EventArgs e)
		{
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = main.bearer_token;

				string url = $"{main.url_host}/comments/{commentId}/report";
				var loginData = new { comment_id = commentId, report_reason = reportContentRichTextBox.Text };
				string json = JsonSerializer.Serialize(loginData);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				try
				{
					HttpResponseMessage response = client.PostAsync(url, content).Result;

					if (response.IsSuccessStatusCode)
					{
						string responseBody = response.Content.ReadAsStringAsync().Result;

						var commentResult = JsonSerializer.Deserialize<NewCommentnfo>(responseBody);

						MessageBox.Show("Скаргу відправлено");
						Close();
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
	}
}
