using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.Amazonia.Web.Pages.Sale
{
    public partial class FinishedSalePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            long saleId = Convert.ToInt64(Request.Params.Get("saleId"));

            lclSaleId.Text = saleId.ToString();
        }
    }
}