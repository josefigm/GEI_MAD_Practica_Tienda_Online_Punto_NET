using Es.Udc.DotNet.Amazonia.Model;
using Es.Udc.DotNet.Amazonia.Model.ProductServiceImp;
using Es.Udc.DotNet.Amazonia.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.Amazonia.Web.Pages.Product
{
    public partial class ViewProductPage : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                long productId = Convert.ToInt64(Request.Params.Get("productId"));
                lblProductId.Text = productId.ToString();

                CompleteProductDTO product = GetProduct(productId);

                productName.Text = product.name;
                productCategory.Text = product.categoryName;
                productPrice.Text = product.price.ToString() + " €";
                entryDate.Text = product.entryDate.ToShortDateString();
                stock.Text = product.stock.ToString();

                if (product.image != null)
                {
                    //productImage.ImageUrl = Server.MapPath("~/images/img.jpg");
                    productImage.ImageUrl = "./noImg.jpg";
                }
                else
                {
                    productImage.ImageUrl = "./noImg.jpg";
                    //productImage.ImageUrl = "../../../images/img.jpg";
                    //productImage.ImageUrl = Server.MapPath("~/images/img.jpg");
                }

                // A role equal to 1 indicates that the user is an administrator.
                // If a user is an administrator, the user can edit the product info.
                if (SessionManager.IsUserAuthenticated(Context) && SessionManager.GetUserSession(Context).Role == 1)
                {
                    btnEditProduct.Visible = true;
                }

                if (product.description != null)
                {
                    lclDescription.Visible = true;
                    productDescription.Text = product.description;
                }
                else
                {
                    lblNoDescription.Visible = true;
                }
            }
        }

        private CompleteProductDTO GetProduct(long id)
        {
            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IProductService productService = iocManager.Resolve<IProductService>();

            return productService.FindProductById(id);
        }

        protected void btnEditProduct_Click(object sender, EventArgs e)
        {
            String url = String.Format("./EditProductPage.aspx?productId={0}", lblProductId.Text);
            Response.Redirect(Response.ApplyAppPathModifier(url));
        }
    }
}