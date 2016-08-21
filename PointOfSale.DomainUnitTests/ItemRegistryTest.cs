using System;
using System.Collections.Generic;
using FluentAssertions;
using PointOfSale.Domain;
using Xunit;

namespace PointOfSale.DomainUnitTests
{
	public class ItemRegistryTest
	{
		[Fact]
		public void ItShouldBeAbleToReturnAllItemsStored()
		{
			var registry = new InMemoryItemRegistry();
			var expected = new List<Item>();
			expected.Add(new Item("123456", "Bowl", 12.50));
			expected.Add(new Item("900000", "Phone", 7.50));
			expected.Add(new Item("456789", "Crab", 24.50));
			expected.Add(new Item("345670", "Plunger", 6.50));
			expected.Add(new Item("789010", "Fish", 10.25));

			var actual = registry.getAvailableItems();


			actual.ShouldBeEquivalentTo(expected);
		}

		[Fact]
		public void basicTest()
		{

		}
	}
}

