using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace PointOfSale.Domain
{
	public class SaleTest
	{
		int numOfDecimalPlaces = 1;

		[Theory]
		[InlineData("123456", 12.50)]
		[InlineData("456789", 24.50)]
		[InlineData("900000", 7.50)]
		public void GetTotalPriceForOneItemOnSaleReturnsCorrectResult(string barcode, decimal expectedPrice)
		{
			// Arrange
			var itemRepo = new ItemRegistry();
			var sut = new Sale(itemRepo);

			// Act
			sut.OnBarcode(barcode);

			// Assert
			Assert.Equal(expectedPrice, sut.TotalPrice, numOfDecimalPlaces);
		}


		[Theory]
		[InlineData(new string[2] { "123456", "456789" }, 37.00)]
		public void GetTotalPriceForTwoItemsOnSaleReturnsTotalPrice(string[] barcodes, decimal expectedTotalPrice)
		{
			// Arrange
			var itemRepo = new ItemRegistry();
			var sut = new Sale(itemRepo);

			// Act
			barcodes.ToList().ForEach(barcode => sut.OnBarcode(barcode));

			// Assert
			Assert.Equal(expectedTotalPrice, sut.TotalPrice, numOfDecimalPlaces);
		}


		[Fact]
		public void GetTotalPriceOnEmptyBarcodeReturns0Price()
		{

			// Arrange
			var itemRepo = new ItemRegistry();
			var sut = new Sale(itemRepo);
			// Act
			sut.OnBarcode("");
			// Assert
			var expected = new decimal(0);
			Assert.Equal(expected, sut.TotalPrice, numOfDecimalPlaces);
		}

		[Theory]
		[InlineData("123456", "Bowl", 12.50)]
		public void SaleStoresItemNameOfAssociatedBarcode(string barcode, string name, double price)
		{
			var itemRepo = new ItemRegistry();
			var sut = new Sale(itemRepo);
			var expected = new Item(barcode, name, price);

			sut.OnBarcode(barcode);

			sut.ScannedItems.Should().Contain(expected);
		}

		[Fact]
		public void DisplayShowsItemInStringForEveryBarcodeRead()
		{

		}
	}

}
