using System;
using System.Configuration;
using Autofac;
using PointOfSale.Domain;
using PointOfSale.SqlDataAccess;

namespace PointOfSale.UI
{
	public class MainClass
	{
		public static void Main(string[] args)
		{
			var builder = new ContainerBuilder();
			//	builder.RegisterModule(new ProductionModule());
			builder.RegisterModule(new DevelopmentModule());
			var container = builder.Build();
			var installer = new DBInstaller();

			installer.InstallDatabase(ConfigurationManager.ConnectionStrings["pointofsale"].ConnectionString);


			using (container.BeginLifetimeScope())
			{

				var sale = container.Resolve<PointOfSaleService>();
				bool exit = false;
				while (!exit)
				{
					Console.WriteLine("Options: \nScan [Scans an Item Barcode] " +
							  "\nComplete [Complete's Sale by displaying Total Price]" +
							  "\nQuit [To Exit Program]");
					Console.Write(">");
					var command = Console.ReadLine().ToUpperInvariant();
					switch (command)
					{
						case "SCAN":
							Console.WriteLine("Enter Barcode: ");
							var barcode = Console.ReadLine();
							sale.OnBarcode(barcode);
							Console.Read();
							break;
						case "COMPLETE":
							sale.OnCompleteSale();
							Console.Read();
							break;
						case "QUIT":
							exit = true;
							break;
						default:
							Console.Clear();
							break;
					}
					Console.Clear();
				}
			}
		}
	}
}
