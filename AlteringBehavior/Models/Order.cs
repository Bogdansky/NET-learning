using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlteringBehavior.Enums;

namespace AlteringBehavior.Models
{
    public class Order
    {
        public Order(ShipmentOptions shipmentOptions, ProductType product, double weight)
        {
            ShipmentOptions = shipmentOptions;
            Product = product;
            Weight = weight;
        }

        public ShipmentOptions ShipmentOptions { get; }
        public ProductType Product { get; }
        public double Weight { get; }
    }
}
