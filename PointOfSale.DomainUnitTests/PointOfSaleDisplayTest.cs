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
			var receiptService = new ReceiptService(stubFactory.Object, sut.Object, stubGenerator.Object);
			var sale = new PointOfSaleService(registry, receiptService);
			sale.OnScan += delegate { };
			sale.Scan(barcode);
			var expected = new Receipt(transactionId, sale.ScannedItems);

			stubGenerator.Setup(s => s.GenerateTransactionId()).Returns(transactionId);
			stubFactory.Setup(s => s.CreateReceiptFrom(transactionId, sale.ScannedItems)).Returns(expected);

			// Act
			sale.CompleteSale();

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
			var receiptService = new ReceiptService(dummyFactory.Object, sut.Object, dummyGenerator.Object);
			var sale = new PointOfSaleService(registry, receiptService);
			var expected = new ScanEventArgs(registry.Read(barcode));
			sale.OnScan += sut.Object.HandleScanEvent;

			// Act
			sale.Scan(barcode);

			// Assert
			sut.Verify(s => s.HandleScanEvent(sale, expected));
		}

	}
}

