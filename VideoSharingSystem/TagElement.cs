using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoSharingSystem
{
	internal class TagElement
	{
		Form1 _main;

		FlowLayoutPanel _mainFlowPanel;

		FlowLayoutPanel _flowPanel;
		Label _nameLabel;
		Label _commentLabel;

		public TagElement(FlowLayoutPanel mainFlowPanel, Form1 main, string name)
		{
			_mainFlowPanel = mainFlowPanel;
			_main = main;

			_flowPanel = new();
			_nameLabel = new();

			_mainFlowPanel.SuspendLayout();
			_flowPanel.SuspendLayout();


			_mainFlowPanel.Controls.Add(_flowPanel);

			_flowPanel.AutoSize = true;
			_flowPanel.Controls.Add(_nameLabel);
			_flowPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			_flowPanel.Location = new System.Drawing.Point(3, 3);
			_flowPanel.Name = "flowLayoutPanel4";
			_flowPanel.Size = new System.Drawing.Size(41, 26);
			_flowPanel.TabIndex = 0;
			//_flowPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			_flowPanel.BackColor = System.Drawing.Color.FromArgb(0, 0, 0);


			// 
			// label4
			// 
			_nameLabel.AutoSize = true;
			_nameLabel.Location = new System.Drawing.Point(3, 0);
			_nameLabel.Name = "label4";
			_nameLabel.Size = new System.Drawing.Size(35, 13);
			_nameLabel.TabIndex = 0;
			_nameLabel.Text = name;
			_nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, 0, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			_nameLabel.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
			//_nameLabel.ForeColor = System.Drawing.SystemColors.HotTrack;


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
			//_commentLabel.MaximumSize = _nameLabel.MaximumSize = new System.Drawing.Size(_mainFlowPanel.Size.Width - 25, 200);

			//_flowPanel.MinimumSize = new System.Drawing.Size(_mainFlowPanel.Size.Width - 25, 0);
		}

		private void OnResized(object sender, EventArgs e)
		{
			SetSize();
		}
		private void Delete(object sender, EventArgs e)
		{
		}

	}
}
