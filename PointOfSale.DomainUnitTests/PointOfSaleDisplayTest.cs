using System;
using FluentAssertions;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Kernel;
using Ploeh.AutoFixture.Xunit;
using PointOfSale.Domain;
using Xunit.Extensions;

namespace PointOfSale.DomainUnitTests
{
	public class PointOfSaleDisplayTest
	{


		[Theory, AutoConfiguredMoq]
		[InlineAutoData(334456, "123456")]
		public void CompleteSaleDisplaysReceiptAndClearsScannedItems(
			int transactionId,
			string barcode,
			InMemoryItemRegistry registry,
			[Frozen] Mock<ReceiptFactory> stubFactory,
			Mock<TransactionIdGenerator> stubGenerator,
			Mock<Display> sut)
		{
			// Arrange
			var itemService = new ItemService(registry, sut.Object);
			var receiptService = new ReceiptService(stubFactory.Object, sut.Object);
			var sale = new PointOfSaleService(itemService, receiptService, stubGenerator.Object);

			sale.Scan(barcode);

			var expected = new Receipt(transactionId, sale.ScannedItems);
			stubGenerator.Setup(s => s.GenerateTransactionId()).Returns(transactionId);
			stubFactory.Setup(s => s.CreateReceiptFrom(transactionId, sale.ScannedItems)).Returns(expected);

			// Act
			sale.OnCompleteSale();

			// Assert
			sut.Verify(s => s.DisplayReceipt(expected));
			sale.ScannedItems.Should().BeEmpty();
		}

		[Theory, AutoConfiguredMoq]
		public void ScannedItemIsDisplayed(
			InMemoryItemRegistry registry,
			[Frozen] Mock<Display> sut,
			Mock<ReceiptFactory> dummyFactory,
			Mock<TransactionIdGenerator> dummyGenerator)
		{
			// Arrange
			string barcode = "123456";
			var itemService = new ItemService(registry, sut.Object);
			var receiptService = new ReceiptService(dummyFactory.Object, sut.Object);
			var sale = new PointOfSaleService(itemService, receiptService, dummyGenerator.Object);
			var expected = registry.Read(barcode);

			// Act
			sale.Scan(barcode);

			// Assert
			sut.Verify(s => s.DisplayItem(expected));
		}

	}
}

