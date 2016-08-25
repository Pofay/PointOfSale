using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using Ploeh.AutoFixture.Xunit;
using PointOfSale.Domain;
using Xunit;
using Xunit.Extensions;

namespace PointOfSale.DomainUnitTests
{
	public class PointOfSaleServiceTest
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
			var itemService = new ItemService(itemRepo, dummyDisplay.Object);
			var receiptService = new ReceiptService(dummyFactory.Object, dummyDisplay.Object);
			var sut = new PointOfSaleService(itemService, receiptService);

			// Act
			sut.Scan(barcode);

			// Assert
			Assert.Equal(expected, sut.SubTotal, numOfDecimalPlaces);
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
			var itemService = new ItemService(itemRepo, dummyDisplay.Object);
			var receiptService = new ReceiptService(dummyFactory.Object, dummyDisplay.Object);
			var sut = new PointOfSaleService(itemService, receiptService);

			// Act
			barcodes.ToList().ForEach(barcode => sut.Scan(barcode));

			// Assert
			Assert.Equal(expected, sut.SubTotal, numOfDecimalPlaces);
		}


		[Theory, AutoMoqData]
		public void GetTotalPriceOnEmptyBarcodeReturns0Price(
			InMemoryItemRegistry registry,
			Mock<Display> dummyDisplay,
			Mock<ReceiptFactory> dummyFactory)
		{
			// Arrange
			var itemService = new ItemService(registry, dummyDisplay.Object);
			var receiptService = new ReceiptService(dummyFactory.Object, dummyDisplay.Object);
			var sut = new PointOfSaleService(itemService, receiptService);
			string emptyBarcode = "";

			// Act
			sut.Scan(emptyBarcode);

			// Assert
			var expected = new decimal(0);
			Assert.Equal(expected, sut.SubTotal, numOfDecimalPlaces);
		}


		[Theory]
		[InlineData("123456")]
		public void ItemWithRegisteredBarcodeIsStoredInsideScannedItems(string barcode)
		{
			// Arrange
			var registry = new InMemoryItemRegistry();
			var dummyDisplay = new Mock<Display>();
			var dummyFactory = new Mock<ReceiptFactory>();
			var itemService = new ItemService(registry, dummyDisplay.Object);
			var receiptService = new ReceiptService(dummyFactory.Object, dummyDisplay.Object);
			var sut = new PointOfSaleService(itemService, receiptService);
			var expected = registry.Read(barcode);

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
			var itemService = new ItemService(registry, sut.Object);
			var receiptService = new ReceiptService(dummy.Object, sut.Object);
			var sale = new PointOfSaleService(itemService, receiptService);
			var expected = registry.Read("123456");

			// Act
			sale.Scan("123456");

			// Assert
			sut.Verify(s => s.DisplayScannedItem(expected));
		}

		[Theory, AutoMoqData]
		public void CompleteSaleDisplaysReceiptAndClearsScannedItems(
			InMemoryItemRegistry registry,
			[Frozen] Mock<ReceiptFactory> stub,
			[Frozen] Mock<Display> sut)
		{
			// Arrange
			var itemService = new ItemService(registry, sut.Object);
			var receiptService = new ReceiptService(stub.Object, sut.Object);
			var sale = new PointOfSaleService(itemService, receiptService);
			sale.Scan("123456");
			var expected = new Receipt(sale.ScannedItems);
			stub.Setup(s => s.CreateReceiptFrom(sale.ScannedItems)).Returns(expected);

			// Act
			sale.OnCompleteSale();

			// Assert
			sut.Verify(s => s.DisplayReceipt(expected));
			sale.ScannedItems.Should().BeEmpty();
		}

		[Fact]
		public void ServiceCreatesOrderOnCompleteSale()
		{
			var registry = new InMemoryItemRegistry();
			var dummyDisplay = new Mock<Display>();
			var dummyFactory = new Mock<ReceiptFactory>();
			var itemService = new ItemService(registry, dummyDisplay.Object);
			var receiptService = new ReceiptService(dummyFactory.Object, dummyDisplay.Object);
			var sut = new Mock<OrderRepository>();
			var sale = new PointOfSaleService(itemService, receiptService, sut.Object);
			sale.Scan("123456");
			var expected = sale.ScannedItems.ToList();

			// Act
			sale.OnCompleteSale();

			// Assert
			sut.Verify(s => s.CreateOrder(11234, expected));
		}


	}

}
