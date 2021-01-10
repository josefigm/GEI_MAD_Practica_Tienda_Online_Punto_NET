using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.Amazonia.Model.CardServiceImp;
using Es.Udc.DotNet.Amazonia.Web.HTTP.Session;

namespace Es.Udc.DotNet.Amazonia.Web.Pages.User
{
    public partial class UpdateCardDetailsPage : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                long idCard = Convert.ToInt64(Request.Params.Get("idCard"));
                CardDTO cardDTO = SessionManager.FindCardDetails(Context, idCard);

                Label1.Text = cardDTO.Number;
                tbCVV.Text = cardDTO.CVV;
                tbExpireDateMonth.Text = cardDTO.ExpireDate.Month.ToString();
                tbExpireDateYear.Text = cardDTO.ExpireDate.Year.ToString();
                ddlType.Text = cardDTO.Type;
            }



        }

        protected void BtnUpdateCard_Click(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {

                string monthString = tbExpireDateMonth.Text;
                int month = (int)Int64.Parse(monthString);

                string yearString = tbExpireDateYear.Text;
                int year = (int)Int64.Parse(yearString);

                DateTime expirationDate = new DateTime(year, month, 1);

                String cardTypeString = this.ddlType.SelectedValue;
                bool cardType = false;

                if (cardTypeString == "debitCard")
                {
                    cardType = true;
                }

                CardDTO cardDTO =
                    new CardDTO(Label1.Text, tbCVV.Text, expirationDate, cardType);

                SessionManager.UpdateCardDetails(cardDTO);

                Response.Redirect(
                    Response.ApplyAppPathModifier("~/Pages/User/ManageCardsPage.aspx"));


            }


        }
    }
}