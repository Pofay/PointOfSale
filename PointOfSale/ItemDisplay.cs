using System;
namespace PointOfSale.Domain
{
	public interface ItemDisplay
	{
		void HandleScanEvent(object sender, ScanEventArgs args);
	}
}

