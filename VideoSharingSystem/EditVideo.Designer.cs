
namespace VideoSharingSystem
{
	partial class EditVideo
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditVideo));
			this.saveButton = new System.Windows.Forms.Button();
			this.canselButton = new System.Windows.Forms.Button();
			this.descriptionRichTextBox = new System.Windows.Forms.RichTextBox();
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.deleteButton = new System.Windows.Forms.Button();
			this.SelectImageButton = new System.Windows.Forms.Button();
			this.label10 = new System.Windows.Forms.Label();
			this.stopFrameButton = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label4 = new System.Windows.Forms.Label();
			this.TagsCheckedListBox = new System.Windows.Forms.CheckedListBox();
			this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
			this.SuspendLayout();
			// 
			// saveButton
			// 
			this.saveButton.Location = new System.Drawing.Point(646, 279);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(96, 23);
			this.saveButton.TabIndex = 14;
			this.saveButton.Text = "Застосувати";
			this.saveButton.UseVisualStyleBackColor = true;
			this.saveButton.Click += new System.EventHandler(this.button3_Click);
			// 
			// canselButton
			// 
			this.canselButton.Location = new System.Drawing.Point(748, 279);
			this.canselButton.Name = "canselButton";
			this.canselButton.Size = new System.Drawing.Size(96, 23);
			this.canselButton.TabIndex = 13;
			this.canselButton.Text = "Скасувати";
			this.canselButton.UseVisualStyleBackColor = true;
			this.canselButton.Click += new System.EventHandler(this.button2_Click);
			// 
			// descriptionRichTextBox
			// 
			this.descriptionRichTextBox.Location = new System.Drawing.Point(57, 53);
			this.descriptionRichTextBox.MaxLength = 500;
			this.descriptionRichTextBox.Name = "descriptionRichTextBox";
			this.descriptionRichTextBox.Size = new System.Drawing.Size(311, 220);
			this.descriptionRichTextBox.TabIndex = 12;
			this.descriptionRichTextBox.Text = "";
			// 
			// nameTextBox
			// 
			this.nameTextBox.Location = new System.Drawing.Point(56, 18);
			this.nameTextBox.MaxLength = 40;
			this.nameTextBox.Name = "nameTextBox";
			this.nameTextBox.Size = new System.Drawing.Size(312, 20);
			this.nameTextBox.TabIndex = 11;
			this.nameTextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(14, 55);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(36, 13);
			this.label2.TabIndex = 10;
			this.label2.Text = "Опис:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(42, 13);
			this.label1.TabIndex = 9;
			this.label1.Text = "Назва:";
			// 
			// deleteButton
			// 
			this.deleteButton.BackColor = System.Drawing.Color.Red;
			this.deleteButton.ForeColor = System.Drawing.SystemColors.Control;
			this.deleteButton.Location = new System.Drawing.Point(56, 279);
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.Size = new System.Drawing.Size(96, 23);
			this.deleteButton.TabIndex = 15;
			this.deleteButton.Text = "Видалити";
			this.deleteButton.UseVisualStyleBackColor = false;
			this.deleteButton.Click += new System.EventHandler(this.button1_Click);
			// 
			// SelectImageButton
			// 
			this.SelectImageButton.Location = new System.Drawing.Point(514, 211);
			this.SelectImageButton.Name = "SelectImageButton";
			this.SelectImageButton.Size = new System.Drawing.Size(75, 20);
			this.SelectImageButton.TabIndex = 22;
			this.SelectImageButton.Text = "Огляд";
			this.SelectImageButton.UseVisualStyleBackColor = true;
			this.SelectImageButton.Click += new System.EventHandler(this.SelectImageButton_Click);
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(385, 166);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(50, 13);
			this.label10.TabIndex = 21;
			this.label10.Text = "Превью:";
			this.label10.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// stopFrameButton
			// 
			this.stopFrameButton.Location = new System.Drawing.Point(514, 182);
			this.stopFrameButton.Name = "stopFrameButton";
			this.stopFrameButton.Size = new System.Drawing.Size(75, 23);
			this.stopFrameButton.TabIndex = 20;
			this.stopFrameButton.Text = "Стоп-кадр";
			this.stopFrameButton.UseVisualStyleBackColor = true;
			this.stopFrameButton.Click += new System.EventHandler(this.stopFrameButton_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(388, 182);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(120, 120);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 19;
			this.pictureBox1.TabStop = false;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(385, 18);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(34, 13);
			this.label4.TabIndex = 18;
			this.label4.Text = "Теги:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// TagsCheckedListBox
			// 
			this.TagsCheckedListBox.CheckOnClick = true;
			this.TagsCheckedListBox.FormattingEnabled = true;
			this.TagsCheckedListBox.Location = new System.Drawing.Point(388, 39);
			this.TagsCheckedListBox.Name = "TagsCheckedListBox";
			this.TagsCheckedListBox.Size = new System.Drawing.Size(201, 124);
			this.TagsCheckedListBox.TabIndex = 17;
			// 
			// axWindowsMediaPlayer1
			// 
			this.axWindowsMediaPlayer1.Enabled = true;
			this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(595, 18);
			this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
			this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
			this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(249, 255);
			this.axWindowsMediaPlayer1.TabIndex = 16;
			// 
			// EditVideo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(855, 313);
			this.Controls.Add(this.SelectImageButton);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.stopFrameButton);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.TagsCheckedListBox);
			this.Controls.Add(this.axWindowsMediaPlayer1);
			this.Controls.Add(this.deleteButton);
			this.Controls.Add(this.saveButton);
			this.Controls.Add(this.canselButton);
			this.Controls.Add(this.descriptionRichTextBox);
			this.Controls.Add(this.nameTextBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EditVideo_FormClosed);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EditVideo";
			this.Text = "Редагування інформації про відео";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.Button canselButton;
		private System.Windows.Forms.RichTextBox descriptionRichTextBox;
		private System.Windows.Forms.TextBox nameTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button deleteButton;
		private System.Windows.Forms.Button SelectImageButton;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Button stopFrameButton;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckedListBox TagsCheckedListBox;
		private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
	}
}