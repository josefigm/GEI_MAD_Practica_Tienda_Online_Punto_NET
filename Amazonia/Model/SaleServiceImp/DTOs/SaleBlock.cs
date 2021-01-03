using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.Amazonia.Model.SaleServiceImp.DTOs
{
    public class SaleBlock
    {

        public List<SaleListItemDTO> Sales { get; private set; }
        public bool ExistMoreSales { get; private set; }

        public SaleBlock(List<SaleListItemDTO> sales, bool existMoreSales)
        {
            this.Sales = sales;
            ExistMoreSales = existMoreSales;
        }


    }
}
