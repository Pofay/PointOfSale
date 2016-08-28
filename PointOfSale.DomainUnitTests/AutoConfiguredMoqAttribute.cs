using System;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Xunit;

namespace PointOfSale.DomainUnitTests
{
	public class AutoConfiguredMoqAttribute : AutoDataAttribute
	{
		public AutoConfiguredMoqAttribute() : base(new Fixture().Customize(new AutoConfiguredMoqCustomization()))
		{
		}
	}
}