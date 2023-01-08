using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Infrastructure
{
    public static class DecimalExtensions
    {
        public static string GetPriceString(this double value) =>
            $"$ {value:N2}";
    }
}
