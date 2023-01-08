using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class CartProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public int StockId { get; set; }
        public double Price { get; set; }
        public int Num { get; set; }
    }
}
