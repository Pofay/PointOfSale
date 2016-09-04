using System;
using PointOfSale.Domain;

namespace PointOfSale.UI
{
	public class ConsolePosDisplay : Display
	{
		public ConsolePosDisplay()
		{
		}

		public void DisplayReceipt(Receipt receipt)
		{
			Console.WriteLine(receipt);
		}

		public void HandleItemRead(object sender, ScanEventArgs args)
		{
			Console.WriteLine(args.ReadItem);
		}
	}
}

