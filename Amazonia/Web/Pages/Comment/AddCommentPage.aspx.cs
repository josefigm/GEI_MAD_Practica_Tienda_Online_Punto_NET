using Es.Udc.DotNet.Amazonia.Model.CommentServiceImp;
using Es.Udc.DotNet.Amazonia.Model.CommentServiceImp.Exceptions;
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
    public partial class AddCommentPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblAlreadyCommentedError.Visible = false;

            if (!IsPostBack)
            {
                long productId = Convert.ToInt64(Request.Params.Get("productId"));
                lblProductId.Text = productId.ToString();
            }
        }

        protected void btnAddComment_Click(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ICommentService commentService = iocManager.Resolve<ICommentService>();

            try
            {
                commentService.AddComment(tbTitle.Text, tbValue.Text, Convert.ToInt64(lblProductId.Text), SessionManager.GetUserSession(Context).UserProfileId);

                String url = String.Format("../Product/ViewProductPage.aspx?productId={0}", lblProductId.Text);
                Response.Redirect(Response.ApplyAppPathModifier(url));
            }
            catch(AlreadyCommentedThisProduct)
            {
                lblAlreadyCommentedError.Visible = true;
            }
        }
    }
}