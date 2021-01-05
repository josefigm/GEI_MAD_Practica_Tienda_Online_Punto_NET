using Es.Udc.DotNet.Amazonia.Model.LabelServiceImp;
using Es.Udc.DotNet.Amazonia.Model.LabelServiceImp.DTOs;
using Es.Udc.DotNet.Amazonia.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.Amazonia.Web
{
    public partial class Amazonia : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SessionManager.IsUserAuthenticated(Context))
            {
                if (lnkUpdate != null)
                {
                    lnkUpdate.Visible = false;
                }

                if (lblDash3 != null)
                {
                    lblDash3.Visible = false;
                }

                if (lnkLogout != null)
                {
                    lnkLogout.Visible = false;
                }

                if (lblDash4 != null)
                {
                    lblDash4.Visible = false;
                }

                if (lnkSales != null)
                {
                    lblDash5.Visible = false;
                    lnkSales.Visible = false;
                }

            }
            else
            {
                if (lblWelcome != null)
                {
                    lblWelcome.Text = GetLocalResourceObject("lblWelcome.Hello.Text").ToString()
                        + " " + SessionManager.GetUserSession(Context).FirstName;
                }
                
                if (lnkAuthenticate != null)
                {
                    lnkAuthenticate.Visible = false;
                }

                if (lblDash2 != null)
                {
                    lblDash2.Visible = false;
                }
            }
            lblNoLabels.Visible = true;
            SetLabelCloud();
        }

        private List<LabelDTO> GetMostUsedLabels()
        {
            IIoCManager iiocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ILabelService labelService = iiocManager.Resolve<ILabelService>();

            List<LabelDTO> mostUsedLabels = labelService.FindMostUsedLabels(5);

            return mostUsedLabels;
        }

        private List<int> GetNumberOfComments(List<LabelDTO> labels)
        {
            IIoCManager iiocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ILabelService labelService = iiocManager.Resolve<ILabelService>();

            List<long> labelIds = labels.Select(l => l.id).ToList();

            List<int> numberOfComments = labelService.GetNumberOfComments(labelIds);

            return numberOfComments;
        }

        private void SetLabelCloud()
        {
            List<LabelDTO> mostUsedLabels = GetMostUsedLabels();
            List<int> numberOfComments = GetNumberOfComments(mostUsedLabels);

            if (numberOfComments.Count != 0 && numberOfComments[0] != 0)
            {
                lblNoLabels.Visible = false;

                List<int> sizeOfLabel = GetSizes(numberOfComments);

                for (int i = 0; i < mostUsedLabels.Count; i++)
                {
                    HyperLink link = new HyperLink();
                    link.Text = mostUsedLabels[i].value;
                    link.NavigateUrl = String.Format("./Pages/Product/ProductsWithLabel.aspx?labelId={0}&startIndex={1}&count={2}", mostUsedLabels[i].id, 0, 5);
                    link.Font.Size = sizeOfLabel[i];
                    ContentPlaceHolder1.Controls.Add(link);
                    ContentPlaceHolder1.Controls.Add(new Literal() { Text = "&nbsp;&nbsp;" });
                    if (i == 1 || i == 3)
                    {
                        ContentPlaceHolder1.Controls.Add(new Literal() { Text = "<br>" });
                    }
                }
            }
        }

        private List<int> GetSizes(List<int> numberOfCommentsOfLabel)
        {
            int MAX_SIZE = 25;
            int MIN_SIZE = 8;

            int div = MAX_SIZE - MIN_SIZE;

            int max;
            
            max = numberOfCommentsOfLabel[0];
            
            List<int> resultado = new List<int>();
            resultado.Add(MAX_SIZE);

            for (int i = 1; i < numberOfCommentsOfLabel.Count; i++)
            {
                int tamaño = numberOfCommentsOfLabel[i] * div / max;
                resultado.Add(tamaño + MIN_SIZE);
            }
            return resultado;
        }
    }
}