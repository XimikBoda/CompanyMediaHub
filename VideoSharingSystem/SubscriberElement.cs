using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoSharingSystem
{
	class SubscriberElement
	{
		Form1 _main;

		FlowLayoutPanel _mainFlowPanel;

		int _IdUser;

		Panel _panel;
		Label _nameLabel;

		public SubscriberElement(FlowLayoutPanel mainFlowPanel, Form1 main, int IdUser, string nameAndSurname)
		{
			_mainFlowPanel = mainFlowPanel;
			_IdUser = IdUser;
			_main = main;

			_panel = new();
			_nameLabel = new();


			_mainFlowPanel.SuspendLayout();

			_mainFlowPanel.Controls.Add(_panel);

			_panel.Controls.Add(_nameLabel);
			_panel.Location = new System.Drawing.Point(3, 3);
			_panel.Name = "panel1";
			_panel.Size = new System.Drawing.Size(479, 44);
			_panel.TabIndex = 0;
			//_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));


			_nameLabel.AutoSize = true;
			_nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			_nameLabel.Location = new System.Drawing.Point(16, 9);
			_nameLabel.Name = "label6";
			_nameLabel.Size = new System.Drawing.Size(52, 18);
			_nameLabel.TabIndex = 0;
			_nameLabel.Text = nameAndSurname;
			_nameLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
			_nameLabel.Click += GoToProfile;

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
			_main.InitProfileViewer(_IdUser);
		}
	}
}
