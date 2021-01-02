using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.Amazonia.Model.CardServiceImp;
using Es.Udc.DotNet.Amazonia.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.Amazonia.Web.Pages.User
{
    public partial class CreateNewCardPage : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblNumberError.Visible = false;

        }

        protected void btnCreateCard_Click(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {

                try
                {
                    string monthString = tbExpireDateMonth.Text;
                    int month = (int) Int64.Parse(monthString);

                    string yearString = tbExpireDateYear.Text;
                    int year = (int) Int64.Parse(yearString);

                    DateTime expirationDate = new DateTime(year,month,1);

                    String cardTypeString = this.ddlType.SelectedValue;
                    bool cardType = false;

                    if (cardTypeString == "debitCard")
                    {
                        cardType = true;
                    }

                    CardDTO cardDTO = new CardDTO(tbNumber.Text, tbCVV.Text, expirationDate, cardType, false);

                    SessionManager.CreateNewCardToClient(Context, cardDTO);

                    Response.Redirect(Response.ApplyAppPathModifier("~/Pages/User/ManageCardsPage.aspx"));
                }
                catch (DuplicateInstanceException)
                {
                    lblNumberError.Visible = true;
                }

            }

        }

    }
}