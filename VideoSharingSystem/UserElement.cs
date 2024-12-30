using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoSharingSystem
{

	class UserElement
	{
		Form1 main;

		FlowLayoutPanel mainFlowPanel;

		FlowLayoutPanel flowPanel;
		Label nameLabel;
		Label emailLabel;

		int userId;
		public UserElement(FlowLayoutPanel mainFlowPanel, Form1 main, int userId, string name, string email)
		{
			this.userId = userId;
			this.mainFlowPanel = mainFlowPanel;
			this.main = main;

			flowPanel = new();
			nameLabel = new();
			emailLabel = new();

			nameLabel.AutoSize = true;
			nameLabel.Location = new System.Drawing.Point(3, 0);
			nameLabel.Name = "label4";
			nameLabel.Size = new System.Drawing.Size(35, 13);
			nameLabel.TabIndex = 0;
			nameLabel.Text = name;
			nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			nameLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
			nameLabel.Click += GoToProfile;

			emailLabel.AutoSize = true;
			emailLabel.Location = new System.Drawing.Point(3, 13);
			emailLabel.Name = "label5";
			emailLabel.Size = new System.Drawing.Size(35, 13);
			emailLabel.TabIndex = 1;
			emailLabel.Text = email;

			SetSize();

			flowPanel.SuspendLayout();
			flowPanel.AutoSize = true;
			flowPanel.Controls.Add(nameLabel);
			flowPanel.Controls.Add(emailLabel);
			flowPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			flowPanel.Location = new System.Drawing.Point(3, 3);
			flowPanel.Name = "flowLayoutPanel4";
			flowPanel.Size = new System.Drawing.Size(41, 26);
			flowPanel.TabIndex = 0;
			
			flowPanel.ResumeLayout(false);

			this.mainFlowPanel.Controls.Add(flowPanel);

			//_flowPanel.Visible = false;
			//_flowPanel.Visible = true;

			//_mainFlowPanel.Update();

			this.mainFlowPanel.Resize += OnResized;
		}
		public void Deatach()
		{
			mainFlowPanel.Controls.Remove(flowPanel);
		}

		private void SetSize()
		{
			emailLabel.MaximumSize = nameLabel.MaximumSize = new System.Drawing.Size(mainFlowPanel.Size.Width - 25, 200);

			flowPanel.MinimumSize = new System.Drawing.Size(mainFlowPanel.Size.Width - 25, 0);
		}

		private void OnResized(object sender, EventArgs e)
		{
			SetSize();
		}

		private void GoToProfile(object sender, EventArgs e)
		{
			new ShowUser(main, userId).ShowDialog(main);
		}
	}
}
