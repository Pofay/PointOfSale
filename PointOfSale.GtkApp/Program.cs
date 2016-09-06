using System;
using System.Configuration;
using Autofac;
using Gtk;
using PointOfSale.Domain;
using PointOfSale.SqlDataAccess;

namespace PointOfSale.GtkApp
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			var builder = new ContainerBuilder();
			builder.RegisterModule(new ProductionModule());
			var container = builder.Build();
			var installer = new DBInstaller();

			installer.InstallDatabase(ConfigurationManager.ConnectionStrings["pointofsale"].ConnectionString);
			using (container.BeginLifetimeScope())
			{
				Application.Init();
				MainWindow win = container.Resolve<MainWindow>();
				win.ShowAll();
				Application.Run();
			}


		}
	}
}
