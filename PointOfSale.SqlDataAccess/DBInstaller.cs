using System;
using System.Reflection;
using MySql.Data.MySqlClient;

namespace PointOfSale.SqlDataAccess
{
	public class DBInstaller
	{
		public void InstallDatabase(string connStr)
		{
			var builder = new MySqlConnectionStringBuilder(connStr);
			using (var conn = new MySqlConnection(builder.ConnectionString))
			{
				conn.Open();
				using (var cmd = new MySqlCommand())
				{
					cmd.Connection = conn;
					var schemaSql = EmbeddedResourceLoader.GetEmbeddedResourceString(
						Assembly.GetAssembly(typeof(MySqlItemRegistry)), "PointOfSaleDBSchema.sql");

					foreach (var sql in schemaSql.Split(
						new[] { "" }, StringSplitOptions.RemoveEmptyEntries))
					{
						cmd.CommandText = sql;
						cmd.ExecuteNonQuery();
					}
				}
			}
		}

	}
}

