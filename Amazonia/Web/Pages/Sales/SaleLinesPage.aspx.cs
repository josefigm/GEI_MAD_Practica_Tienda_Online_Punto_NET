using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.Amazonia.Model.SaleServiceImp;
using Es.Udc.DotNet.Amazonia.Model.SaleServiceImp.DTOs;
using Es.Udc.DotNet.Amazonia.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.IoC;

namespace Es.Udc.DotNet.Amazonia.Web.Pages.Sales
{
    public partial class SaleLinesPage : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string saleIdString = Request.Params.Get("idSale");
                long saleId = System.Int64.Parse(saleIdString);

                List<SaleLineDTO> linesList = GetSales(saleId);

                GvListLinesSales.DataSource = linesList;
                GvListLinesSales.DataBind();
            }
        }

        private List<SaleLineDTO> GetSales(long saleId)
        {

            IIoCManager iiocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ISaleService saleService = iiocManager.Resolve<ISaleService>();

            return saleService.ShowSaleLines(saleId);
        }
    }
}