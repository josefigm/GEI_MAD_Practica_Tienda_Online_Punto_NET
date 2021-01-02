using Es.Udc.DotNet.Amazonia.Model.ClientServiceImp.Exceptions;
using Es.Udc.DotNet.Amazonia.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.Amazonia.Web.Pages.User
{
    public partial class WebForm1 : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblPasswordError.Visible = false;
            lblLoginError.Visible = false;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    SessionManager.Login(Context, tbLogin.Text, tbPassword.Text, checkPassword.Checked);
                    FormsAuthentication.RedirectFromLoginPage(tbLogin.Text, checkPassword.Checked);
                }
                catch (InstanceNotFoundException)
                {
                    lblLoginError.Visible = true;
                }
                catch (IncorrectPasswordException)
                {
                    lblPasswordError.Visible = true;
                }
            }
        }
    }
}