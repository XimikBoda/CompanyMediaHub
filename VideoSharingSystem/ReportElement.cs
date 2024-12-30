using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static VideoSharingSystem.Form1;

namespace VideoSharingSystem
{
	internal class ReportElement
	{
		Form1 mainForm;
		FlowLayoutPanel mainFlowPanel;

		Panel panel;
		FlowLayoutPanel flowPanel;
		Label videoNameDLabel;
		Label videoNameLabel;
		Label userNameDLabel;
		Label userNameLabel;
		Label commentDLabel;
		Label commentLabel;
		Label reportDLabel;
		Label reportLabel;
		Label reportTimeLabel;
		Button approveButton;
		Button dismissButton;

		public int userId;
		public int commentId;
		public int videoId;
		public int reportId;

		public ReportElement(FlowLayoutPanel mainFlowPanel, Form1 main, int videoId, int userId, int commentId, int reportId,
			string video_name, string user_name, string comment, string report, string report_time)
		{
			this.userId = userId;
			this.mainFlowPanel = mainFlowPanel;
			this.mainForm = main;
			this.userId = userId;
			this.commentId = commentId;
			this.videoId = videoId;
			this.reportId = reportId;

			panel = new();
			flowPanel = new();
			videoNameDLabel = new();
			videoNameLabel = new();
			userNameDLabel = new();
			userNameLabel = new();
			commentDLabel = new();
			commentLabel = new();
			reportDLabel = new();
			reportLabel = new();
			reportTimeLabel = new();
			approveButton = new();
			dismissButton = new();

			reportTimeLabel.AutoSize = true;
			//reportTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			reportTimeLabel.Location = new System.Drawing.Point(3, 13);
			reportTimeLabel.Name = "reportTimeLabel";
			reportTimeLabel.Size = new System.Drawing.Size(35, 13);
			reportTimeLabel.TabIndex = 1;
			reportTimeLabel.Text = "Дата: " + report_time;

			videoNameDLabel.AutoSize = true;
			videoNameDLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			videoNameDLabel.Location = new System.Drawing.Point(3, 13);
			videoNameDLabel.Name = "videoNameDLabel";
			videoNameDLabel.Size = new System.Drawing.Size(35, 13);
			videoNameDLabel.TabIndex = 1;
			videoNameDLabel.Text = "Відео:";

			videoNameLabel.AutoSize = true;
			videoNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			videoNameLabel.Location = new System.Drawing.Point(76, 9);
			videoNameLabel.Name = "videoNameLabel";
			videoNameLabel.Size = new System.Drawing.Size(52, 18);
			videoNameLabel.TabIndex = 0;
			videoNameLabel.Text = video_name;
			videoNameLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
			videoNameLabel.Click += GoToVideo;

			userNameDLabel.AutoSize = true;
			userNameDLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			userNameDLabel.Location = new System.Drawing.Point(3, 13);
			userNameDLabel.Name = "userNameDLabel";
			userNameDLabel.Size = new System.Drawing.Size(35, 13);
			userNameDLabel.TabIndex = 1;
			userNameDLabel.Text = "Користувач:";

			userNameLabel.AutoSize = true;
			userNameLabel.Location = new System.Drawing.Point(3, 0);
			userNameLabel.Name = "userNameLabel";
			userNameLabel.Size = new System.Drawing.Size(35, 13);
			userNameLabel.TabIndex = 0;
			userNameLabel.Text = user_name;
			userNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			userNameLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
			userNameLabel.Click += GoToProfile;

			commentDLabel.AutoSize = true;
			commentDLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			commentDLabel.Location = new System.Drawing.Point(3, 13);
			commentDLabel.Name = "commentDLabel";
			commentDLabel.Size = new System.Drawing.Size(35, 13);
			commentDLabel.TabIndex = 1;
			commentDLabel.Text = "Коментар:";

			commentLabel.AutoSize = true;
			commentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			commentLabel.Location = new System.Drawing.Point(3, 13);
			commentLabel.Name = "commentLabel";
			commentLabel.Size = new System.Drawing.Size(35, 13);
			commentLabel.TabIndex = 1;
			commentLabel.Text = comment;

			reportDLabel.AutoSize = true;
			reportDLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			reportDLabel.Location = new System.Drawing.Point(3, 13);
			reportDLabel.Name = "reportDLabel";
			reportDLabel.Size = new System.Drawing.Size(35, 13);
			reportDLabel.TabIndex = 1;
			reportDLabel.Text = "Скарга:";

			reportLabel.AutoSize = true;
			reportLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			reportLabel.Location = new System.Drawing.Point(3, 13);
			reportLabel.Name = "reportLabel";
			reportLabel.Size = new System.Drawing.Size(35, 13);
			reportLabel.TabIndex = 1;
			reportLabel.Text = report + "\n\n";

			flowPanel.AutoSize = true;
			flowPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			flowPanel.Location = new System.Drawing.Point(0, 0);
			flowPanel.Name = "flowPanel";
			flowPanel.Size = new System.Drawing.Size(41, 26);
			flowPanel.TabIndex = 0;

			//_flowPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			flowPanel.SuspendLayout();
			flowPanel.Controls.Add(reportTimeLabel);
			flowPanel.Controls.Add(videoNameDLabel);
			flowPanel.Controls.Add(videoNameLabel);
			flowPanel.Controls.Add(userNameDLabel);
			flowPanel.Controls.Add(userNameLabel);
			flowPanel.Controls.Add(commentDLabel);
			flowPanel.Controls.Add(commentLabel);
			flowPanel.Controls.Add(reportDLabel);
			flowPanel.Controls.Add(reportLabel);
			flowPanel.ResumeLayout(false);

			approveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			approveButton.ForeColor = System.Drawing.SystemColors.ControlText;
			approveButton.Location = new System.Drawing.Point(0, 0);
			approveButton.Name = "approveButton";
			approveButton.Size = new System.Drawing.Size(100, 24);
			approveButton.TabIndex = 2;
			approveButton.Text = "Схвалити";
			approveButton.UseVisualStyleBackColor = true;
			approveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			approveButton.Click += new System.EventHandler(approveButton_Click);

			dismissButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			dismissButton.ForeColor = System.Drawing.SystemColors.ControlText;
			dismissButton.Location = new System.Drawing.Point(0, 0);
			dismissButton.Name = "dismissButton";
			dismissButton.Size = new System.Drawing.Size(100, 24);
			dismissButton.TabIndex = 2;
			dismissButton.Text = "Відхилити";
			dismissButton.UseVisualStyleBackColor = true;
			dismissButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			dismissButton.Click += new System.EventHandler(dissmisButton_Click);

			//approveButton.Click += new System.EventHandler(this.button8_Click);

			//panel.SuspendLayout();
			panel.Controls.Add(flowPanel);
			panel.Controls.Add(approveButton);
			panel.Controls.Add(dismissButton);
			panel.Location = new System.Drawing.Point(3, 3);
			panel.Name = "panel";
			panel.Size = new System.Drawing.Size(479, 79);
			panel.TabIndex = 0;
			panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

			SetSize();

			mainFlowPanel.Controls.Add(panel);

			//_flowPanel.Visible = false;
			//_flowPanel.Visible = true;

			//_mainFlowPanel.Update();

			mainFlowPanel.Resize += OnResized;
		}
		public void Deatach()
		{
			mainFlowPanel.Controls.Remove(panel);
			flowPanel.Dispose();
		}

