using System;
namespace PointOfSale.Domain
{
	public interface ReceiptDisplay
	{
		void CompleteSaleHandler(object sender, CompleteSaleEventArgs e);
	}
}

