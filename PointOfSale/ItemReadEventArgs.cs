using System;

namespace PointOfSale.Domain
{
	public class ItemReadEventArgs : EventArgs
	{
		public Item ReadItem { get; }

		public ItemReadEventArgs(Item item)
		{
			this.ReadItem = item;
		}

		public override bool Equals(object obj)
		{
			var other = obj as ItemReadEventArgs;
			return other.ReadItem.Barcode.Equals(this.ReadItem.Barcode);
		}
	}
}