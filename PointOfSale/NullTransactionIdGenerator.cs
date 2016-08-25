using System;
namespace PointOfSale.Domain
{
	public class NullTransactionIdGenerator : TransactionIdGenerator
	{
		public NullTransactionIdGenerator()
		{
		}

		public int GenerateTransactionId()
		{
			return 0;
		}
	}
}

