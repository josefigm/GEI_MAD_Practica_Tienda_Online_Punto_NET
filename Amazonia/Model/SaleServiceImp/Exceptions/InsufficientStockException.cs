using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.Amazonia.Model.SaleServiceImp.Exceptions
{
    [Serializable]
    public class InsufficientStockException : Exception
    {
        public InsufficientStockException(long stock, long units)
            : base("Insufficient stock exception => stock = " + stock + " units = " + units)
        {
            this.Units = units;
            this.Stock = stock;
        }

        public long Units { get; private set; }
        public long Stock { get; private set; }
    }
}
