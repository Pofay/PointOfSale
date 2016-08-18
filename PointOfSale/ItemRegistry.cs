using System;
namespace PointOfSale.Domain
{
	public interface ItemRegistry
	{
		Item getItemWith(string barcode);
	}
}