		private void GoToVideo(object sender, EventArgs e)
		{
			mainForm.InitVideoPlayer(videoId);
		}

		private void GoToProfile(object sender, EventArgs e)
		{
			new ShowUser(mainForm, userId).ShowDialog(mainForm);
		}
		private void SetSize()
		{
			panel.Size = new System.Drawing.Size(mainFlowPanel.Size.Width - 25, flowPanel.Size.Height);
			flowPanel.Size = new System.Drawing.Size(panel.Size.Width - 100, flowPanel.Size.Height);
			approveButton.Location = new System.Drawing.Point(flowPanel.Size.Width, 0);
			dismissButton.Location = new System.Drawing.Point(flowPanel.Size.Width, 29);
		}
		private void OnResized(object sender, EventArgs e)
		{
			SetSize();
		}

		private void approveButton_Click(object sender, EventArgs e)
		{
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = mainForm.bearer_token;

				string url = $"{mainForm.url_host}/reports/{reportId}/approve";

				try
				{
					HttpResponseMessage response = client.PostAsync(url, null).Result;

					if (response.IsSuccessStatusCode)
					{
						string responseBody = response.Content.ReadAsStringAsync().Result;

						var commentResult = JsonSerializer.Deserialize<GeneralResult>(responseBody);

						mainForm.deleteReportsByCommetId(commentId);
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
		private void dissmisButton_Click(object sender, EventArgs e)
		{
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = mainForm.bearer_token;

				string url = $"{mainForm.url_host}/reports/{reportId}/dismiss";

				try
				{
					HttpResponseMessage response = client.PostAsync(url, null).Result;

					if (response.IsSuccessStatusCode)
					{
						string responseBody = response.Content.ReadAsStringAsync().Result;

						var commentResult = JsonSerializer.Deserialize<GeneralResult>(responseBody);

						Deatach();
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
