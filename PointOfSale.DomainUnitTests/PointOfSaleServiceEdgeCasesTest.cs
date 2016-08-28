using System;
using Moq;
using PointOfSale.Domain;
using Xunit;
using Xunit.Extensions;

namespace PointOfSale.DomainUnitTests
{
	public class PointOfSaleServiceEdgeCasesTest
	{

		int numOfDecimalPlaces = 1;

		[Theory, AutoConfiguredMoq]
		public void GetTotalPriceOnEmptyBarcodeReturns0Price(
			InMemoryItemRegistry registry,
			Mock<Display> dummyDisplay,
			Mock<ReceiptFactory> dummyFactory,
			Mock<OrderFulFiller> dummyRepo,
			Mock<TransactionIdGenerator> dummyGenerator)
		{
			// Arrange
			var itemService = new ItemService(registry, dummyDisplay.Object);
			var receiptService = new ReceiptService(dummyFactory.Object, dummyDisplay.Object);
			var sut = new PointOfSaleService(itemService, receiptService, dummyRepo.Object, dummyGenerator.Object);
			string emptyBarcode = "";

			// Act
			sut.Scan(emptyBarcode);

			// Assert
			var expected = new decimal(0);
			Assert.Equal(expected, sut.SubTotal, numOfDecimalPlaces);
		}
	}
}

