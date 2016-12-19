using System;
using FluentAssertions;
using Moq;
using PointOfSale.Domain;
using Xunit;
using Xunit.Extensions;

namespace PointOfSale.DomainUnitTests
{
	public class PointOfSaleDisplayTest
	{

		[Theory]
		[InlineData(334456, "123456")]
		public void CompleteSaleDisplaysReceiptAndClearsScannedItems(int transactionId, string barcode)
		{
			// Arrange
			var sut = new Mock<Display>();
			var stubGenerator = new Mock<TransactionIdGenerator>();
			stubGenerator.Setup(s => s.GenerateTransactionId()).Returns(transactionId);
			var sale = new PointOfSaleServiceBuilder()
				.WithDisplay(sut.Object)
				.WithQuery(new InMemoryItemRegistry())
				.WithGenerator(stubGenerator.Object)
				.Build();
			sale.OnBarcodeScan(barcode);
			var expected = new CompleteSaleEventArgs(transactionId, sale.ScannedItems);
			// Act
			sale.OnCompleteSale();

			// Assert
			sut.Verify(s => s.CompleteSaleHandler(sale, expected));
			sale.ScannedItems.Should().BeEmpty();
		}

		[Fact]
		public void ScannedItemIsDisplayed()
		{
			// Arrange
			var registry = new InMemoryItemRegistry();
			var sut = new Mock<Display>();
			string barcode = "123456";
			var sale = new PointOfSaleServiceBuilder()
				.WithQuery(new InMemoryItemRegistry())
				.WithGenerator(new Mock<TransactionIdGenerator>().Object)
				.WithDisplay(sut.Object)
				.Build();
			var expected = new ScannedBarcodeEventArgs(registry.Read(barcode));

			// Act
			sale.OnBarcodeScan(barcode);

			// Assert
			sut.Verify(s => s.BarcodeHandler(sale, expected));
		}

	}
}

