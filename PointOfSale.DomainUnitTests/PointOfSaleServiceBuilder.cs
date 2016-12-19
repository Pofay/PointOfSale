using System;
using Moq;
using PointOfSale.Domain;

namespace PointOfSale.DomainUnitTests
{
	public class PointOfSaleServiceBuilder
	{

		ScanBarcodeQuery query;
		TransactionIdGenerator generator;
		Display display;

		public PointOfSaleServiceBuilder()
		{
			this.query = new InMemoryItemRegistry();
			this.generator = new Mock<TransactionIdGenerator>().Object;
			this.display = new Mock<Display>().Object;
		}

		public PointOfSaleServiceBuilder WithDisplay(Display display)
		{
			this.display = display;
			return this;
		}

		public PointOfSaleServiceBuilder WithGenerator(TransactionIdGenerator generator)
		{
			this.generator = generator;
			return this;
		}

		public PointOfSaleServiceBuilder WithQuery(ScanBarcodeQuery query)
		{
			this.query = query;
			return this;
		}

		public PointOfSaleService Build()
		{
			var service = new PointOfSaleService(query, generator);
			service.BarcodeEvent += display.BarcodeHandler;
			service.CompleteSaleEvent += display.CompleteSaleHandler;
			return service;
		}


	}
}
