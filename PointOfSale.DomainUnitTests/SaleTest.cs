using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Ploeh.AutoFixture.Xunit;
using PointOfSale.Domain;
using Xunit;
using Xunit.Extensions;

namespace PointOfSale.DomainUnitTests
{
	public class SaleTest
	{
		int numOfDecimalPlaces = 1;

		[Theory]
		[InlineData("123456", 12.50)]
		[InlineData("456789", 24.50)]
		[InlineData("900000", 7.50)]
		public void GetTotalPriceForOneItemReturnsCorrectResult(string barcode, double price)
		{
			// Arrange
			var expected = new decimal(price);
			var itemRepo = new InMemoryItemRegistry();
			var dummyDisplay = new Mock<Display>();
			var dummyFactory = new Mock<ReceiptFactory>();
			var sut = new Sale(itemRepo, dummyDisplay.Object, dummyFactory.Object);

			// Act
			sut.Scan(barcode);

			// Assert
			Assert.Equal(expected, sut.TotalPrice, numOfDecimalPlaces);
		}


		[Theory]
		[InlineData(new string[2] { "123456", "456789" }, 37.00)]
		public void GetTotalPriceForTwoItemsReturnsTotalPrice(string[] barcodes, double price)
		{
			// Arrange
			var expected = new decimal(price);
			var itemRepo = new InMemoryItemRegistry();
			var dummyDisplay = new Mock<Display>();
			var dummyFactory = new Mock<ReceiptFactory>();
			var sut = new Sale(itemRepo, dummyDisplay.Object, dummyFactory.Object);

			// Act
			barcodes.ToList().ForEach(barcode => sut.Scan(barcode));

			// Assert
			Assert.Equal(expected, sut.TotalPrice, numOfDecimalPlaces);
		}


		[Theory, AutoMoqData]
		public void GetTotalPriceOnEmptyBarcodeReturns0Price(
			InMemoryItemRegistry registry,
			Mock<Display> dummyDisplay,
			Mock<ReceiptFactory> dummyFactory)
		{
			// Arrange
			var sut = new Sale(registry, dummyDisplay.Object, dummyFactory.Object);
			string emptyBarcode = "";

			// Act
			sut.Scan(emptyBarcode);

			// Assert
			var expected = new decimal(0);
			Assert.Equal(expected, sut.TotalPrice, numOfDecimalPlaces);
		}

		[Theory]
		[InlineData("123456")]
		public void ItemWithRegisteredBarcodeIsStoredInsideScannedItems(string barcode)
		{
			// Arrange
			var registry = new InMemoryItemRegistry();
			var dummyDisplay = new Mock<Display>();
			var dummyFactory = new Mock<ReceiptFactory>();
			var sut = new Sale(registry, dummyDisplay.Object, dummyFactory.Object);
			var expected = registry.getItemWith(barcode);

			// Act
			sut.Scan(barcode);

			// Assert
			sut.ScannedItems.Should().Contain(expected);
		}


		[Theory, AutoMoqData]
		public void ScannedItemIsDisplayed(
			InMemoryItemRegistry registry,
			[Frozen] Mock<Display> sut,
			Mock<ReceiptFactory> dummy)
		{
			// Arrange
			var sale = new Sale(registry, sut.Object, dummy.Object);
			var expected = registry.getItemWith("123456");

			// Act
			sale.Scan("123456");

			// Assert
			sut.Verify(s => s.DisplayScannedItem(expected));
		}

		[Theory, AutoMoqData]
		public void CompleteSaleDisplaysReceipt(
			InMemoryItemRegistry registry,
			[Frozen] Mock<ReceiptFactory> stub,
			[Frozen] Mock<Display> sut)
		{
			// Arrange
			var sale = new Sale(registry, sut.Object, stub.Object);
			var item = registry.getItemWith("123456");
			var expected = new Receipt(item.Price);
			stub.Setup(s => s.CreateReceiptFrom(item.Price)).Returns(expected);

			// Act
			sale.Scan("123456");
			sale.OnCompleteSale();

			// Assert
			sut.Verify(s => s.DisplayReceipt(expected));
		}
	}

}
