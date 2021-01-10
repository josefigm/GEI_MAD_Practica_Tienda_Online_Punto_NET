using Es.Udc.DotNet.Amazonia.Model;
using Es.Udc.DotNet.Amazonia.Model.CommentServiceImp;
using Es.Udc.DotNet.Amazonia.Model.CommentServiceImp.DTOs;
using Es.Udc.DotNet.Amazonia.Model.ProductServiceImp;
using Es.Udc.DotNet.Amazonia.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.IoC;
using System;
using System.Collections.Generic;
using System.IO;
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
                productImage.ImageUrl = "./noImg.jpg";
 
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

            // Every time the existence of comments is checked
            btnReadComments.Visible = false;
            ComprobarComentarios(Convert.ToInt64(lblProductId.Text));
        }

        // If there are no comments, view comments button should not be displayed
        private void ComprobarComentarios(long productId)
        {
            CommentBlock commentBlock;
            IIoCManager iiocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ICommentService commentService = iiocManager.Resolve<ICommentService>();

            commentBlock = commentService.FindCommentsOfProduct(productId, 0, 1);

            if (commentBlock.Comments.Count > 0)
            {
                btnReadComments.Visible = true;
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

        protected void btnAddComent_Click(object sender, EventArgs e)
        {
            String url = String.Format("../Comment/AddCommentPage.aspx?productId={0}", lblProductId.Text);
            Response.Redirect(Response.ApplyAppPathModifier(url));
        }

        protected void btnManageComment_Click(object sender, EventArgs e)
        {
            String url = String.Format("../Comment/ManageCommentPage.aspx?productId={0}", lblProductId.Text);
            Response.Redirect(Response.ApplyAppPathModifier(url));
        }

        protected void btnReadComments_Click(object sender, EventArgs e)
        {
            String url = String.Format("../Comment/CommentsPage.aspx?productId={0}&startIndex={1}&count={2}", lblProductId.Text, 0, 5);
            Response.Redirect(Response.ApplyAppPathModifier(url));
        }
    }
}