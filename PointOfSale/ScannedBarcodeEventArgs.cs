using System;

namespace PointOfSale.Domain
{
	public class ScannedBarcodeEventArgs : EventArgs
	{
		public Item ReadItem { get; }

		public ScannedBarcodeEventArgs(Item item)
		{
			this.ReadItem = item;
		}

		public override bool Equals(object obj)
		{
			var other = obj as ScannedBarcodeEventArgs;
			return other.ReadItem.Barcode.Equals(this.ReadItem.Barcode);
		}
	}
}