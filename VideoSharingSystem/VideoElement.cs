using AxWMPLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoSharingSystem.Properties;
using static VideoSharingSystem.Form1;

namespace VideoSharingSystem
{
	class VideoElement
	{
		Form1 main;

		FlowLayoutPanel mainFlowPanel;

		int videoId;
		int companyId;
		bool image_set = false;

		Panel panel;
		Label nameLabel;
		Label companyNameLabel;
		Label descriptionLabel;
		Label uploadTimeLabel;
		PictureBox pictureBox1;

		public VideoElement(FlowLayoutPanel mainFlowPanel, Form1 main, int videoId, string name, string description, string uploadTime, int companyId = -1, string companyName = "")
		{
			this.mainFlowPanel = mainFlowPanel;
			this.videoId = videoId;
			this.main = main;
			this.companyId = companyId;

			panel = new();
			nameLabel = new();
			companyNameLabel = new();
			descriptionLabel = new();
			uploadTimeLabel = new();
			pictureBox1 = new();

			//_mainFlowPanel.SuspendLayout();


			//_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			// 
			// label6
			// 
			nameLabel.AutoSize = true;
			nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			nameLabel.Location = new System.Drawing.Point(76, 9);
			nameLabel.Name = "label6";
			nameLabel.Size = new System.Drawing.Size(52, 18);
			nameLabel.TabIndex = 0;
			nameLabel.Text = name;
			nameLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
			nameLabel.Click += GoToVideo;
			// 
			// label7
			// 

			int add_y = 0;
			if (companyId != -1)
			{
				companyNameLabel.AutoSize = true;
				companyNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
				companyNameLabel.Location = new System.Drawing.Point(76, 31);
				companyNameLabel.Name = "companyNameLabel";
				companyNameLabel.Size = new System.Drawing.Size(52, 18);
				companyNameLabel.TabIndex = 0;
				companyNameLabel.Text = companyName;
				companyNameLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
				companyNameLabel.Click += GoToCompany;
				add_y = 15;
			}
			descriptionLabel.AutoSize = true;
			descriptionLabel.Location = new System.Drawing.Point(76, 31 + add_y);
			descriptionLabel.Name = "label7";
			descriptionLabel.Size = new System.Drawing.Size(35, 13);
			descriptionLabel.TabIndex = 1;
			descriptionLabel.Text = description;
			descriptionLabel.Click += GoToVideo;
			// 
			// label8
			// 
			uploadTimeLabel.AutoSize = true;
			uploadTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			uploadTimeLabel.Location = new System.Drawing.Point(76, 52 + add_y);
			uploadTimeLabel.Name = "label8";
			uploadTimeLabel.Size = new System.Drawing.Size(41, 13);
			uploadTimeLabel.TabIndex = 2;
			uploadTimeLabel.Text = uploadTime;
			uploadTimeLabel.Click += GoToVideo;
			// 
			// pictureBox1
			// 
			pictureBox1.Location = new System.Drawing.Point(9, 9);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new System.Drawing.Size(60, 60);
			pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBox1.TabIndex = 3;
			pictureBox1.TabStop = false;
			pictureBox1.Click += GoToVideo;

			panel.SuspendLayout();
			panel.Controls.Add(uploadTimeLabel);
			panel.Controls.Add(descriptionLabel);
			panel.Controls.Add(nameLabel);
			if (companyId != -1)
				panel.Controls.Add(companyNameLabel);
			panel.Controls.Add(pictureBox1);
			panel.Location = new System.Drawing.Point(3, 3);
			panel.Name = "panel1";
			panel.Size = new System.Drawing.Size(479, 79 + add_y);
			panel.TabIndex = 0;
			panel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
			panel.Click += GoToVideo;


			if (description.Length == 0)
			{
				descriptionLabel.Visible = false;
				uploadTimeLabel.Location = new System.Drawing.Point(
					uploadTimeLabel.Location.X, uploadTimeLabel.Location.Y - descriptionLabel.Size.Height);
				panel.Size = new System.Drawing.Size(panel.Size.Width, panel.Size.Height - descriptionLabel.Size.Height);
			}

			SetSize();

			//_mainFlowPanel.ResumeLayout(false);

			panel.ResumeLayout();
			this.mainFlowPanel.Controls.Add(panel);

			//_panel.Visible = false;
			//_panel.Visible = true;

			//_mainFlowPanel.Update();

			this.mainFlowPanel.Resize += OnResized;

		}
		public void Deatach()
		{
			if (pictureBox1.Image != null)
				pictureBox1.Image.Dispose();
			mainFlowPanel.Controls.Remove(panel);
			panel.Dispose();
		}

		private void SetSize()
		{
			panel.Size = new System.Drawing.Size(mainFlowPanel.Size.Width - 25, panel.Size.Height);
		}

		private void OnResized(object sender, EventArgs e)
		{
			SetSize();
		}
		private void GoToVideo(object sender, EventArgs e)
		{
			main.InitVideoPlayer(videoId);
		}
		private void GoToCompany(object sender, EventArgs e)
		{
			main.InitCompanyViewer(companyId);
		}
		public async void LoadImage()
		{
			if (!image_set)
			{
				image_set = true;
				System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
				pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
				using (HttpClient client = new HttpClient())
				{
					client.DefaultRequestHeaders.Authorization = main.bearer_token;

					string url = $"{main.url_host}/video/{videoId}/preview";

					try
					{
						byte[] imageBytes = await client.GetByteArrayAsync(url);
						using MemoryStream ms = new MemoryStream(imageBytes);
						Image image = Image.FromStream(ms);
						pictureBox1.Image = image;
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.Message);
						MessageBox.Show(ex.Message);
					}
				}
			}
		}
		private void panel_Paint(object sender, PaintEventArgs e)
		{
			if (!e.ClipRectangle.IsEmpty)
				LoadImage();
		}
	}
}
