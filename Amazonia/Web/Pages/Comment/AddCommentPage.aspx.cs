using Es.Udc.DotNet.Amazonia.Model.CommentServiceImp;
using Es.Udc.DotNet.Amazonia.Model.CommentServiceImp.Exceptions;
using Es.Udc.DotNet.Amazonia.Model.LabelServiceImp;
using Es.Udc.DotNet.Amazonia.Web.HTTP.Session;
using Es.Udc.DotNet.Amazonia.Web.HTTP.View.ApplicationObjects;
using Es.Udc.DotNet.ModelUtil.Exceptions;
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
                UpdateCheckboxList();
            }

        }

        protected void btnAddComment_Click(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ICommentService commentService = iocManager.Resolve<ICommentService>();
            ILabelService labelService = iocManager.Resolve<ILabelService>();

            try
            {
                List<long> labels = getSelectedLabels();

                long commentId = commentService.AddComment(tbTitle.Text, tbValue.Text, Convert.ToInt64(lblProductId.Text), SessionManager.GetUserSession(Context).UserProfileId);

                labelService.AssignLabelsToComment(commentId, labels);

                String url = String.Format("../Product/ViewProductPage.aspx?productId={0}", lblProductId.Text);
                Response.Redirect(Response.ApplyAppPathModifier(url));
            }
            catch(AlreadyCommentedThisProduct)
            {
                lblAlreadyCommentedError.Visible = true;
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
                catch (DuplicateInstanceException)
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
        }


        private List<long> getSelectedLabels()
        {
            List<long> selectedValues = CheckBoxList1.Items.Cast<ListItem>()
               .Where(li => li.Selected)
               .Select(li => Convert.ToInt64(li.Value))
               .ToList();

            return selectedValues;
        }
        
    }
}