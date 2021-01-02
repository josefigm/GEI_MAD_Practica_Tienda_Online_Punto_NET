using Es.Udc.DotNet.Amazonia.Model.SaleServiceImp;
using Es.Udc.DotNet.Amazonia.Model.SaleServiceImp.DTOs;
using Es.Udc.DotNet.ModelUtil.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.Amazonia.Web.Pages.Sale
{
    public partial class ShoppingCartPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["shoppingCart"] != null)
                {
                    ShoppingCart shoppingCart = (ShoppingCart)Session["shoppingCart"];

                    GvShoppingCart.DataSource = shoppingCart.items;
                    GvShoppingCart.DataBind();
                }
                else
                {
                    lclEmptyShoppingCart.Visible = true;
                }
            }
        }

        protected void GvShoppingCart_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ShoppingCart shoppingCart;

            if (e.CommandName == "Delete")
            {
                shoppingCart = (ShoppingCart)Session["shoppingCart"];
                Session["shoppingCart"] = DeleteFromShoppingCart(shoppingCart, Convert.ToInt64(e.CommandArgument));


                String url = "/Pages/Sale/ShoppingCartPage.aspx";
                Response.Redirect(Response.ApplyAppPathModifier(url));
            }
        }

        private ShoppingCart DeleteFromShoppingCart(ShoppingCart shoppingCart, long productId)
        {
            IIoCManager iiocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ISaleService saleService = iiocManager.Resolve<ISaleService>();

            return saleService.DeleteFromShoppingCart(shoppingCart, productId);
        }

 //       protected void GvShoppingCart_RowDeleting(Object sender, GridViewDeleteEventArgs e)
 //       {
 //           GvShoppingCart.DeleteRow(e.RowIndex);
  //          GvShoppingCart.DataBind();
   //     }
    }
}