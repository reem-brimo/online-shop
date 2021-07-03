using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public int Num { get; set; }
        public string Descripion { get; set; }

        public Product Product { get; set; }
        public int ProductId { get; set; }
    }
}
