using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoSharingSystem
{
	partial class Form1
	{
		List<VideoElement> findVideoElements;
		List<SubscriberElement> findUserElement;

		public void InitHistory() {
			//using (SqlConnection connection = new SqlConnection(connectionString))
			//{
			//	SqlCommand command = new SqlCommand(
			//		"SELECT IdSearchHistory, IdUser, SearchQuery, SearchTime " +
			//		"FROM SearchHistory WHERE IdUser = @IdUser ORDER BY IdSearchHistory DESC", connection);
			//	command.Parameters.AddWithValue("@IdUser", myUserId);

			//	try
			//	{
			//		connection.Open();
			//		{
			//			SqlDataReader reader = command.ExecuteReader();
			//			comboBox1.Items.Clear();

			//			while (reader.Read())
			//			{
			//				comboBox1.Items.Add(reader[2].ToString());
			//			}
			//			reader.Close();
			//		}
			//	}
			//	catch (Exception ex)
			//	{
			//		Console.WriteLine(ex.Message);
			//		MessageBox.Show(ex.Message);
			//	}
			//}
		}
		public void Find(string findString)
		{
			//using (SqlConnection connection = new SqlConnection(connectionString))
			//{
			//	SqlCommand command = new SqlCommand("SELECT IdVideo, NameV, DescriptionV, UploadTime FROM Videos " +
			//		"WHERE LOWER(NameV) LIKE LOWER('%'+Trim(@str)+'%') OR " +
			//		"LOWER(DescriptionV) LIKE LOWER('%'+Trim(@str)+'%')", connection);

			//	command.Parameters.AddWithValue("@str", findString);

			//	SqlCommand command2 = new SqlCommand(
			//		"SELECT IdUser, NameUser, Surname, About FROM Users " +
			//		"WHERE LOWER(NameUser) LIKE LOWER('%'+Trim(@str)+'%') OR " +
			//		"LOWER(Surname) LIKE LOWER('%'+Trim(@str)+'%') OR " +
			//		"LOWER(About) LIKE LOWER('%'+Trim(@str)+'%')", connection);
			//	command2.Parameters.AddWithValue("@str", findString);

			//	SqlCommand command3 = new SqlCommand("INSERT INTO SearchHistory (IdUser, SearchQuery, SearchTime) " +
			//		"VALUES (@IdUser, @SearchQuery, GETDATE())", connection);
			//	command3.Parameters.Add("@IdUser", SqlDbType.Int);
			//	command3.Parameters.Add("@SearchQuery", SqlDbType.VarChar, 100);

			//	command3.Parameters["@IdUser"].Value = myUserId;
			//	command3.Parameters["@SearchQuery"].Value = findString;

			//	try
			//	{
			//		connection.Open();
			//		{
			//			SqlDataReader reader = command.ExecuteReader();
			//			foreach (var el in findVideoElements)
			//				el.Deatach();
			//			findVideoElements.Clear();
			//			while (reader.Read())
			//			{

			//				findVideoElements.Add(new VideoElement(flowLayoutPanel5, this,
			//					Convert.ToInt32(reader[0].ToString()),
			//					reader[1].ToString().Trim(), reader[2].ToString().Trim(), reader[3].ToString().Trim()));
			//			}
			//			reader.Close();
			//		}
			//		{
			//			SqlDataReader reader = command2.ExecuteReader();
			//			foreach (var el in findUserElement)
			//				el.Deatach();
			//			findUserElement.Clear();
			//			while (reader.Read())
			//			{

			//				findUserElement.Add(new SubscriberElement(flowLayoutPanel5, this,
			//					Convert.ToInt32(reader[0].ToString()),
			//					reader[1].ToString().Trim() + ' ' + reader[2].ToString().Trim()));
			//			}
			//			reader.Close();
			//		}
			//		try
			//		{
			//			command3.ExecuteNonQuery();
			//			InitHistory();
			//		}
			//		catch { }
			//	}
			//	catch (Exception ex)
			//	{
			//		Console.WriteLine(ex.Message);
			//		MessageBox.Show(ex.Message);
			//	}
			//}
		}
	}
}
