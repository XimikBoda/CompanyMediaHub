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
	public partial class EditVideo : Form
	{
		Form1 _mainForm;
		int _idVideo;
		public EditVideo(Form1 mainForm, int idVideo)
		{
			_mainForm = mainForm;
			_idVideo = idVideo;

			InitializeComponent();

			//using (SqlConnection connection = new SqlConnection(_mainForm.connectionString))
			//{
			//	SqlCommand command = new SqlCommand("SELECT NameV, DescriptionV FROM Videos WHERE IdVideo = @IdVideo", connection);
			//	command.Parameters.AddWithValue("@IdVideo", _idVideo);

			//	try
			//	{
			//		connection.Open();
			//		{
			//			SqlDataReader reader = command.ExecuteReader();
			//			if (reader.Read())
			//			{
			//				textBox1.Text = reader[0].ToString();
			//				richTextBox1.Text = reader[1].ToString();
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

		private void button3_Click(object sender, EventArgs e)
		{
			//using (SqlConnection connection = new SqlConnection(_mainForm.connectionString))
			//{
			//	SqlCommand command = new SqlCommand("UPDATE Videos SET NameV = @NameV, DescriptionV = @DescriptionV " +
			//		"WHERE IdVideo = @IdVideo", connection);
			//	command.Parameters.Add("@NameV", SqlDbType.VarChar, 30);
			//	command.Parameters.Add("@DescriptionV", SqlDbType.VarChar, 500);
			//	command.Parameters.Add("@IdVideo", SqlDbType.Int);

			//	command.Parameters["@NameV"].Value = textBox1.Text;
			//	command.Parameters["@DescriptionV"].Value = richTextBox1.Text;
			//	command.Parameters["@IdVideo"].Value = _idVideo;
			//	try
			//	{
			//		connection.Open();
			//		command.ExecuteNonQuery();
			//		_mainForm.DeInitVideoPlayer();
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

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			button3.Enabled = textBox1.Text.Length != 0;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var result = MessageBox.Show("Ви впевненні, що хочете видалити це відно?\nВидалиться вся інформація пов'язанна з ним (оцінки, коментарі).\nЦю дію буде неможливо відминити!",
				"Видалення відео", MessageBoxButtons.OKCancel);

			if (result == DialogResult.OK)
			{
				//using (SqlConnection connection = new SqlConnection(_mainForm.connectionString))
				//{
				//	SqlCommand command = new SqlCommand("Delete FROM Videos WHERE IdVideo = @IdVideo", connection);
				//	command.Parameters.Add("@IdVideo", SqlDbType.Int);

				//	command.Parameters["@IdVideo"].Value = _idVideo;
				//	try
				//	{
				//		connection.Open();
				//		command.ExecuteNonQuery();
				//		MessageBox.Show("Відео видаленно");
				//		Close();
				//		_mainForm.InitProfileViewer(_mainForm.myUserId);
				//		_mainForm.DeInitVideoPlayer();
				//	}
				//	catch (Exception ex)
				//	{
				//		Console.WriteLine(ex.Message);
				//		MessageBox.Show("Відео не вдалося видалити\n" + ex.Message);
				//	}
				//}
			}
		}
	}
}
