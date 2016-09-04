using System;
namespace PointOfSale.Domain
{
	public interface ItemDisplay
	{
		void BarcodeHandler(object sender, ScannedBarcodeEventArgs args);
	}
}

