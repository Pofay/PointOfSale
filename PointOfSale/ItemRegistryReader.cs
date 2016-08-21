using System;
namespace PointOfSale.Domain
{
	public interface ItemRegistryReader
	{
		Item Read(string barcode);
	}
}

