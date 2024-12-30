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

namespace VideoSharingSystem
{
	class SubscriberElement
	{
		Form1 _main;

		FlowLayoutPanel _mainFlowPanel;

		int _IdUser;
		bool image_set = false;

		Panel _panel;
		Label _nameLabel;
		PictureBox _pictureBox1;

		public SubscriberElement(FlowLayoutPanel mainFlowPanel, Form1 main, int IdUser, string nameAndSurname)
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));

			_mainFlowPanel = mainFlowPanel;
			_IdUser = IdUser;
			_main = main;

			_panel = new();
			_nameLabel = new();
			_pictureBox1 = new();


			_mainFlowPanel.SuspendLayout();

			_mainFlowPanel.Controls.Add(_panel);

			_panel.Controls.Add(_nameLabel);
			_panel.Controls.Add(_pictureBox1);
			_panel.Location = new System.Drawing.Point(3, 3);
			_panel.Name = "panel1";
			_panel.Size = new System.Drawing.Size(479, 44);
			_panel.TabIndex = 0;
			_panel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
			_panel.Click += GoToProfile;
			//_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));


			_nameLabel.AutoSize = true;
			_nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			_nameLabel.Location = new System.Drawing.Point(44, 9);
			_nameLabel.Name = "label6";
			_nameLabel.Size = new System.Drawing.Size(52, 18);
			_nameLabel.TabIndex = 0;
			_nameLabel.Text = nameAndSurname;
			_nameLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
			_nameLabel.Click += GoToProfile;
			// 
			// pictureBox1
			// 
			_pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
			_pictureBox1.Location = new System.Drawing.Point(3, 3);
			_pictureBox1.Name = "pictureBox1";
			_pictureBox1.Size = new System.Drawing.Size(35, 35);
			_pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			_pictureBox1.TabIndex = 3;
			_pictureBox1.TabStop = false;
			_pictureBox1.Click += GoToProfile;

			SetSize();

			_mainFlowPanel.ResumeLayout(false);

			_panel.Visible = false;
			_panel.Visible = true;

			_mainFlowPanel.Update();

			_mainFlowPanel.Resize += OnResized;
		}
		public void Deatach()
		{
			_mainFlowPanel.Controls.Remove(_panel);
		}

		private void SetSize()
		{
			_panel.Size = new System.Drawing.Size(_mainFlowPanel.Size.Width - 25, _panel.Size.Height);
		}

		private void OnResized(object sender, EventArgs e)
		{
			SetSize();
		}
		private void GoToProfile(object sender, EventArgs e)
		{
			_main.InitCompanyViewer(_IdUser);
		}

		public async void LoadImage()
		{
			if (!image_set)
			{
				image_set = true;
				using (HttpClient client = new HttpClient())
				{
					client.DefaultRequestHeaders.Authorization = _main.bearer_token;

					string url = $"{_main.url_host}/company/{_IdUser}/logo";

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
