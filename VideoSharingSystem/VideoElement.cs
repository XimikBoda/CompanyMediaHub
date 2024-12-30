using AxWMPLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoSharingSystem.Properties;
using static VideoSharingSystem.Form1;

namespace VideoSharingSystem
{
	class VideoElement
	{
		Form1 _main;

		FlowLayoutPanel _mainFlowPanel;

		int _videoId;
		bool image_set = false;

		Panel _panel;
		Label _nameLabel;
		Label _descriptionLabel;
		Label _uploadTimeLabel;
		PictureBox _pictureBox1;

		public VideoElement(FlowLayoutPanel mainFlowPanel, Form1 main, int videoId, string name, string description, string uploadTime)
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));

			_mainFlowPanel = mainFlowPanel;
			_videoId = videoId;
			_main = main;

			_panel = new();
			_nameLabel = new();
			_descriptionLabel = new();
			_uploadTimeLabel = new();
			_pictureBox1 = new();

			//_mainFlowPanel.SuspendLayout();

			
			//_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			// 
			// label6
			// 
			_nameLabel.AutoSize = true;
			_nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			_nameLabel.Location = new System.Drawing.Point(76, 9);
			_nameLabel.Name = "label6";
			_nameLabel.Size = new System.Drawing.Size(52, 18);
			_nameLabel.TabIndex = 0;
			_nameLabel.Text = name;
			_nameLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
			_nameLabel.Click += GoToVideo;
			// 
			// label7
			// 
			_descriptionLabel.AutoSize = true;
			_descriptionLabel.Location = new System.Drawing.Point(76, 31);
			_descriptionLabel.Name = "label7";
			_descriptionLabel.Size = new System.Drawing.Size(35, 13);
			_descriptionLabel.TabIndex = 1;
			_descriptionLabel.Text = description;
			_descriptionLabel.Click += GoToVideo;
			// 
			// label8
			// 
			_uploadTimeLabel.AutoSize = true;
			_uploadTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			_uploadTimeLabel.Location = new System.Drawing.Point(76, 52);
			_uploadTimeLabel.Name = "label8";
			_uploadTimeLabel.Size = new System.Drawing.Size(41, 13);
			_uploadTimeLabel.TabIndex = 2;
			_uploadTimeLabel.Text = uploadTime;
			_uploadTimeLabel.Click += GoToVideo;
			// 
			// pictureBox1
			// 
			_pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
			_pictureBox1.Location = new System.Drawing.Point(9, 9);
			_pictureBox1.Name = "pictureBox1";
			_pictureBox1.Size = new System.Drawing.Size(60, 60);
			_pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			_pictureBox1.TabIndex = 3;
			_pictureBox1.TabStop = false;
			_pictureBox1.Click += GoToVideo;

			_panel.SuspendLayout();
			_panel.Controls.Add(_uploadTimeLabel);
			_panel.Controls.Add(_descriptionLabel);
			_panel.Controls.Add(_nameLabel);
			_panel.Controls.Add(_pictureBox1);
			_panel.Location = new System.Drawing.Point(3, 3);
			_panel.Name = "panel1";
			_panel.Size = new System.Drawing.Size(479, 79);
			_panel.TabIndex = 0;
			_panel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
			_panel.Click += GoToVideo;


			if (description.Length == 0)
			{
				_descriptionLabel.Visible = false;
				_uploadTimeLabel.Location = new System.Drawing.Point(
					_uploadTimeLabel.Location.X, _uploadTimeLabel.Location.Y - _descriptionLabel.Size.Height);
				_panel.Size = new System.Drawing.Size(_panel.Size.Width, _panel.Size.Height - _descriptionLabel.Size.Height);
			}

			SetSize();

			//_mainFlowPanel.ResumeLayout(false);

			_panel.ResumeLayout();
			_mainFlowPanel.Controls.Add(_panel);

			//_panel.Visible = false;
			//_panel.Visible = true;

			//_mainFlowPanel.Update();

			_mainFlowPanel.Resize += OnResized;

		}
		public void Deatach()
		{
			_pictureBox1.Image.Dispose();
			_mainFlowPanel.Controls.Remove(_panel);
			_panel.Dispose();
		}

		private void SetSize()
		{
			_panel.Size = new System.Drawing.Size(_mainFlowPanel.Size.Width - 25, _panel.Size.Height);
		}

		private void OnResized(object sender, EventArgs e)
		{
			SetSize();
		}
		private void GoToVideo(object sender, EventArgs e)
		{
			_main.InitVideoPlayer(_videoId);
		}
		public async void LoadImage() 
		{
			if (!image_set)
			{
				image_set = true;
				using (HttpClient client = new HttpClient())
				{
					client.DefaultRequestHeaders.Authorization = _main.bearer_token;

					string url = $"{_main.url_host}/video/{_videoId}/preview";

					try
					{
						byte[] imageBytes = await client.GetByteArrayAsync(url);
						using MemoryStream ms = new MemoryStream(imageBytes);
						Image image = Image.FromStream(ms);
						_pictureBox1.Image = image;
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
