using System;
using System.Collections.Generic;

namespace PointOfSale.Domain
{
	public class CompleteSaleEventArgs : EventArgs
	{
		public int Id { get; }
		public IEnumerable<Item> Items { get; }

		public CompleteSaleEventArgs(int id, IEnumerable<Item> items)
		{
			this.Id = id;
			this.Items = items;
		}

		public override bool Equals(object obj)
		{
			var other = obj as CompleteSaleEventArgs;
			return this.Id == other.Id;
		}
	}
}