using System;
namespace PointOfSale.Domain
{
	public interface ScanBarcodeQuery
	{
		Item Read(string barcode);
	}
}

