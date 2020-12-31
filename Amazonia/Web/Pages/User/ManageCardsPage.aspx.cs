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
    public partial class ManageCardsPage : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                List<CardDTO> clientCards = SessionManager.GetClientCards(Context);

                GvListCards.DataSource = clientCards;
                GvListCards.DataBind();
                
            }

        }

        protected void GvListCards_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Update Card Details") return;
            long cardNumber = Convert.ToInt64(e.CommandArgument);

            // do something

            String url = String.Format("./UpdateCardDetailsPage.aspx?number={0}", cardNumber);
            Response.Redirect(Response.ApplyAppPathModifier(url));

        }

    }
}