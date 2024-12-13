using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoSharingSystem
{
	public partial class EditUser : Form
	{
		Form1 _mainForm;
		int _userId;
		bool _isAdmin;
		public EditUser(Form1 mainForm, int userId, bool isAdmin)
		{
			_mainForm = mainForm;
			_userId = userId;
			_isAdmin = isAdmin;
			InitializeComponent();

			//using (SqlConnection connection = new SqlConnection(_mainForm.connectionString))
			//{
			//	SqlCommand command = new SqlCommand(
			//		"SELECT Email, LoginUser, NameUser, Surname, Patronymic, Birthday, About, IsAdmin FROM Users WHERE IdUser = @id ", connection);
			//	command.Parameters.AddWithValue("@id", _userId);

			//	try
			//	{
			//		connection.Open();
			//		{
			//			SqlDataReader reader = command.ExecuteReader();
			//			if (reader.Read())
			//			{
			//				textBox1.Text = reader[0].ToString();
			//				textBox2.Text = reader[1].ToString();
			//				textBox3.Text = reader[2].ToString();
			//				textBox4.Text = reader[3].ToString();
			//				textBox5.Text = reader[4].ToString();
			//				dateTimePicker1.Value = (DateTime)reader[5];
			//				richTextBox1.Text = reader[6].ToString();
			//				checkBox1.Checked = reader.GetBoolean(7);
			//				checkBox1.Enabled = isAdmin;
			//			}
			//			else
			//				Close();
			//			reader.Close();
			//		}
			//	}
			//	catch (Exception ex)
			//	{
			//		Console.WriteLine(ex.Message);
			//		MessageBox.Show(ex.Message);
			//		Close();
			//	}
			//}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (textBox6.Text == "" && textBox7.Text != "")
			{
				MessageBox.Show("Введіть старий пароль");
				return;
			}
			if (textBox6.Text != "" && textBox7.Text == "")
			{
				MessageBox.Show("Введіть новий пароль");
				return;
			}
			//if (textBox6.Text != "" && textBox7.Text != "")
				//using (SqlConnection connection = new SqlConnection(_mainForm.connectionString))
				//{
				//	SqlCommand command = new SqlCommand("SetNewPassword", connection);
				//	command.CommandType = CommandType.StoredProcedure;
				//	command.Parameters.Add("@IdUser", SqlDbType.Int);
				//	command.Parameters.Add("@OldPassword", SqlDbType.VarChar, 20);
				//	command.Parameters.Add("@NewPassword", SqlDbType.VarChar, 30);

				//	command.Parameters["@IdUser"].Value = _userId;
				//	command.Parameters["@OldPassword"].Value = textBox6.Text;
				//	command.Parameters["@NewPassword"].Value = textBox7.Text;
				//	try
				//	{
				//		connection.Open();
				//		if (command.ExecuteScalar().ToString() == "1")
				//		{
				//			MessageBox.Show("Пароль оновленно");
				//			textBox6.Text = textBox7.Text = "";
				//		}
				//		else
				//			MessageBox.Show("Старий пароль не вірний");
				//		Close();
				//	}
				//	catch (Exception ex)
				//	{
				//		Console.WriteLine(ex.Message);
				//		MessageBox.Show("Пароль не було оновленно, можливо невірний старий пароль" + ex.Message);
				//		return;
				//	}
				//}
			//using (SqlConnection connection = new SqlConnection(_mainForm.connectionString))
			//{
			//	SqlCommand command = new SqlCommand("UPDATE Users SET Email = @Email, LoginUser = @LoginUser, NameUser = @NameUser, " +
			//		"Surname = @Surname, Patronymic = @Patronymic, Birthday  = @Birthday, About = @About, IsAdmin = @IsAdmin " +
			//		"WHERE IdUser = @IdUser", connection);
			//	command.Parameters.Add("@Email", SqlDbType.VarChar, 30);
			//	command.Parameters.Add("@LoginUser", SqlDbType.VarChar, 20);
			//	command.Parameters.Add("@NameUser", SqlDbType.VarChar, 30);
			//	command.Parameters.Add("@Surname", SqlDbType.VarChar, 30);
			//	command.Parameters.Add("@Patronymic", SqlDbType.VarChar, 30);
			//	command.Parameters.Add("@Birthday", SqlDbType.Date);
			//	command.Parameters.Add("@About", SqlDbType.VarChar, 500);
			//	command.Parameters.Add("@IsAdmin", SqlDbType.Bit);
			//	command.Parameters.Add("@IdUser", SqlDbType.Int);

			//	command.Parameters["@Email"].Value = textBox1.Text;
			//	command.Parameters["@LoginUser"].Value = textBox2.Text;
			//	command.Parameters["@NameUser"].Value = textBox3.Text;
			//	command.Parameters["@Surname"].Value = textBox4.Text;
			//	command.Parameters["@Patronymic"].Value = textBox5.Text;
			//	command.Parameters["@Birthday"].Value = dateTimePicker1.Value;
			//	command.Parameters["@About"].Value = richTextBox1.Text;
			//	command.Parameters["@IsAdmin"].Value = checkBox1.Checked;
			//	command.Parameters["@IdUser"].Value = _userId;
			//	try
			//	{
			//		connection.Open();
			//		command.ExecuteNonQuery();
			//		Close();
			//	}
			//	catch (Exception ex)
			//	{
			//		Console.WriteLine(ex.Message);
			//		MessageBox.Show(ex.Message);
			//		return;
			//	}
			//}

		}

		private void button3_Click(object sender, EventArgs e)
		{
			var result = MessageBox.Show("Ви впевненні, що хочете видалити цей профіль?\nВидалиться вся інформація пов'язанна з профілем (завантаженні відео, коментарі).\nЦю дію буде неможливо відминити!",
				"Видалення профілю", MessageBoxButtons.OKCancel);

			if (result == DialogResult.OK)
			{
				//using (SqlConnection connection = new SqlConnection(_mainForm.connectionString))
				//{
				//	SqlCommand command = new SqlCommand("Delete FROM Users WHERE IdUser = @IdUser", connection);
				//	command.Parameters.Add("@IdUser", SqlDbType.Int);

				//	command.Parameters["@IdUser"].Value = _userId;
				//	try
				//	{
				//		connection.Open();
				//		command.ExecuteNonQuery();
				//		MessageBox.Show("Профіль видаленно");
				//		Close();
				//		if (_userId == _mainForm.myUserId)
				//			_mainForm.Close();
				//		else
				//		{
				//			_mainForm.InitProfileViewer(_mainForm.myUserId);
				//		}
				//	}
				//	catch (Exception ex)
				//	{
				//		Console.WriteLine(ex.Message);
				//		MessageBox.Show("Профіль не вдалося видалити\n" + ex.Message);
				//	}
				//}
			}
		}
	}
}
