using System;
using Es.Udc.DotNet.Amazonia.Model.ClientServiceImp.Exceptions;
using Es.Udc.DotNet.Amazonia.Web.HTTP.Session;

namespace Es.Udc.DotNet.Amazonia.Web.Pages.User
{
    public partial class ChangePasswordPage : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblOldPasswordError.Visible = false;
        }

        /// <summary>
        /// Handles the Click event of the btnChangePassword control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance 
        /// containing the event data.</param>
        protected void BtnChangePasswordClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {

                    SessionManager.ChangePassword(Context, tbOldPassword.Text,
                        tbNewPassword.Text);

                    Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/MainPage.aspx"));

                }
                catch (IncorrectPasswordException)
                {
                    lblOldPasswordError.Visible = true;
                }
            }
        }

    }
}