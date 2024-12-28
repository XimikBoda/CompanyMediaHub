using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoSharingSystem
{
	class CommentElement
	{
		Form1 _main;

		FlowLayoutPanel _mainFlowPanel;

		FlowLayoutPanel _flowPanel;
		Label _nameLabel;
		Label _commentLabel;

		int _userId;
		int _commentId;
		public CommentElement(FlowLayoutPanel mainFlowPanel, Form1 main, int commentId, int userId, string name, string comment)
		{
			_userId = userId;
			_mainFlowPanel = mainFlowPanel;
			_main = main;
			_commentId = commentId;

			_flowPanel = new();
			_nameLabel = new();
			_commentLabel = new();

			_mainFlowPanel.SuspendLayout();
			_flowPanel.SuspendLayout();


			_mainFlowPanel.Controls.Add(_flowPanel);

			_flowPanel.AutoSize = true;
			_flowPanel.Controls.Add(_nameLabel);
			_flowPanel.Controls.Add(_commentLabel);
			_flowPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			_flowPanel.Location = new System.Drawing.Point(3, 3);
			_flowPanel.Name = "flowLayoutPanel4";
			_flowPanel.Size = new System.Drawing.Size(41, 26);
			_flowPanel.TabIndex = 0;
			//_flowPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));


			// 
			// label4
			// 
			_nameLabel.AutoSize = true;
			_nameLabel.Location = new System.Drawing.Point(3, 0);
			_nameLabel.Name = "label4";
			_nameLabel.Size = new System.Drawing.Size(35, 13);
			_nameLabel.TabIndex = 0;
			_nameLabel.Text = name;
			_nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			_nameLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
			_nameLabel.Click += GoToProfile;



			// 
			// label5
			// 
			_commentLabel.AutoSize = true;
			_commentLabel.Location = new System.Drawing.Point(3, 13);
			_commentLabel.Name = "label5";
			_commentLabel.Size = new System.Drawing.Size(35, 13);
			_commentLabel.TabIndex = 1;
			_commentLabel.Text = comment;
			_commentLabel.Click += Delete;

			SetSize();

			_flowPanel.ResumeLayout(false);
			_mainFlowPanel.ResumeLayout(false);

			_flowPanel.Visible = false;
			_flowPanel.Visible = true;

			_mainFlowPanel.Update();

			_mainFlowPanel.Resize += OnResized;
		}
		public void Deatach()
		{
			_mainFlowPanel.Controls.Remove(_flowPanel);
		}

		private void SetSize()
		{
			_commentLabel.MaximumSize = _nameLabel.MaximumSize = new System.Drawing.Size(_mainFlowPanel.Size.Width - 25, 200);

			_flowPanel.MinimumSize = new System.Drawing.Size(_mainFlowPanel.Size.Width - 25, 0);
		}

		private void OnResized(object sender, EventArgs e)
		{
			SetSize();
		}
		private void Delete(object sender, EventArgs e) 
		{
			if (((MouseEventArgs)e).Button == MouseButtons.Middle)
				if (_main.is_admin || _userId == _main.myUserId)
				{
					if (MessageBox.Show("Ви дійсно хочете видалити цей коментар?\nТю дію буде неможливо відминити!",
						"Видалення коментаря", MessageBoxButtons.OKCancel) == DialogResult.OK)
						_main.DeleteComment(_commentId);
				}
				else
					MessageBox.Show("Ви не можете видалити чужий коментар");
		}

		private void GoToProfile(object sender, EventArgs e)
		{
			_main.InitProfileViewer(_userId);
		}
	}
}
