
using System.Drawing;
using System.Windows.Forms;

namespace VideoSharingSystem
{
	//public class TransparentBackgroundTextBox : TextBox
	//{
	//	public TransparentBackgroundTextBox()
	//	{

	//		SetStyle(ControlStyles.SupportsTransparentBackColor |
	//				 ControlStyles.OptimizedDoubleBuffer |
	//				 ControlStyles.AllPaintingInWmPaint |
	//				 ControlStyles.ResizeRedraw |
	//				 ControlStyles.UserPaint, true);
	//		BackColor = Color.Transparent;
	//	}

	//	public sealed override Color BackColor
	//	{
	//		get => base.BackColor;
	//		set => base.BackColor = value;
	//	}
	//}

	partial class AddVideo
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddVideo));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.descriptionRichTextBox = new System.Windows.Forms.RichTextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.selectButton = new System.Windows.Forms.Button();
			this.CancelButton = new System.Windows.Forms.Button();
			this.UploadButton = new System.Windows.Forms.Button();
			this.TagsCheckedListBox = new System.Windows.Forms.CheckedListBox();
			this.label4 = new System.Windows.Forms.Label();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.stopFrameButton = new System.Windows.Forms.Button();
			this.label10 = new System.Windows.Forms.Label();
			this.SelectImageButton = new System.Windows.Forms.Button();
			this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(15, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(42, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Назва:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(21, 51);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(36, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Опис:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// nameTextBox
			// 
			this.nameTextBox.Location = new System.Drawing.Point(63, 14);
			this.nameTextBox.MaxLength = 40;
			this.nameTextBox.Name = "nameTextBox";
			this.nameTextBox.Size = new System.Drawing.Size(455, 20);
			this.nameTextBox.TabIndex = 2;
			// 
			// descriptionRichTextBox
			// 
			this.descriptionRichTextBox.Location = new System.Drawing.Point(64, 49);
			this.descriptionRichTextBox.MaxLength = 500;
			this.descriptionRichTextBox.Name = "descriptionRichTextBox";
			this.descriptionRichTextBox.Size = new System.Drawing.Size(198, 284);
			this.descriptionRichTextBox.TabIndex = 3;
			this.descriptionRichTextBox.Text = "";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(527, 17);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(78, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Оберіть файл:";
			// 
			// selectButton
			// 
			this.selectButton.Location = new System.Drawing.Point(618, 14);
			this.selectButton.Name = "selectButton";
			this.selectButton.Size = new System.Drawing.Size(121, 20);
			this.selectButton.TabIndex = 6;
			this.selectButton.Text = "Огляд";
			this.selectButton.UseVisualStyleBackColor = true;
			this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
			// 
			// CancelButton
			// 
			this.CancelButton.Location = new System.Drawing.Point(618, 310);
			this.CancelButton.Name = "CancelButton";
			this.CancelButton.Size = new System.Drawing.Size(121, 23);
			this.CancelButton.TabIndex = 7;
			this.CancelButton.Text = "Скасувати";
			this.CancelButton.UseVisualStyleBackColor = true;
			this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
			// 
			// UploadButton
			// 
			this.UploadButton.Enabled = false;
			this.UploadButton.Location = new System.Drawing.Point(490, 310);
			this.UploadButton.Name = "UploadButton";
			this.UploadButton.Size = new System.Drawing.Size(115, 23);
			this.UploadButton.TabIndex = 8;
			this.UploadButton.Text = "Завантажити";
			this.UploadButton.UseVisualStyleBackColor = true;
			this.UploadButton.Click += new System.EventHandler(this.UploadButton_Click);
			// 
			// TagsCheckedListBox
			// 
			this.TagsCheckedListBox.CheckOnClick = true;
			this.TagsCheckedListBox.FormattingEnabled = true;
			this.TagsCheckedListBox.Location = new System.Drawing.Point(283, 70);
			this.TagsCheckedListBox.Name = "TagsCheckedListBox";
			this.TagsCheckedListBox.Size = new System.Drawing.Size(201, 124);
			this.TagsCheckedListBox.TabIndex = 9;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(280, 49);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(34, 13);
			this.label4.TabIndex = 10;
			this.label4.Text = "Теги:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(63, 32);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(455, 11);
			this.progressBar1.TabIndex = 11;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(283, 213);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(120, 120);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 12;
			this.pictureBox1.TabStop = false;
			// 
			// stopFrameButton
			// 
			this.stopFrameButton.Location = new System.Drawing.Point(409, 213);
			this.stopFrameButton.Name = "stopFrameButton";
			this.stopFrameButton.Size = new System.Drawing.Size(75, 23);
			this.stopFrameButton.TabIndex = 13;
			this.stopFrameButton.Text = "Стоп-кадр";
			this.stopFrameButton.UseVisualStyleBackColor = true;
			this.stopFrameButton.Click += new System.EventHandler(this.button1_Click);
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(280, 197);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(50, 13);
			this.label10.TabIndex = 14;
			this.label10.Text = "Превью:";
			this.label10.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// SelectImageButton
			// 
			this.SelectImageButton.Location = new System.Drawing.Point(409, 242);
			this.SelectImageButton.Name = "SelectImageButton";
			this.SelectImageButton.Size = new System.Drawing.Size(75, 20);
			this.SelectImageButton.TabIndex = 15;
			this.SelectImageButton.Text = "Огляд";
			this.SelectImageButton.UseVisualStyleBackColor = true;
			this.SelectImageButton.Click += new System.EventHandler(this.SelectImageButton_Click);
			// 
			// axWindowsMediaPlayer1
			// 
			this.axWindowsMediaPlayer1.Enabled = true;
			this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(490, 49);
			this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
			this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
			this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(249, 255);
			this.axWindowsMediaPlayer1.TabIndex = 5;
			this.axWindowsMediaPlayer1.Visible = false;
			// 
			// AddVideo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(752, 345);
			this.Controls.Add(this.SelectImageButton);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.stopFrameButton);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.TagsCheckedListBox);
			this.Controls.Add(this.UploadButton);
			this.Controls.Add(this.CancelButton);
			this.Controls.Add(this.selectButton);
			this.Controls.Add(this.axWindowsMediaPlayer1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.descriptionRichTextBox);
			this.Controls.Add(this.nameTextBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.progressBar1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AddVideo";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Завантажити відео";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AddVideo_FormClosed);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox nameTextBox;
		private System.Windows.Forms.RichTextBox descriptionRichTextBox;
		private System.Windows.Forms.Label label3;
		private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
		private System.Windows.Forms.Button selectButton;
		private System.Windows.Forms.Button CancelButton;
		private System.Windows.Forms.Button UploadButton;
		private System.Windows.Forms.CheckedListBox TagsCheckedListBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ProgressBar progressBar1;
		private PictureBox pictureBox1;
		private Button stopFrameButton;
		private Label label10;
		private Button SelectImageButton;
	}
}