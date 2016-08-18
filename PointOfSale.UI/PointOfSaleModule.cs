using System;
using System.Configuration;
using Autofac;
using PointOfSale.Domain;
using PointOfSale.SqlDataAccess;

namespace PointOfSale.UI
{
	public class PointOfSaleModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.Register(r => new MySqlItemRegistry
							 (ConfigurationManager.ConnectionStrings["pointofsale"].ConnectionString))
				   .AsImplementedInterfaces();
			builder.RegisterType<ConsolePosDisplay>().AsImplementedInterfaces();
			builder.RegisterType<ConsoleReceiptFactory>().AsImplementedInterfaces();
			builder.RegisterType<Sale>().InstancePerDependency();
		}
	}
}

