using System;
namespace PointOfSale.Domain
{
	public class Item
	{
		private double price;

		public string Name { get; private set; }
		public decimal Price { get { return new decimal(price); } }
		public string Barcode { get; private set; }

		public Item(string barcode, string name, double price)
		{
			this.Barcode = barcode;
			this.Name = name;
			this.price = price;
		}

		public override bool Equals(object obj)
		{
			var other = obj as Item;
			if (other.Barcode.Equals(this.Barcode))
				return true;
			return false;
		}
	}
}

