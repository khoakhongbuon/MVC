using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using W2_test.Models;
using Microsoft.Data.SqlClient; 

namespace W2_test.DataAccess
{
	public class BlogDAL
	{
		private readonly string _connectionString;

		public BlogDAL(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("DefaultConnection");
		}

		public List<Blog> GetAllBlogs()
		{
			var blogs = new List<Blog>();
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				string sql = "SELECT * FROM Blogs ORDER BY CreatedAt DESC";
				SqlCommand cmd = new SqlCommand(sql, conn);
				conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					blogs.Add(new Blog
					{
						Id = (int)reader["Id"],
						Title = reader["Title"].ToString(),
						Content = reader["Content"].ToString(),
						CreatedAt = (DateTime)reader["CreatedAt"]
					});
				}
			}
			return blogs;
		}

		public Blog GetBlogById(int id)
		{
			Blog blog = null;
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				string sql = "SELECT * FROM Blogs WHERE Id = @Id";
				SqlCommand cmd = new SqlCommand(sql, conn);
				cmd.Parameters.AddWithValue("@Id", id);
				conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					blog = new Blog
					{
						Id = (int)reader["Id"],
						Title = reader["Title"].ToString(),
						Content = reader["Content"].ToString(),
						CreatedAt = (DateTime)reader["CreatedAt"]
					};
				}
			}
			return blog;
		}

		public void AddBlog(Blog blog)
		{
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				string sql = "INSERT INTO Blogs (Title, Content, CreatedAt) VALUES (@Title, @Content, @CreatedAt)";
				SqlCommand cmd = new SqlCommand(sql, conn);
				cmd.Parameters.AddWithValue("@Title", blog.Title);
				cmd.Parameters.AddWithValue("@Content", blog.Content);
				cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
				conn.Open();
				cmd.ExecuteNonQuery();
			}
		}

		public void UpdateBlog(Blog blog)
		{
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				string sql = "UPDATE Blogs SET Title = @Title, Content = @Content WHERE Id = @Id";
				SqlCommand cmd = new SqlCommand(sql, conn);
				cmd.Parameters.AddWithValue("@Id", blog.Id);
				cmd.Parameters.AddWithValue("@Title", blog.Title);
				cmd.Parameters.AddWithValue("@Content", blog.Content);
				conn.Open();
				cmd.ExecuteNonQuery();
			}
		}

		public void DeleteBlog(int id)
		{
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				string sql = "DELETE FROM Blogs WHERE Id = @Id";
				SqlCommand cmd = new SqlCommand(sql, conn);
				cmd.Parameters.AddWithValue("@Id", id);
				conn.Open();
				cmd.ExecuteNonQuery();
			}
		}
	}
}
