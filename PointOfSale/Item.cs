using System;
namespace PointOfSale.Domain
{
	public class Item
	{
		private double price;

		public string Name { get; private set; }
		public decimal Price { get { return new decimal(price); } }

		// This needs to be extracted as a Domain Concept, equals() method is screaming for it.
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

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			return string.Format("[Item: Name={0}, Price={1}, Barcode={2}]", Name, Price, Barcode);
		}
	}
}

