using System;
using System.Data;
using MySql.Data.MySqlClient;
using PointOfSale.Domain;

namespace PointOfSale.SqlDataAccess
{
	public class MySqlItemRegistry : ScanBarcodeQuery, IDisposable
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

		public Item Read(string barcode)
		{
			const string sql = "SELECT barcode, name, price FROM ITEM WHERE barcode = @barcode";

			var adapter = new MySqlDataAdapter();
			var table = new DataTable();
			using (var cmd = new MySqlCommand(sql, connection))
			{
				cmd.Parameters.Add(new MySqlParameter("@barcode", barcode));

				adapter.SelectCommand = cmd;
				adapter.SelectCommand.CommandType = CommandType.Text;
				adapter.Fill(table);
			}
			var row = table.Rows[0];
			return new Item(row["barcode"].ToString(), row["name"].ToString(), double.Parse(row["price"].ToString()));

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

