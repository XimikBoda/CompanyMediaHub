
namespace VideoSharingSystem
{
	partial class EditUser
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.newPassTextBox = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.oldPassTextBox = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.patronymicTextBox = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.surnameTextBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.loginTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.emailTextBox = new System.Windows.Forms.TextBox();
			this.birthDateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.deleteButton = new System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.45033F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.54967F));
			this.tableLayoutPanel1.Controls.Add(this.newPassTextBox, 1, 7);
			this.tableLayoutPanel1.Controls.Add(this.label9, 0, 7);
			this.tableLayoutPanel1.Controls.Add(this.oldPassTextBox, 1, 6);
			this.tableLayoutPanel1.Controls.Add(this.label8, 0, 6);
			this.tableLayoutPanel1.Controls.Add(this.label6, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.patronymicTextBox, 1, 4);
			this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.surnameTextBox, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.nameTextBox, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.loginTextBox, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.emailTextBox, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.birthDateTimePicker, 1, 5);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 16);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 8;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.39604F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.38614F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(453, 173);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// newPassTextBox
			// 
			this.newPassTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.newPassTextBox.Location = new System.Drawing.Point(149, 150);
			this.newPassTextBox.MaxLength = 30;
			this.newPassTextBox.Name = "newPassTextBox";
			this.newPassTextBox.PasswordChar = '*';
			this.newPassTextBox.Size = new System.Drawing.Size(301, 20);
			this.newPassTextBox.TabIndex = 17;
			this.newPassTextBox.TextChanged += new System.EventHandler(this.emailTextBox_TextChanged);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Dock = System.Windows.Forms.DockStyle.Right;
			this.label9.Location = new System.Drawing.Point(62, 147);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(81, 26);
			this.label9.TabIndex = 16;
			this.label9.Text = "Новий пароль:";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oldPassTextBox
			// 
			this.oldPassTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.oldPassTextBox.Location = new System.Drawing.Point(149, 129);
			this.oldPassTextBox.MaxLength = 30;
			this.oldPassTextBox.Name = "oldPassTextBox";
			this.oldPassTextBox.PasswordChar = '*';
			this.oldPassTextBox.Size = new System.Drawing.Size(301, 20);
			this.oldPassTextBox.TabIndex = 15;
			this.oldPassTextBox.TextChanged += new System.EventHandler(this.emailTextBox_TextChanged);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Dock = System.Windows.Forms.DockStyle.Right;
			this.label8.Location = new System.Drawing.Point(58, 126);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(85, 21);
			this.label8.TabIndex = 14;
			this.label8.Text = "Старий пароль:";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Dock = System.Windows.Forms.DockStyle.Right;
			this.label6.Location = new System.Drawing.Point(42, 104);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(101, 22);
			this.label6.TabIndex = 10;
			this.label6.Text = "Дата народження:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// patronymicTextBox
			// 
			this.patronymicTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.patronymicTextBox.Location = new System.Drawing.Point(149, 87);
			this.patronymicTextBox.MaxLength = 30;
			this.patronymicTextBox.Name = "patronymicTextBox";
			this.patronymicTextBox.Size = new System.Drawing.Size(301, 20);
			this.patronymicTextBox.TabIndex = 9;
			this.patronymicTextBox.TextChanged += new System.EventHandler(this.emailTextBox_TextChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Dock = System.Windows.Forms.DockStyle.Right;
			this.label5.Location = new System.Drawing.Point(73, 84);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(70, 20);
			this.label5.TabIndex = 8;
			this.label5.Text = "По батькові:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// surnameTextBox
			// 
			this.surnameTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.surnameTextBox.Location = new System.Drawing.Point(149, 66);
			this.surnameTextBox.MaxLength = 30;
			this.surnameTextBox.Name = "surnameTextBox";
			this.surnameTextBox.Size = new System.Drawing.Size(301, 20);
			this.surnameTextBox.TabIndex = 7;
			this.surnameTextBox.TextChanged += new System.EventHandler(this.emailTextBox_TextChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Dock = System.Windows.Forms.DockStyle.Right;
			this.label4.Location = new System.Drawing.Point(84, 63);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(59, 21);
			this.label4.TabIndex = 6;
			this.label4.Text = "Прізвище:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// nameTextBox
			// 
			this.nameTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.nameTextBox.Location = new System.Drawing.Point(149, 45);
			this.nameTextBox.MaxLength = 30;
			this.nameTextBox.Name = "nameTextBox";
			this.nameTextBox.Size = new System.Drawing.Size(301, 20);
			this.nameTextBox.TabIndex = 5;
			this.nameTextBox.TextChanged += new System.EventHandler(this.emailTextBox_TextChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Dock = System.Windows.Forms.DockStyle.Right;
			this.label3.Location = new System.Drawing.Point(114, 42);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(29, 21);
			this.label3.TabIndex = 4;
			this.label3.Text = "Ім\'я:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// loginTextBox
			// 
			this.loginTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.loginTextBox.Location = new System.Drawing.Point(149, 24);
			this.loginTextBox.MaxLength = 20;
			this.loginTextBox.Name = "loginTextBox";
			this.loginTextBox.Size = new System.Drawing.Size(301, 20);
			this.loginTextBox.TabIndex = 3;
			this.loginTextBox.TextChanged += new System.EventHandler(this.emailTextBox_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Right;
			this.label2.Location = new System.Drawing.Point(106, 21);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(37, 21);
			this.label2.TabIndex = 2;
			this.label2.Text = "Логін:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Right;
			this.label1.Location = new System.Drawing.Point(108, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 21);
			this.label1.TabIndex = 0;
			this.label1.Text = "Email:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// emailTextBox
			// 
			this.emailTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.emailTextBox.Location = new System.Drawing.Point(149, 3);
			this.emailTextBox.MaxLength = 30;
			this.emailTextBox.Name = "emailTextBox";
			this.emailTextBox.Size = new System.Drawing.Size(301, 20);
			this.emailTextBox.TabIndex = 1;
			this.emailTextBox.TextChanged += new System.EventHandler(this.emailTextBox_TextChanged);
			// 
			// birthDateTimePicker
			// 
			this.birthDateTimePicker.CustomFormat = "yyyy-MM-dd";
			this.birthDateTimePicker.Location = new System.Drawing.Point(149, 107);
			this.birthDateTimePicker.Name = "birthDateTimePicker";
			this.birthDateTimePicker.Size = new System.Drawing.Size(300, 20);
			this.birthDateTimePicker.TabIndex = 11;
			this.birthDateTimePicker.Value = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
			this.birthDateTimePicker.ValueChanged += new System.EventHandler(this.birthDateTimePicker_ValueChanged);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(484, 16);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(86, 23);
			this.button1.TabIndex = 3;
			this.button1.Text = "Зберегти";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(484, 46);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(86, 23);
			this.button2.TabIndex = 4;
			this.button2.Text = "Відмінити";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// deleteButton
			// 
			this.deleteButton.BackColor = System.Drawing.Color.Red;
			this.deleteButton.ForeColor = System.Drawing.SystemColors.Control;
			this.deleteButton.Location = new System.Drawing.Point(12, 195);
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.Size = new System.Drawing.Size(96, 23);
			this.deleteButton.TabIndex = 16;
			this.deleteButton.Text = "Видалити";
			this.deleteButton.UseVisualStyleBackColor = false;
			this.deleteButton.Click += new System.EventHandler(this.button3_Click);
			// 
			// EditUser
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(583, 225);
			this.Controls.Add(this.deleteButton);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EditUser";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Редагування користувача";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TextBox newPassTextBox;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox oldPassTextBox;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox patronymicTextBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox surnameTextBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox nameTextBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox loginTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox emailTextBox;
		private System.Windows.Forms.DateTimePicker birthDateTimePicker;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button deleteButton;
	}
}