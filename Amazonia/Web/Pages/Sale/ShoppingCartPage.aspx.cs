using Es.Udc.DotNet.Amazonia.Model.SaleServiceImp;
using Es.Udc.DotNet.Amazonia.Model.SaleServiceImp.DTOs;
using Es.Udc.DotNet.Amazonia.Model.SaleServiceImp.Exceptions;
using Es.Udc.DotNet.Amazonia.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.Amazonia.Web.Pages.Sale
{
    public partial class ShoppingCartPage : SpecificCulturePage
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

                    if (shoppingCart.items.Count == 0)
                    {
                        lclTotalPrice.Visible = false;
                        lclEmptyShoppingCart.Visible = true;
                    }
                    else
                    {
                        lclShoppingCart.Visible = true;
                        lnkEndSale.Visible = true;
                        lclTotalPrice.Visible = true;
                        totalPrice.Text = shoppingCart.totalPrice.ToString();
                    }
                   
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

            if (e.CommandName == "Modify")
            {
                try
                {
                    shoppingCart = (ShoppingCart)Session["shoppingCart"];

                    GridViewRow gvr = (GridViewRow)((Button)e.CommandSource).NamingContainer;

                    TextBox tbbUnits = (TextBox)GvShoppingCart.Rows[gvr.RowIndex].FindControl("tbUnits");
                    CheckBox cbGift = (CheckBox)GvShoppingCart.Rows[gvr.RowIndex].FindControl("cbGift");

                    Session["shoppingCart"] = ModifyItem(shoppingCart, Convert.ToInt64(e.CommandArgument.ToString()), Convert.ToInt64(tbbUnits.Text.ToString()), cbGift.Checked);

                    String url = "/Pages/Sale/ShoppingCartPage.aspx";
                    Response.Redirect(Response.ApplyAppPathModifier(url));
                }
                catch (InsufficientStockException)
                {
                    lblInsufficientStock.Visible = true;
                }
            }
            
        }

        private ShoppingCart DeleteFromShoppingCart(ShoppingCart shoppingCart, long productId)
        {
            IIoCManager iiocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ISaleService saleService = iiocManager.Resolve<ISaleService>();

            return saleService.DeleteFromShoppingCart(shoppingCart, productId);
        }

        private ShoppingCart ModifyItem(ShoppingCart shoppingCart, long productId, long units, bool gift)
        {
            IIoCManager iiocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ISaleService saleService = iiocManager.Resolve<ISaleService>();

            return saleService.ModifyShoppingCartItem(shoppingCart, productId, units, gift);
        }
    }
}