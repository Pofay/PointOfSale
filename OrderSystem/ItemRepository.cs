using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OrderSystem.Domain
{

    public class ItemRepository
    {
        Dictionary<string, double> itemPrices;


        public ItemRepository()
        {
            itemPrices = new Dictionary<string, double>();
            itemPrices.Add("123456", 12.50);
            itemPrices.Add("900000", 7.50);
            itemPrices.Add("456789", 24.50);
        }

        public double getPriceFor(string barcode)
        {
            return itemPrices.ContainsKey(barcode) ? itemPrices[barcode] : 0.0;
        }
    }

}