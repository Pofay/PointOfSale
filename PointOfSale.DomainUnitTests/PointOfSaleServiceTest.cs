using System.Linq;
using FluentAssertions;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Kernel;
using Ploeh.AutoFixture.Xunit;
using PointOfSale.Domain;
using Xunit;
using Xunit.Extensions;

namespace PointOfSale.DomainUnitTests
{
	public class PointOfSaleServiceTest
	{
		int numOfDecimalPlaces = 1;

		readonly Fixture fixture;

		public PointOfSaleServiceTest()
		{
			fixture = new Fixture();
			fixture.Customize(new AutoConfiguredMoqCustomization());
			fixture.Customizations.Add(new TypeRelay(typeof(ScanBarcodeQuery), typeof(InMemoryItemRegistry)));
		}

		[Fact]
		public void GetTotalPriceOnEmptyBarcodeReturns0Price()
		{
			// Arrange
			var sut = fixture.Create<PointOfSaleService>();
			sut.OnScan += delegate { };
			string emptyBarcode = "";

			// Act
			sut.Scan(emptyBarcode);

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
			var sut = fixture.Create<PointOfSaleService>();
			sut.OnScan += delegate { };
			var expected = new decimal(price);

			// Act
			sut.Scan(barcode);

			// Assert
			Assert.Equal(expected, sut.SubTotal, numOfDecimalPlaces);
		}


		[Theory]
		[InlineData("123456")]
		public void ItemWithRegisteredBarcodeIsStoredInsideScannedItems(string barcode)
		{
			// Arrange
			var query = fixture.Create<ScanBarcodeQuery>();
			var sut = fixture.Create<PointOfSaleService>();
			sut.OnScan += delegate { };
			var expected = query.Read(barcode);

			// Act
			sut.Scan(barcode);

			// Assert
			sut.ScannedItems.Should().Contain(expected);
		}

		[Theory]
		[InlineData(11234)]
		[InlineData(44556)]
		public void ServiceCreatesOrderOnCompleteSale(int transactionId)
		{
			// Arrange
			var stub = fixture.Freeze<Mock<TransactionIdGenerator>>();
			var sut = fixture.Freeze<Mock<CompleteSaleCommand>>();
			var sale = fixture.Create<PointOfSaleService>();
			sale.OnScan += delegate { };
			stub.Setup(s => s.GenerateTransactionId()).Returns(transactionId);

			// Act
			sale.CompleteSale();

			// Assert
			sut.Verify(s => s.Execute(sale.ScannedItems));
		}
	}

}
