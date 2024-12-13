using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoSharingSystem
{
	class VideoElement
	{
		Form1 _main;

		FlowLayoutPanel _mainFlowPanel;

		int _videoId;

		Panel _panel;
		Label _nameLabel;
		Label _descriptionLabel;
		Label _uploadTimeLabel;

		public VideoElement(FlowLayoutPanel mainFlowPanel, Form1 main, int videoId, string name, string description, string uploadTime)
		{
			_mainFlowPanel = mainFlowPanel;
			_videoId = videoId;
			_main = main;

			_panel = new();
			_nameLabel = new();
			_descriptionLabel = new();
			_uploadTimeLabel = new();


			_mainFlowPanel.SuspendLayout();

			_mainFlowPanel.Controls.Add(_panel);

			_panel.Controls.Add(_uploadTimeLabel);
			_panel.Controls.Add(_descriptionLabel);
			_panel.Controls.Add(_nameLabel);
			_panel.Location = new System.Drawing.Point(3, 3);
			_panel.Name = "panel1";
			_panel.Size = new System.Drawing.Size(479, 79);
			_panel.TabIndex = 0;
			//_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			// 
			// label6
			// 
			_nameLabel.AutoSize = true;
			_nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			_nameLabel.Location = new System.Drawing.Point(16, 9);
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
			_descriptionLabel.Location = new System.Drawing.Point(17, 31);
			_descriptionLabel.Name = "label7";
			_descriptionLabel.Size = new System.Drawing.Size(35, 13);
			_descriptionLabel.TabIndex = 1;
			_descriptionLabel.Text = description;
			// 
			// label8
			// 
			_uploadTimeLabel.AutoSize = true;
			_uploadTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			_uploadTimeLabel.Location = new System.Drawing.Point(17, 52);
			_uploadTimeLabel.Name = "label8";
			_uploadTimeLabel.Size = new System.Drawing.Size(41, 13);
			_uploadTimeLabel.TabIndex = 2;
			_uploadTimeLabel.Text = uploadTime;


			if (description.Length == 0)
			{
				_descriptionLabel.Visible = false;
				_uploadTimeLabel.Location = new System.Drawing.Point(
					_uploadTimeLabel.Location.X, _uploadTimeLabel.Location.Y - _descriptionLabel.Size.Height);
				_panel.Size = new System.Drawing.Size(_panel.Size.Width, _panel.Size.Height - _descriptionLabel.Size.Height);
			}

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
		private void GoToVideo(object sender, EventArgs e)
		{
			_main.InitVideoPlayer(_videoId);
		}
	}
}
