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
			var registry = new InMemoryItemRegistry();
			var dummyDisplay = new Mock<Display>();
			var dummyFactory = new Mock<ReceiptFactory>();
			var dummyRepo = new Mock<OrderRepository>();
			var itemService = new ItemService(registry, dummyDisplay.Object);
			var receiptService = new ReceiptService(dummyFactory.Object, dummyDisplay.Object);
			var sut = new PointOfSaleService(itemService, receiptService, dummyRepo.Object);

			// Act
			sut.Scan(barcode);

			// Assert
			Assert.Equal(expected, sut.SubTotal, numOfDecimalPlaces);
		}

		[Theory, AutoMoqData]
		public void GetTotalPriceOnEmptyBarcodeReturns0Price(
			InMemoryItemRegistry registry,
			Mock<Display> dummyDisplay,
			Mock<ReceiptFactory> dummyFactory,
			Mock<OrderRepository> dummyRepo,
			Mock<TransactionIdGenerator> dummyGenerator)
		{
			// Arrange
			var itemService = new ItemService(registry, dummyDisplay.Object);
			var receiptService = new ReceiptService(dummyFactory.Object, dummyDisplay.Object);
			var sut = new PointOfSaleService(itemService, receiptService, dummyRepo.Object, dummyGenerator.Object);
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
			var dummyRepo = new Mock<OrderRepository>();
			var dummyGenerator = new Mock<TransactionIdGenerator>();
			var itemService = new ItemService(registry, dummyDisplay.Object);
			var receiptService = new ReceiptService(dummyFactory.Object, dummyDisplay.Object);
			var sut = new PointOfSaleService(itemService, receiptService, dummyRepo.Object, dummyGenerator.Object);
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
			Mock<OrderRepository> dummyRepo,
			Mock<ReceiptFactory> dummyFactory,
			Mock<TransactionIdGenerator> dummyGenerator)
		{
			// Arrange
			var itemService = new ItemService(registry, sut.Object);
			var receiptService = new ReceiptService(dummyFactory.Object, sut.Object);
			var sale = new PointOfSaleService(itemService, receiptService, dummyRepo.Object, dummyGenerator.Object);
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
			Mock<OrderRepository> dummy,
			[Frozen] Mock<Display> sut,
			Mock<TransactionIdGenerator> dummyGenerator)
		{
			// Arrange
			var itemService = new ItemService(registry, sut.Object);
			var receiptService = new ReceiptService(stub.Object, sut.Object);
			var sale = new PointOfSaleService(itemService, receiptService, dummy.Object, dummyGenerator.Object);
			sale.Scan("123456");
			var expected = new Receipt(sale.ScannedItems);
			stub.Setup(s => s.CreateReceiptFrom(sale.ScannedItems)).Returns(expected);

			// Act
			sale.OnCompleteSale();

			// Assert
			sut.Verify(s => s.DisplayReceipt(expected));
			sale.ScannedItems.Should().BeEmpty();
		}

		[Theory]
		[InlineData("123456", 11234)]
		[InlineData("456789", 44556)]
		public void ServiceCreatesOrderOnCompleteSale(string barcode, int transactionId)
		{
			var registry = new InMemoryItemRegistry();
			var dummyDisplay = new Mock<Display>();
			var dummyFactory = new Mock<ReceiptFactory>();
			var stub = new Mock<TransactionIdGenerator>();
			var itemService = new ItemService(registry, dummyDisplay.Object);
			var receiptService = new ReceiptService(dummyFactory.Object, dummyDisplay.Object);
			var sut = new Mock<OrderRepository>();
			stub.Setup(s => s.GenerateTransactionId()).Returns(transactionId);
			var sale = new PointOfSaleService(itemService, receiptService, sut.Object, stub.Object);
			sale.Scan(barcode);
			var expected = sale.ScannedItems.ToList();

			// Act
			sale.OnCompleteSale();

			// Assert
			sut.Verify(s => s.CreateOrder(transactionId, expected));
		}


	}

}
