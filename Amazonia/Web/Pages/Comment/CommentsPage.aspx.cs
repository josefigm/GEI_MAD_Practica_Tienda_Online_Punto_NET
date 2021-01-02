using Es.Udc.DotNet.Amazonia.Model.CommentServiceImp;
using Es.Udc.DotNet.Amazonia.Model.CommentServiceImp.DTOs;
using Es.Udc.DotNet.ModelUtil.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.Amazonia.Web.Pages.Comment
{
    public partial class CommentsPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int startIndex, count;

            lnkPrevious.Visible = false;
            lnkNext.Visible = false;

            long productId = Convert.ToInt64(Request.Params.Get("productId"));

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

            CommentBlock commentBlock = GetData(productId, startIndex, count);

            if (commentBlock.Comments.Count == 0)
            {
                lblNoResults.Visible = true;
            }

            if (!IsPostBack)
            {
                SetRows(commentBlock.Comments);
            }

            /* "Previous" link */
            if ((startIndex - count) >= 0)
            {
                String url = String.Format("./CommentsPage.aspx?productId={0}&startIndex={1}&count={2}", productId, (startIndex - count), count);

                this.lnkPrevious.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkPrevious.Visible = true;
            }

            /* "Next" link */
            if (commentBlock.ExistMoreComments)
            {
                String url = String.Format("./CommentsPage.aspx?productId={0}&startIndex={1}&count={2}", productId, (startIndex + count), count);

                this.lnkNext.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkNext.Visible = true;
            }
        }

        private CommentBlock GetData(long productId, int startIndex, int count)
        {
            CommentBlock commentBlock;
            IIoCManager iiocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ICommentService commentService = iiocManager.Resolve<ICommentService>();

            commentBlock = commentService.FindCommentsOfProduct(productId, startIndex, count);

            // Return the product block

            return commentBlock;
        }

        private void SetRows(List<CommentDTO> products)
        {
            gvComments.DataSource = products;
            gvComments.DataBind();
        }
    }
}