using System;
using System.Data;
using MySql.Data.MySqlClient;
using PointOfSale.Domain;

namespace PointOfSale.SqlDataAccess
{
	public class MySqlItemRegistry : ItemRegistry, IDisposable
	{
		private readonly MySqlConnection connection;

		public MySqlItemRegistry(string connectionString)
		{
			this.connection = this.OpenDBConnection(connectionString);
		}

		private MySqlConnection OpenDBConnection(string connStr)
		{
			var conn = new MySqlConnection(connStr);
			try
			{
				conn.Open();
			}
			catch
			{
				conn.Dispose();
				throw;
			}
			return conn;
		}

		public Item getItemWith(string barcode)
		{
			const string sql = "SELECT barcode, name, price FROM ITEM WHERE barcode = @barcode";

			using (var cmd = new MySqlCommand(sql, connection))
			{
				cmd.Parameters.Add(new MySqlParameter("@barcode", barcode));

				var adapter = new MySqlDataAdapter(cmd);
				//	adapter.SelectCommand.CommandType = System.Data.CommandType.Text;
				adapter.SelectCommand.CommandType = CommandType.Text;
				var table = new DataTable();
				adapter.Fill(table);
				var row = table.Rows[0];
				return new Item(row["barcode"].ToString(), row["name"].ToString(), double.Parse(row["price"].ToString()));
			}
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
				this.connection.Dispose();
		}
	}
}

