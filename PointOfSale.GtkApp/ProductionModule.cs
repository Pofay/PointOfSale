using System;
using System.Configuration;
using Autofac;
using Autofac.Features.ResolveAnything;
using Gtk;
using PointOfSale.Domain;
using PointOfSale.SqlDataAccess;

namespace PointOfSale.GtkApp
{
	public class ProductionModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{

			Application.Init();
			string connectionString = ConfigurationManager.ConnectionStrings["pointofsale"].ConnectionString;

			builder.RegisterTypes(typeof(MySqlItemRegistry))
				   .Where(t => t.Name.StartsWith("MySql", StringComparison.Ordinal))
				   .WithParameter("connectionString", connectionString)
				   .AsImplementedInterfaces();

			builder.RegisterType<GtkReceiptFactory>()
				   .AsImplementedInterfaces();
			builder.RegisterType<NullTransactionIdGenerator>()
				   .AsImplementedInterfaces();
			builder.RegisterType<PointOfSaleService>();

			builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
		}
	}
}