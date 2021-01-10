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

                UpdateSessionDefaultCard(clientCards);

                GvListCards.DataSource = clientCards;
                GvListCards.DataBind();
                
            }

        }

        protected void GvListCards_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {


            if (e.CommandName == "Update Card Details")
            {
                // Conseguimos el id de la tarjeta
                long idCard = Convert.ToInt64(e.CommandArgument);
                String url = String.Format("./UpdateCardDetailsPage.aspx?idCard={0}", idCard);
                Response.Redirect(Response.ApplyAppPathModifier(url));
            }

            if (e.CommandName == "Set Default Card")
            {
                // Conseguimos el número de tarjeta
                long cardNumber = Convert.ToInt64(e.CommandArgument);
                SessionManager.SetDefaultCard(cardNumber);
                String url = String.Format("./ManageCardsPage.aspx");
                Response.Redirect(Response.ApplyAppPathModifier(url));
            }

        }

        private void UpdateSessionDefaultCard(List<CardDTO> clientCards)
        {
            foreach(CardDTO card in clientCards)
            {
                if (card.DefaultCard)
                {
                    UserSession session = SessionManager.GetUserSession(Context);
                    session.DefaultCard = card;
                    SessionManager.UpdateSessionForAuthenticatedUser(Context, session, SessionManager.GetLocale(Context));
                }
            }
        }
    }
}