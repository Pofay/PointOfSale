using System;

namespace PointOfSale.Domain
{
	public class ScanEventArgs : EventArgs
	{
		public Item ReadItem { get; }

		public ScanEventArgs(Item item)
		{
			this.ReadItem = item;
		}

		public override bool Equals(object obj)
		{
			var other = obj as ScanEventArgs;
			return other.ReadItem.Barcode.Equals(this.ReadItem.Barcode);
		}
	}
}