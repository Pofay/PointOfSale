using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
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
			var itemRepo = new ItemRegistry();
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
			var itemRepo = new ItemRegistry();
			var dummyDisplay = new Mock<Display>();
			var dummyFactory = new Mock<ReceiptFactory>();
			var sut = new Sale(itemRepo, dummyDisplay.Object, dummyFactory.Object);

			// Act
			barcodes.ToList().ForEach(barcode => sut.Scan(barcode));

			// Assert
			Assert.Equal(expected, sut.TotalPrice, numOfDecimalPlaces);
		}


		[Fact]
		public void GetTotalPriceOnEmptyBarcodeReturns0Price()
		{

			// Arrange
			var itemRepo = new ItemRegistry();
			var dummyDisplay = new Mock<Display>();
			var dummyFactory = new Mock<ReceiptFactory>();
			var sut = new Sale(itemRepo, dummyDisplay.Object, dummyFactory.Object);

			// Act
			sut.Scan("");

			// Assert
			var expected = new decimal(0);
			Assert.Equal(expected, sut.TotalPrice, numOfDecimalPlaces);
		}

		[Theory]
		[InlineData("123456")]
		public void ItemWithRegisteredBarcodeIsStoredInsideScannedItems(string barcode)
		{
			// Arrange
			var itemRepo = new ItemRegistry();
			var dummyDisplay = new Mock<Display>();
			var dummyFactory = new Mock<ReceiptFactory>();
			var sut = new Sale(itemRepo, dummyDisplay.Object, dummyFactory.Object);
			var expected = itemRepo.getItemWith(barcode);

			// Act
			sut.Scan(barcode);

			// Assert
			sut.ScannedItems.Should().Contain(expected);
		}


		[Fact]
		public void ScannedItemIsDisplayed()
		{
			// Arrange
			var itemRepo = new ItemRegistry();
			var sut = new Mock<Display>();
			var dummy = new Mock<ReceiptFactory>();
			var sale = new Sale(itemRepo, sut.Object, dummy.Object);
			var item = itemRepo.getItemWith("123456");

			// Act
			sale.Scan("123456");

			// Assert
			sut.Verify(s => s.DisplayScannedItem(item));
		}

		[Fact]
		public void CompleteSaleDisplaysReceipt()
		{
			// Arrange
			var itemRepo = new ItemRegistry();

			var sut = new Mock<Display>();
			var stubFactory = new Mock<ReceiptFactory>();
			var item = itemRepo.getItemWith("123456");
			var expected = new Receipt(item.Price);
			stubFactory.Setup(s => s.CreateReceiptFrom(item.Price)).Returns(expected);

			var sale = new Sale(itemRepo, sut.Object, stubFactory.Object);
			// Act
			sale.Scan("123456");
			sale.OnCompleteSale();

			// Assert
			sut.Verify(s => s.DisplayReceipt(expected));
		}
	}

}
