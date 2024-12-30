using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoSharingSystem.Properties;

namespace VideoSharingSystem
{
	class CompanyElement
	{
		Form1 main;

		FlowLayoutPanel mainFlowPanel;

		int CompanyId;
		bool image_set = false;

		Panel panel;
		Label nameLabel;
		Label aboutLabel;
		PictureBox pictureBox1;

		public CompanyElement(FlowLayoutPanel mainFlowPanel, Form1 main, int CompanyId, string name, string about)
		{
			
			this.mainFlowPanel = mainFlowPanel;
			this.CompanyId = CompanyId;
			this.main = main;

			panel = new();
			nameLabel = new();
			aboutLabel = new();
			pictureBox1 = new();

			nameLabel.AutoSize = true;
			nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			nameLabel.Location = new System.Drawing.Point(44, 9);
			nameLabel.Name = "nameLabel";
			nameLabel.Size = new System.Drawing.Size(52, 18);
			nameLabel.TabIndex = 0;
			nameLabel.Text = name;
			nameLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
			nameLabel.Click += GoToProfile;

			int numLines = about != null ? about.Split('\n').Length : 1;

			nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8, 0, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			aboutLabel.AutoSize = true;
			aboutLabel.Location = new System.Drawing.Point(44, 9 + 13);
			aboutLabel.Name = "aboutLabel";
			aboutLabel.Size = new System.Drawing.Size(52, 12 * numLines);
			aboutLabel.TabIndex = 0;
			aboutLabel.Text = about;
			aboutLabel.Click += GoToProfile;
			aboutLabel.Update();


			pictureBox1.Location = new System.Drawing.Point(3, 3);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new System.Drawing.Size(35, 35);
			pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBox1.TabIndex = 3;
			pictureBox1.TabStop = false;
			pictureBox1.Click += GoToProfile;


			panel.Location = new System.Drawing.Point(3, 3);
			panel.Name = "panel1";
			panel.Size = new System.Drawing.Size(479, aboutLabel.Location.Y + aboutLabel.Size.Height);
			panel.TabIndex = 0;
			panel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
			panel.Click += GoToProfile;

			panel.Controls.Add(nameLabel);
			panel.Controls.Add(aboutLabel);
			panel.Controls.Add(pictureBox1);

			SetSize();

			this.mainFlowPanel.Controls.Add(panel);

			this.mainFlowPanel.ResumeLayout(false);

			panel.Visible = false;
			panel.Visible = true;

			this.mainFlowPanel.Update();

			this.mainFlowPanel.Resize += OnResized;
		}
		public void Deatach()
		{
			if (pictureBox1.Image != null)
				pictureBox1.Image.Dispose();
			mainFlowPanel.Controls.Remove(panel);
		}

		private void SetSize()
		{
			panel.Size = new System.Drawing.Size(mainFlowPanel.Size.Width - 25, panel.Size.Height);
		}

		private void OnResized(object sender, EventArgs e)
		{
			SetSize();
		}
		private void GoToProfile(object sender, EventArgs e)
		{
			main.InitCompanyViewer(CompanyId);
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

					string url = $"{main.url_host}/company/{CompanyId}/logo";

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
