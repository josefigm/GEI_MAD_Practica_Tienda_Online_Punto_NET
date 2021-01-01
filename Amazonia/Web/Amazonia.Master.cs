using Es.Udc.DotNet.Amazonia.Web.HTTP.Session;
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
        }
    }
}