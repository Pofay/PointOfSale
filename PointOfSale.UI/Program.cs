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

			var sale = container.Resolve<Sale>();

			sale.Scan("123456");
			sale.OnCompleteSale();

		}
	}
}
