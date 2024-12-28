﻿namespace VideoSharingSystem
{
	partial class EditCompany
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
			this.SelectImageButton = new System.Windows.Forms.Button();
			this.label10 = new System.Windows.Forms.Label();
			this.logoPictureBox = new System.Windows.Forms.PictureBox();
			this.descriptionRichTextBox = new System.Windows.Forms.RichTextBox();
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.ModeratorsCheckedListBox = new System.Windows.Forms.CheckedListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.button1 = new System.Windows.Forms.Button();
			this.addModeratorButton = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.saveButton = new System.Windows.Forms.Button();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			this.SuspendLayout();
			// 
			// SelectImageButton
			// 
			this.SelectImageButton.Location = new System.Drawing.Point(256, 190);
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
			this.label10.Location = new System.Drawing.Point(253, 47);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(50, 13);
			this.label10.TabIndex = 21;
			this.label10.Text = "Превью:";
			this.label10.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// logoPictureBox
			// 
			this.logoPictureBox.Location = new System.Drawing.Point(256, 64);
			this.logoPictureBox.Name = "logoPictureBox";
			this.logoPictureBox.Size = new System.Drawing.Size(120, 120);
			this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.logoPictureBox.TabIndex = 20;
			this.logoPictureBox.TabStop = false;
			// 
			// descriptionRichTextBox
			// 
			this.descriptionRichTextBox.Location = new System.Drawing.Point(13, 64);
			this.descriptionRichTextBox.MaxLength = 500;
			this.descriptionRichTextBox.Name = "descriptionRichTextBox";
			this.descriptionRichTextBox.Size = new System.Drawing.Size(238, 353);
			this.descriptionRichTextBox.TabIndex = 19;
			this.descriptionRichTextBox.Text = "";
			this.descriptionRichTextBox.TextChanged += new System.EventHandler(this.descriptionRichTextBox_TextChanged);
			// 
			// nameTextBox
			// 
			this.nameTextBox.Location = new System.Drawing.Point(52, 12);
			this.nameTextBox.MaxLength = 40;
			this.nameTextBox.Name = "nameTextBox";
			this.nameTextBox.Size = new System.Drawing.Size(324, 20);
			this.nameTextBox.TabIndex = 18;
			this.nameTextBox.TextChanged += new System.EventHandler(this.nameTextBox_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 47);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(36, 13);
			this.label2.TabIndex = 17;
			this.label2.Text = "Опис:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(10, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(42, 13);
			this.label1.TabIndex = 16;
			this.label1.Text = "Назва:";
			// 
			// ModeratorsCheckedListBox
			// 
			this.ModeratorsCheckedListBox.FormattingEnabled = true;
			this.ModeratorsCheckedListBox.Location = new System.Drawing.Point(405, 33);
			this.ModeratorsCheckedListBox.Name = "ModeratorsCheckedListBox";
			this.ModeratorsCheckedListBox.Size = new System.Drawing.Size(201, 289);
			this.ModeratorsCheckedListBox.TabIndex = 23;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(405, 14);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(72, 13);
			this.label3.TabIndex = 24;
			this.label3.Text = "Модератори:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(402, 353);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(112, 13);
			this.label4.TabIndex = 25;
			this.label4.Text = "Додати модератора:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(405, 369);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(198, 21);
			this.comboBox1.TabIndex = 26;
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.button1.Location = new System.Drawing.Point(402, 327);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(201, 23);
			this.button1.TabIndex = 27;
			this.button1.Text = "Видалити обраних";
			this.button1.UseVisualStyleBackColor = false;
			// 
			// addModeratorButton
			// 
			this.addModeratorButton.Location = new System.Drawing.Point(405, 396);
			this.addModeratorButton.Name = "addModeratorButton";
			this.addModeratorButton.Size = new System.Drawing.Size(75, 20);
			this.addModeratorButton.TabIndex = 28;
			this.addModeratorButton.Text = "Додати";
			this.addModeratorButton.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(257, 390);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(86, 23);
			this.button2.TabIndex = 30;
			this.button2.Text = "Відминити";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// saveButton
			// 
			this.saveButton.Enabled = false;
			this.saveButton.Location = new System.Drawing.Point(257, 361);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(86, 23);
			this.saveButton.TabIndex = 29;
			this.saveButton.Text = "Зберегти";
			this.saveButton.UseVisualStyleBackColor = true;
			this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
			// 
			// pictureBox2
			// 
			this.pictureBox2.BackColor = System.Drawing.Color.Black;
			this.pictureBox2.Location = new System.Drawing.Point(388, -3);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(2, 429);
			this.pictureBox2.TabIndex = 31;
			this.pictureBox2.TabStop = false;
			// 
			// EditCompany
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(618, 425);
			this.Controls.Add(this.pictureBox2);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.saveButton);
			this.Controls.Add(this.addModeratorButton);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.ModeratorsCheckedListBox);
			this.Controls.Add(this.SelectImageButton);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.logoPictureBox);
			this.Controls.Add(this.descriptionRichTextBox);
			this.Controls.Add(this.nameTextBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "EditCompany";
			this.Text = "EditCompany";
			((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button SelectImageButton;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.PictureBox logoPictureBox;
		private System.Windows.Forms.RichTextBox descriptionRichTextBox;
		private System.Windows.Forms.TextBox nameTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckedListBox ModeratorsCheckedListBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button addModeratorButton;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.PictureBox pictureBox2;
	}
}