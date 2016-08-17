using System;
using Autofac;
using PointOfSale.Domain;

namespace PointOfSale.UI
{
	public class MainClass
	{
		public static void Main(string[] args)
		{
			var builder = new ContainerBuilder();
			builder.RegisterModule(new PointOfSaleModule());
			var container = builder.Build();

			bool exit = false;

			var sale = container.Resolve<Sale>();
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
						sale.Scan(barcode);
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
