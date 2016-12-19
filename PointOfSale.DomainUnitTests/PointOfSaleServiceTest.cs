using System.Linq;
using FluentAssertions;
using Moq;
using PointOfSale.Domain;
using Xunit;
using Xunit.Extensions;

namespace PointOfSale.DomainUnitTests
{
	public class PointOfSaleServiceTest
	{
		int numOfDecimalPlaces = 1;


		[Fact]
		public void GetTotalPriceOnEmptyBarcodeReturns0Price()
		{
			// Arrange
			var sut = new PointOfSaleServiceBuilder()
				.WithQuery(new InMemoryItemRegistry())
				.WithGenerator(new Mock<TransactionIdGenerator>().Object)
				.Build();
			string emptyBarcode = "";

			// Act
			sut.OnBarcodeScan(emptyBarcode);

			// Assert
			var expected = new decimal(0);
			Assert.Equal(expected, sut.SubTotal, numOfDecimalPlaces);
		}

		[Theory]
		[InlineData("123456", 12.50)]
		[InlineData("456789", 24.50)]
		[InlineData("900000", 7.50)]
		public void GetTotalPriceForOneItemReturnsCorrectResult(string barcode, double price)
		{
			// Arrange
			var sut = new PointOfSaleServiceBuilder()
				.WithQuery(new InMemoryItemRegistry())
				.WithGenerator(new Mock<TransactionIdGenerator>().Object)
				.Build();
			var expected = new decimal(price);

			// Act
			sut.OnBarcodeScan(barcode);

			// Assert
			Assert.Equal(expected, sut.SubTotal, numOfDecimalPlaces);
		}


		[Theory]
		[InlineData("123456")]
		public void ItemWithRegisteredBarcodeIsStoredInsideScannedItems(string barcode)
		{
			// Arrange
			var registry = new InMemoryItemRegistry();
			var sut = new PointOfSaleServiceBuilder()
				.WithQuery(registry)
				.WithGenerator(new Mock<TransactionIdGenerator>().Object)
				.Build();
			var expected = registry.Read(barcode);

			// Act
			sut.OnBarcodeScan(barcode);

			// Assert
			sut.ScannedItems.Should().Contain(expected);
		}
	}

}
