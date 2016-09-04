using System;
using PointOfSale.Domain;

namespace PointOfSale.UI
{
	public class ConsolePosDisplay : Display
	{
		public ConsolePosDisplay()
		{
		}

		public void BarcodeHandler(object sender, ScannedBarcodeEventArgs args)
		{
			Console.WriteLine(args.ReadItem);
		}

		public void CompleteSaleHandler(object sender, CompleteSaleEventArgs e)
		{
			var receipt = new Receipt(e.Id, e.Items);
			Console.WriteLine(receipt);
		}
	}
}

