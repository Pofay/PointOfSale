using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OrderSystem.Domain
{

    public class Sale
    {
        public decimal TotalPrice { get { return new decimal(totalPrice); } }

        private double totalPrice;
        private readonly ItemRepository repo;


        public Sale(ItemRepository repo)
        {
            this.repo = repo;
        }


        public void OnBarcode(string barcode)
        {
            totalPrice += repo.getPriceFor(barcode);
        }
    }

}