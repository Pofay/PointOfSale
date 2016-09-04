using System;
namespace PointOfSale.Domain
{
	public interface ItemDisplay
	{
		void HandleItemRead(object sender, ItemReadEventArgs args);
	}
}

