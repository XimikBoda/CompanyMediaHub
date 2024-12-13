using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace VideoSharingSystem
{
	class BDStream : Stream
	{
		long _size;//DATALENGTH
		long _pos;

		string _connectionString;
		int _idVideo;

		public BDStream(string connectionString, int idVideo) 
		{
			_connectionString = connectionString;
			_idVideo = idVideo;
		}
		public override bool CanRead => true;

		public override bool CanSeek => true;

		public override bool CanWrite => false;

		public override long Length => _size;

		public override long Position { get => _pos; set { _pos = value; } }

		public override void Flush()
		{
			//todo
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				SqlCommand command = new SqlCommand(
					"SELECT varbinary(VideoData, @pos, @len) FROM Users WHERE IdVideo = @id ", connection);
				command.Parameters.AddWithValue("@IdVideo", _idVideo);
				command.Parameters.AddWithValue("@pos", offset);
				command.Parameters.AddWithValue("@len", count);

				try
				{
					connection.Open();
					{
						SqlDataReader reader = command.ExecuteReader();
						buffer = ((byte[])reader[0]).ToArray();
						reader.Close();
						return ((byte[])reader[0]).Length;
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					MessageBox.Show(ex.Message);
				}
			}
			return 0;
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			switch (origin)
			{
				case SeekOrigin.Begin:
					_pos = offset;
					break;
				case SeekOrigin.Current:
					_pos += offset;
					break;
				case SeekOrigin.End:
					_pos = _size - offset;
					break;
			}
			throw new NotImplementedException();
		}

		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}
	}

}
