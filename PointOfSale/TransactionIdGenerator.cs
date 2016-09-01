using System;
namespace PointOfSale.Domain
{
	public interface TransactionIdGenerator
	{
		int GenerateTransactionId();
	}
}

