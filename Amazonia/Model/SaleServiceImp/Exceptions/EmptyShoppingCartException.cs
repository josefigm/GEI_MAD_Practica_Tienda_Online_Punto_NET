using System;

namespace Es.Udc.DotNet.Amazonia.Model.SaleServiceImp.Exceptions
{
    /// <summary>Exception trying to buy an empty shopping cart</summary>
    [Serializable]
    public class EmptyShoppingCartException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="EmptyShoppingCartException" /> class.</summary>
        public EmptyShoppingCartException()
            : base("Can't buy an empty shopping cart")
        {
        }
    }
}