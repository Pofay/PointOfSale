using System;
using System.Collections.Generic;
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
			if (table.Rows[0] != null)
				return new Item(table.Rows[0]["barcode"].ToString(),
								table.Rows[0]["name"].ToString(),
								double.Parse(table.Rows[0]["price"].ToString()));
			else
				throw new IndexOutOfRangeException();
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

