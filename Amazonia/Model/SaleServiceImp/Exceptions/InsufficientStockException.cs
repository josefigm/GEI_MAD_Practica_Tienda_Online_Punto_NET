using System;

namespace Es.Udc.DotNet.Amazonia.Model.SaleServiceImp.Exceptions
{
    /// <summary>Stock de un producto menor a las unidades solicitadas para este</summary>
    [Serializable]
    public class InsufficientStockException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="InsufficientStockException" /> class.</summary>
        /// <param name="stock">The stock.</param>
        /// <param name="units">The units.</param>
        public InsufficientStockException(String name, long stock, long units)
            : base("Insufficient stock for " + name + " => stock = " + stock + " units = " + units)
        {
            this.Units = units;
            this.Stock = stock;
            this.Name = name;
        }

        /// <summary>Gets the required units of the product.</summary>
        /// <value>The required units of the product.</value>
        public long Units { get; private set; }

        /// <summary>Gets the stock of the product.</summary>
        /// <value>The stock  of the product.</value>
        public long Stock { get; private set; }

        /// <summary>Gets the nanme of the product.</summary>
        /// <value>The name  of the product.</value>
        public String Name { get; private set; }
    }
}