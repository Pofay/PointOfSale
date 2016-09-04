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
			Mock<TransactionIdGenerator> stubGenerator,
			Mock<Display> sut)
		{
			// Arrange
			var sale = new PointOfSaleService(registry, stubGenerator.Object);
			sale.BarcodeEvent += delegate { };
			sale.CompleteSaleEvent += sut.Object.CompleteSaleHandler;
			sale.OnBarcodeScan(barcode);
			var expected = new CompleteSaleEventArgs(transactionId, sale.ScannedItems);

			stubGenerator.Setup(s => s.GenerateTransactionId()).Returns(transactionId);

			// Act
			sale.OnCompleteSale();

			// Assert
			sut.Verify(s => s.CompleteSaleHandler(sale, expected));
			sale.ScannedItems.Should().BeEmpty();
		}

		[Theory, AutoConfiguredMoq]
		public void ScannedItemIsDisplayed(
			InMemoryItemRegistry registry,
			[Frozen] Mock<Display> sut,
			Mock<TransactionIdGenerator> dummyGenerator)
		{
			// Arrange
			string barcode = "123456";
			var sale = new PointOfSaleService(registry, dummyGenerator.Object);
			var expected = new ScannedBarcodeEventArgs(registry.Read(barcode));
			sale.BarcodeEvent += sut.Object.BarcodeHandler;

			// Act
			sale.OnBarcodeScan(barcode);

			// Assert
			sut.Verify(s => s.BarcodeHandler(sale, expected));
		}

	}
}

