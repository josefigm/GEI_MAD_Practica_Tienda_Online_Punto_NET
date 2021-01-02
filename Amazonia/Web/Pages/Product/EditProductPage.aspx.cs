using Es.Udc.DotNet.Amazonia.Model.ProductServiceImp;
using Es.Udc.DotNet.ModelUtil.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.Amazonia.Web.Pages.Product
{
    public partial class EditProductPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                long productId = Convert.ToInt64(Request.Params.Get("productId"));

                CompleteProductDTO productDTO = GetData(productId);

                SetData(productDTO);
            }
        }

        private CompleteProductDTO GetData(long productId)
        {
            IIoCManager iiocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IProductService productService = iiocManager.Resolve<IProductService>();

            CompleteProductDTO result = productService.FindProductById(productId);

            return result;
        }

        private void SetData(CompleteProductDTO productDTO)
        {
            lblProductId.Text = productDTO.id.ToString();
            tbProductName.Text = productDTO.name;
            tbPrice.Text = productDTO.price.ToString();
            tbDescription.Text = productDTO.description;
            tbStock.Text = productDTO.stock.ToString();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            IIoCManager iiocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IProductService productService = iiocManager.Resolve<IProductService>();

            productService.UpdateProduct(Convert.ToInt64(lblProductId.Text), tbProductName.Text, Convert.ToDouble(tbPrice.Text), Convert.ToInt64(tbStock.Text), tbDescription.Text);

            String url = String.Format("./ViewProductPage.aspx?productId={0}", lblProductId.Text);
            Response.Redirect(Response.ApplyAppPathModifier(url));
        }
    }
}