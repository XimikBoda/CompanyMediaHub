﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static VideoSharingSystem.Form1;

namespace VideoSharingSystem
{
	public partial class Form1 : Form
	{
		public class Tag
		{
			public int id { get; set; }
			public string name { get; set; }

			public override string ToString() => name;
		}
		public class Tags
		{
			public List<Tag> tags { get; set; }
		}

		public int myUserId = -1;
		public bool isAdmin = false;

		public AuthenticationHeaderValue bearer_token;
		public string url_host;

		public Tags tags = new Tags();

		public Form1(string token, string url_host, int user_id)
		{
			bearer_token = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
			this.url_host = url_host;

			myUserId = user_id;
			this.isAdmin = false;//isAdmin;

			commentElements = new();
			videoElements = new();
			subscriberElement = new();

			findVideoElements = new();
			findUserElement = new();


			InitializeComponent();
			InitHistory();
			GetTags();
			GetMySubscriptions();
			InitProfileViewer(1);
		}

		private void GetTags()
		{
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = bearer_token;

				string loginUrl = $"{url_host}/video/tags";

				try
				{
					HttpResponseMessage response = client.GetAsync(loginUrl).Result;

					if (response.IsSuccessStatusCode)
					{
						string responseBody = response.Content.ReadAsStringAsync().Result;

						tags = JsonSerializer.Deserialize<Tags>(responseBody);
					}
					else
					{
						tags = new Tags();
						MessageBox.Show("Ошибка при виконанні запита: " + response.StatusCode);
					}
				}
				catch (Exception ex)
				{
					tags = new Tags();
					Console.WriteLine(ex.Message);
					MessageBox.Show(ex.Message);
				}
			}
		}



		private void button5_Click(object sender, EventArgs e)
		{
			InitProfileViewer(myUserId);
		}

		private void label9_Click(object sender, EventArgs e)
		{

		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		void UpdateSomeElementPositionOnProfile()
		{
			label10.Location = new Point(splitContainer6.SplitterDistance + 541 - 527, label10.Location.Y);
			button8.Location = new Point(splitContainer6.SplitterDistance + 381 - 527, button8.Location.Y);
		}

		private void Form1_Resized(object sender, EventArgs e)
		{
			UpdateSomeElementPositionOnProfile();
		}

		private void splitContainer6_SplitterMoved(object sender, SplitterEventArgs e)
		{
			UpdateSomeElementPositionOnProfile();
		}

		private void button7_Click(object sender, EventArgs e)
		{
			Find(comboBox1.Text);
		}

		private void FindTextBoxEnter(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				Find(comboBox1.Text);
				e.Handled = true;
			}
		}

		private void flowLayoutPanel5_Paint(object sender, PaintEventArgs e)
		{

		}

		private void button4_Click(object sender, EventArgs e)
		{
			AddComment(richTextBox2.Text);
			richTextBox2.Text = "";
		}
		private void button4_Enter(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				AddComment(richTextBox2.Text);
				richTextBox2.Text = "";
				e.Handled = true;
			}
		}

		private void button8_Click(object sender, EventArgs e)
		{
			AddVideo f = new(this);
			f.ShowDialog(this);
			InitProfileViewer(myUserId);
		}

		

		private void label12_Click(object sender, EventArgs e)
		{
			InitProfileViewer(currentVideoUserId);
		}

		private void button9_Click(object sender, EventArgs e)
		{
			new EditUser(this, currentCompanyId, isAdmin).ShowDialog(this);
			InitProfileViewer(currentCompanyId);
		}

		private void button6_Click(object sender, EventArgs e)
		{
			SetSubscribe();
		}

		private void button10_Click(object sender, EventArgs e)
		{
			int ccurrentVideoId = currentVideoId;
			new EditVideo(this, currentVideoId).ShowDialog(this);
			if (currentVideoId == -1)
			{
				//DeInitVideoPlayer();
				//currentVideoId = -1;
				InitVideoPlayer(ccurrentVideoId);
			}
		}

		private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
		{

		}

		private void label2_Click(object sender, EventArgs e)
		{

		}
	}
}
