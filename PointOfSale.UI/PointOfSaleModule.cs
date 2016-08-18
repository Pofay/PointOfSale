using System;
using Autofac;
using PointOfSale.Domain;

namespace PointOfSale.UI
{
	public class PointOfSaleModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<InMemoryItemRegistry>().AsImplementedInterfaces();
			builder.RegisterType<ConsolePosDisplay>().AsImplementedInterfaces();
			builder.RegisterType<ConsoleReceiptFactory>().AsImplementedInterfaces();
			builder.RegisterType<Sale>().InstancePerDependency();
		}
	}
}

