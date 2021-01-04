using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.Amazonia.Web.Pages.Sale
{
    public partial class StockErrorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String name = Request.Params.Get("productName");
            String stock = Request.Params.Get("stock");

            lclProductName.Text = name;
            lclStock.Text = stock;
        }
    }
}