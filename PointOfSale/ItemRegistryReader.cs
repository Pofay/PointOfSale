using System;
namespace PointOfSale.Domain
{
	public interface ItemRegistryReader
	{
		Item getItemWith(string barcode);
	}
}

