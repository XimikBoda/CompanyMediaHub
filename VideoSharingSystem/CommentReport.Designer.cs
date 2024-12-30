namespace VideoSharingSystem
{
	partial class CommentReport
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.sendButton = new System.Windows.Forms.Button();
			this.canselButton = new System.Windows.Forms.Button();
			this.textRichTextBox = new System.Windows.Forms.RichTextBox();
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.reportContentRichTextBox = new System.Windows.Forms.RichTextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// sendButton
			// 
			this.sendButton.Location = new System.Drawing.Point(443, 222);
			this.sendButton.Name = "sendButton";
			this.sendButton.Size = new System.Drawing.Size(96, 23);
			this.sendButton.TabIndex = 20;
			this.sendButton.Text = "Відправити";
			this.sendButton.UseVisualStyleBackColor = true;
			this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
			// 
			// canselButton
			// 
			this.canselButton.Location = new System.Drawing.Point(545, 222);
			this.canselButton.Name = "canselButton";
			this.canselButton.Size = new System.Drawing.Size(96, 23);
			this.canselButton.TabIndex = 19;
			this.canselButton.Text = "Скасувати";
			this.canselButton.UseVisualStyleBackColor = true;
			this.canselButton.Click += new System.EventHandler(this.canselButton_Click);
			// 
			// textRichTextBox
			// 
			this.textRichTextBox.Location = new System.Drawing.Point(12, 64);
			this.textRichTextBox.MaxLength = 500;
			this.textRichTextBox.Name = "textRichTextBox";
			this.textRichTextBox.ReadOnly = true;
			this.textRichTextBox.Size = new System.Drawing.Size(311, 181);
			this.textRichTextBox.TabIndex = 18;
			this.textRichTextBox.Text = "";
			// 
			// nameTextBox
			// 
			this.nameTextBox.Location = new System.Drawing.Point(12, 25);
			this.nameTextBox.MaxLength = 40;
			this.nameTextBox.Name = "nameTextBox";
			this.nameTextBox.ReadOnly = true;
			this.nameTextBox.Size = new System.Drawing.Size(312, 20);
			this.nameTextBox.TabIndex = 17;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(96, 13);
			this.label2.TabIndex = 16;
			this.label2.Text = "Зміст коментаря:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(125, 13);
			this.label1.TabIndex = 15;
			this.label1.Text = "Ім\'я автора коментаря:";
			// 
			// reportContentRichTextBox
			// 
			this.reportContentRichTextBox.Location = new System.Drawing.Point(330, 25);
			this.reportContentRichTextBox.MaxLength = 500;
			this.reportContentRichTextBox.Name = "reportContentRichTextBox";
			this.reportContentRichTextBox.Size = new System.Drawing.Size(311, 191);
			this.reportContentRichTextBox.TabIndex = 22;
			this.reportContentRichTextBox.Text = "";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(330, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(91, 13);
			this.label3.TabIndex = 21;
			this.label3.Text = "Причина скарги:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// CommentReport
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(648, 253);
			this.Controls.Add(this.reportContentRichTextBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.sendButton);
			this.Controls.Add(this.canselButton);
			this.Controls.Add(this.textRichTextBox);
			this.Controls.Add(this.nameTextBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CommentReport";
			this.Text = "Створення скарги";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button sendButton;
		private System.Windows.Forms.Button canselButton;
		private System.Windows.Forms.RichTextBox textRichTextBox;
		private System.Windows.Forms.TextBox nameTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RichTextBox reportContentRichTextBox;
		private System.Windows.Forms.Label label3;
	}
}