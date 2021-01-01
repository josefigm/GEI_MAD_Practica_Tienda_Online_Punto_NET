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
            long productId = Convert.ToInt64(Request.Params.Get("productId"));

            CompleteProductDTO product = GetProduct(productId);

            productName.Text = product.name;
            productCategory.Text = product.categoryName;
            productPrice.Text = product.price.ToString() + " €";
            entryDate.Text = product.entryDate.ToShortDateString();

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

        private CompleteProductDTO GetProduct(long id)
        {
            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IProductService productService = iocManager.Resolve<IProductService>();

            return productService.FindProductById(id);
        }
    }
}