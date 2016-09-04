using System;
using System.Configuration;
using Autofac;
using Autofac.Features.ResolveAnything;
using PointOfSale.Domain;
using PointOfSale.SqlDataAccess;

namespace PointOfSale.UI
{
	public class DevelopmentModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			string connectionString = ConfigurationManager.ConnectionStrings["pointofsale"].ConnectionString;

			builder.RegisterTypes(typeof(MySqlItemRegistry))
				   .Where(t => t.Name.StartsWith("MySql", StringComparison.Ordinal))
				   .WithParameter("connectionString", connectionString)
				   .AsImplementedInterfaces();

			builder.RegisterType<ConsolePosDisplay>().AsImplementedInterfaces();
			builder.RegisterType<ConsoleReceiptFactory>().AsImplementedInterfaces();

			builder.RegisterType<NullTransactionIdGenerator>().AsImplementedInterfaces();

			builder.RegisterType<PointOfSaleService>()
				   .OnActivating(e => e.Instance.BarcodeEvent += e.Context.Resolve<ItemDisplay>().BarcodeHandler)
				   .OnActivating(e => e.Instance.CompleteSaleEvent +=
								 e.Context.Resolve<ReceiptDisplay>().CompleteSaleHandler);
			builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
		}
	}

}

