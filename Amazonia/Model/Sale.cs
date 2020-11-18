//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Es.Udc.DotNet.Amazonia.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sale
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sale()
        {
            this.SaleLines = new HashSet<SaleLine>();
        }
    
        public long id { get; set; }
        public System.DateTime date { get; set; }
        public string address { get; set; }
        public double totalPrice { get; set; }
        public string cardNumber { get; set; }
        public string clientLogin { get; set; }
    
        public virtual Card Card { get; set; }
        public virtual Client Client { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SaleLine> SaleLines { get; set; }
    }
}
