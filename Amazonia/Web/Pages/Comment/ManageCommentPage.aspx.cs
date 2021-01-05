using Es.Udc.DotNet.Amazonia.Model.CommentServiceImp;
using Es.Udc.DotNet.Amazonia.Model.CommentServiceImp.DTOs;
using Es.Udc.DotNet.Amazonia.Model.CommentServiceImp.Exceptions;
using Es.Udc.DotNet.Amazonia.Model.LabelServiceImp;
using Es.Udc.DotNet.Amazonia.Model.LabelServiceImp.DTOs;
using Es.Udc.DotNet.Amazonia.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.IoC;
using System;
using System.Collections;
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
            lblCommentNotFound.Visible = false;
            lblNotAllowed.Visible = false;

            if (!IsPostBack)
            {
                commentForm.Visible = false;

                long productId = Convert.ToInt64(Request.Params.Get("productId"));
                lblProductId.Text = productId.ToString();

                GetAndSetData(productId);

                UpdateCheckboxList();
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
            ILabelService labelService = iocManager.Resolve<ILabelService>();
            long currentClientId = SessionManager.GetUserSession(Context).UserProfileId;

            try
            {
                List<long> labels = getSelectedLabels();

                commentService.ChangeComment(Convert.ToInt64(lblCommentId.Text), tbTitle.Text, tbValue.Text, currentClientId);
                labelService.AssignLabelsToComment(Convert.ToInt64(lblCommentId.Text), labels);

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

        protected void btnCrateLabel_Click(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ILabelService labelService = iocManager.Resolve<ILabelService>();

            if (tbNewLabel.Text != "")
            {
                try
                {

                    labelService.CreateLabel(tbNewLabel.Text);
                    tbNewLabel.Text = "";
                    UpdateCheckboxList();
                }
                catch (ModelUtil.Exceptions.DuplicateInstanceException)
                {
                    lblRequiredTb.Visible = false;
                    lblDuplicatedLabel.Visible = true;
                }
            }
            else
            {
                lblDuplicatedLabel.Visible = false;
                lblRequiredTb.Visible = true;
            }

        }

        private void UpdateCheckboxList()
        {
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ILabelService labelService = iocManager.Resolve<ILabelService>();

            this.CheckBoxList1.DataSource = labelService.FindAllLabels();
            this.CheckBoxList1.DataTextField = "value";
            this.CheckBoxList1.DataValueField = "id";
            this.CheckBoxList1.DataBind();

            List<LabelDTO> commentLabels = labelService.FindLabelsByComment(Convert.ToInt64(lblCommentId.Text));
            markChecked(commentLabels);
        }

        private List<long> getSelectedLabels()
        {
            List<long> selectedValues = CheckBoxList1.Items.Cast<ListItem>()
               .Where(li => li.Selected)
               .Select(li => Convert.ToInt64(li.Value))
               .ToList();

            return selectedValues;
        }

        private void markChecked(List<LabelDTO> commentLabels)
        {
            List<LabelDTO>.Enumerator labelEnum = commentLabels.GetEnumerator();
            IEnumerator checkListEnum = CheckBoxList1.Items.GetEnumerator();

            while (labelEnum.MoveNext())
            {
                LabelDTO currentLabel = labelEnum.Current;
                while (checkListEnum.MoveNext())
                {
                    ListItem currentCLElement = (ListItem)checkListEnum.Current;
                    if (currentCLElement.Text == currentLabel.value)
                    {
                        currentCLElement.Selected = true;
                        break;
                    }

                }
            }
        }
    }
}