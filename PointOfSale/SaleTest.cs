using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PointOfSale.Domain
{
	public class SaleTest
	{

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
			Assert.Equal(expectedPrice, sut.TotalPrice);
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
			Assert.Equal(expectedTotalPrice, sut.TotalPrice);
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
			decimal expected = new decimal(0);
			Assert.Equal(expected, sut.TotalPrice);
		}

		[Theory]
		[InlineData(new object[] { "123456", "456789" }, new object[] { "Bowl", "Crab" })]
		[InlineData(new object[] { "789010", "345670" }, new object[] { "Fish", "Plunger" })]
		public void SaleStoresItemNameOfAssociatedBarcode(string[] barcodes, string[] expectedItemNames)
		{
			var itemRepo = new ItemRegistry();
			var sut = new Sale(itemRepo);

			foreach (var barcode in barcodes)
			{
				sut.OnBarcode(barcode);
			}
			Assert.Equal(expectedItemNames, sut.ItemNames);
		}
	}

}
