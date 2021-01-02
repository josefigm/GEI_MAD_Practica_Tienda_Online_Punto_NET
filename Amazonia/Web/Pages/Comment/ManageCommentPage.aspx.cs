using Es.Udc.DotNet.Amazonia.Model.CommentServiceImp;
using Es.Udc.DotNet.Amazonia.Model.CommentServiceImp.DTOs;
using Es.Udc.DotNet.Amazonia.Model.CommentServiceImp.Exceptions;
using Es.Udc.DotNet.Amazonia.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.Amazonia.Web.Pages.Comment
{
    public partial class ManageComment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblNoCommentYet.Visible = false;
            commentForm.Visible = false;
            lblCommentNotFound.Visible = false;
            lblNotAllowed.Visible = false;

            if (!IsPostBack)
            {
                long productId = Convert.ToInt64(Request.Params.Get("productId"));
                lblProductId.Text = productId.ToString();

                GetAndSetData(productId);
            }
        }

        private void GetAndSetData(long productId)
        {
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ICommentService commentService = iocManager.Resolve<ICommentService>();

            try
            {
                // Get data
                long currentClientId = SessionManager.GetUserSession(Context).UserProfileId;

                List<CommentDTO> commentList = commentService.FindCommentsOfProductAndClient(productId, currentClientId);

                if (commentList.Count == 0)
                {
                    lblNoCommentYet.Visible = true;
                    return;
                }

                CommentDTO comment = commentList[0];
                
                // Set data
                lblCommentId.Text = comment.id.ToString();
                tbTitle.Text = comment.title;
                tbValue.Text = comment.value;

                commentForm.Visible = true;

            }
            //If the product does not exist. This is not normal (Maybe a hacker changed the URL)
            catch (InstanceNotFoundException)
            {
                String url = String.Format("../Product/ViewProductPage.aspx?productId={0}", lblProductId.Text);
                Response.Redirect(Response.ApplyAppPathModifier(url));
            }
        }

        protected void btnUpdateComment_Click(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ICommentService commentService = iocManager.Resolve<ICommentService>();
            long currentClientId = SessionManager.GetUserSession(Context).UserProfileId;

            try
            {
                commentService.ChangeComment(Convert.ToInt64(lblCommentId.Text), tbTitle.Text, tbValue.Text, currentClientId);

                String url = String.Format("../Product/ViewProductPage.aspx?productId={0}", lblProductId.Text);
                Response.Redirect(Response.ApplyAppPathModifier(url));
            }
            catch (InstanceNotFoundException)
            {
                lblCommentNotFound.Visible = true;
            }
            catch (NotAllowedToChangeCommentException)
            {
                lblNotAllowed.Visible = true;
            }
        }

        protected void btnDeleteComment_Click(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ICommentService commentService = iocManager.Resolve<ICommentService>();
            long currentClientId = SessionManager.GetUserSession(Context).UserProfileId;

            try
            {
                commentService.RemoveComment(Convert.ToInt64(lblCommentId.Text), currentClientId);

                String url = String.Format("../Product/ViewProductPage.aspx?productId={0}", lblProductId.Text);
                Response.Redirect(Response.ApplyAppPathModifier(url));
            }
            catch (InstanceNotFoundException)
            {
                lblCommentNotFound.Visible = true;
            }
            catch (NotAllowedToChangeCommentException)
            {
                lblNotAllowed.Visible = true;
            }
        }
    }
}