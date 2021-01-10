using Es.Udc.DotNet.Amazonia.Model.ProductServiceImp;
using Es.Udc.DotNet.Amazonia.Model.ProductServiceImp.DTOs;
using Es.Udc.DotNet.Amazonia.Model.SaleServiceImp;
using Es.Udc.DotNet.Amazonia.Model.SaleServiceImp.DTOs;
using Es.Udc.DotNet.Amazonia.Model.SaleServiceImp.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.Amazonia.Web.Pages.Product
{
    public partial class ProductsWithLabel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int startIndex, count;

            lnkPrevious.Visible = false;
            lnkNext.Visible = false;

            /* Get the keyword passed as parameter in the request from
                * the previous page
                */
            long labelId = Convert.ToInt64(Request.Params.Get("labelId"));

            /* Get Start Index */
            try
            {
                startIndex = Int32.Parse(Request.Params.Get("startIndex"));
            }
            catch (ArgumentNullException)
            {
                startIndex = 0;
            }

            /* Get Count */
            try
            {
                count = Int32.Parse(Request.Params.Get("count"));
            }
            catch (ArgumentNullException)
            {
                count = 3;
            }

            ProductBlock productBlock = GetData(labelId, startIndex, count);

            if (productBlock.products.Count == 0)
            {
                lblNoResults.Visible = true;
            }

            if (!IsPostBack)
            {
                SetRows(productBlock.products);
            }

            /* "Previous" link */
            if ((startIndex - count) >= 0)
            {
                String url = String.Format("./ProductsWithLabel.aspx?labelId={0}&startIndex={1}&count={2}", labelId, (startIndex - count), count);

                this.lnkPrevious.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkPrevious.Visible = true;
            }

            /* "Next" link */
            if (productBlock.ExistMoreProducts)
            {
                String url = String.Format("./ProductsWithLabel.aspx?labelId={0}&startIndex={1}&count={2}", labelId, (startIndex + count), count);

                this.lnkNext.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkNext.Visible = true;
            }

        }

        private ProductBlock GetData(long labelId, int startIndex, int count)
        {
            ProductBlock productBlock;
            IIoCManager iiocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IProductService productService = iiocManager.Resolve<IProductService>();

            // If the user selected a category the value will be different of -1

            productBlock = productService.RetrieveProductsWithLabel(startIndex, count, labelId);

            // Return the product block

            return productBlock;
        }

        private void SetRows(List<ProductDTO> products)
        {
            gvProducts.DataSource = products;
            gvProducts.DataBind();
        }

        protected void GvProducts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ShoppingCart shoppingCart;

            if (e.CommandName == "SeeDetail")
            {
                String url = String.Format("./ViewProductPage.aspx?productId={0}", e.CommandArgument);
                Response.Redirect(Response.ApplyAppPathModifier(url));
            }

            if (e.CommandName == "AddToCart")
            {
                try
                {
                    if (Session["shoppingCart"] != null)
                    {
                        shoppingCart = (ShoppingCart)Session["shoppingCart"];
                        Session["shoppingCart"] = AddToShoppingCart(shoppingCart, Convert.ToInt64(e.CommandArgument));
                    }
                    else
                    {
                        shoppingCart = new ShoppingCart();
                        Session.Add("shoppingCart", AddToShoppingCart(shoppingCart, Convert.ToInt64(e.CommandArgument)));
                    }

                    String url = "/Pages/Sale/ShoppingCartPage.aspx";
                    Response.Redirect(Response.ApplyAppPathModifier(url));
                }
                catch (InsufficientStockException)
                {
                    lblInsufficientStock.Visible = true;
                }

            }
        }

        private ShoppingCart AddToShoppingCart(ShoppingCart shoppingCart, long productId)
        {
            IIoCManager iiocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ISaleService saleService = iiocManager.Resolve<ISaleService>();

            return saleService.AddToShoppingCart(shoppingCart, productId, 1, false);
        }
    }
}