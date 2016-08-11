using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OrderSystem.Domain
{
    public class OrderTest
    {

        [Theory]
        [InlineData("123456", 12.50)]
        [InlineData("456789", 24.50)]
        [InlineData("900000", 7.50)]
        public void GetTotalPriceForOneItemOnSaleReturnsCorrectResult(string barcode, double price)
        {
            // Arrange
            var itemRepo = new ItemRepository();
            var sut = new Sale(itemRepo);

            // Act
            sut.OnBarcode(barcode);

            // Assert
            decimal expected = new decimal(price);
            Assert.Equal(expected, sut.TotalPrice);
        }


        [Theory]
        [InlineData(new string[2] { "123456", "456789" }, 37.00)]
        public void GetTotalPriceForTwoItemsOnSaleReturnsTotalPrice(string[] barcodes, double totalPrice)
        {
            // Arrange
            var itemRepo = new ItemRepository();
            var sut = new Sale(itemRepo);

            // Act
            foreach (var barcode in barcodes)
            {
                sut.OnBarcode(barcode);
            }

            // Assert
            decimal expected = new decimal(totalPrice);
            Assert.Equal(expected, sut.TotalPrice);
        }


        [Fact]
        public void GetTotalPriceOnEmptyBarcodeReturns0Price()
        {
            // Arrange
            var itemRepo = new ItemRepository();
            var sut = new Sale(itemRepo);
            // Act
            sut.OnBarcode("");
            // Assert
            decimal expected = new decimal(0);
            Assert.Equal(expected, sut.TotalPrice);
        }
    }

}
