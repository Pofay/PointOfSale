using System;

namespace PointOfSale.Domain
{
	public interface ReceiptFactory
	{
		Receipt CreateReceiptFrom(decimal price);
	}
}