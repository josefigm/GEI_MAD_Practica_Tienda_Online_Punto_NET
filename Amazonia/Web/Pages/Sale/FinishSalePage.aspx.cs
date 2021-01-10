using Es.Udc.DotNet.Amazonia.Model.CardServiceImp;
using Es.Udc.DotNet.Amazonia.Model.ClientServiceImp;
using Es.Udc.DotNet.Amazonia.Model.SaleServiceImp;
using Es.Udc.DotNet.Amazonia.Model.SaleServiceImp.DTOs;
using Es.Udc.DotNet.Amazonia.Model.SaleServiceImp.Exceptions;
using Es.Udc.DotNet.Amazonia.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.Amazonia.Web.Pages.Sale
{
    public partial class FinishSalePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            IIoCManager iiocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IClientService clientService = iiocManager.Resolve<IClientService>();

            defaultAddress.Text = SessionManager.GetUserSession(Context).Address;

            CardDTO defaultCard = SessionManager.GetUserSession(Context).DefaultCard;


            if (defaultCard != null)
            {
                lclDefaultCartNumber.Text = defaultCard.Number;
            }
            else
            {
                lclChooseCard.Visible = false;
                comboCards.Visible = false;
                lclPayWithDefault.Visible = false;
                lclNoCards.Visible = true;
                lnkCreateCard.Visible = true;
                btnFinishSale.Enabled = false;
            }

            List<CardDTO> clientCards = clientService.ListCardsByClientId(SessionManager.GetUserSession(Context).UserProfileId);
            UpdateComboCards(clientCards);
        }

        private void UpdateComboCards(List<CardDTO> clientCards)
        {
            comboCards.DataSource = clientCards;
            comboCards.DataTextField = "Number";
            comboCards.DataValueField = "CardId";
            comboCards.DataBind();
        }

        private List<String> GetCards(List<CardDTO> clientCards)
        {
            
            List<String> cards = new List<string>();

            foreach (CardDTO card in clientCards)
            {
                cards.Add(card.Number);
            }

            return cards;
        }

        protected void btnFinishSale_Click(object sender, EventArgs e)
        {
            IIoCManager iiocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ISaleService saleService = iiocManager.Resolve<ISaleService>();

            long clientId = SessionManager.GetUserSession(Context).UserProfileId;
            ShoppingCart shoppingCart = (ShoppingCart)Session["shoppingCart"];
            String address;

            if (tbNewAddress.Text == "")
            {
                address = defaultAddress.Text;
            }
            else
            {
                address = tbNewAddress.Text;
            }

            long saleId;
            String url;

            try
            {
                saleId = saleService.Buy(shoppingCart, tbDescName.Text, address, Convert.ToInt64(comboCards.SelectedValue), clientId);

                url = String.Format("./FinishedSalePage.aspx?saleId={0}", saleId);
                Response.Redirect(Response.ApplyAppPathModifier(url));
            }
            catch (EmptyShoppingCartException)
            {
                url = String.Format("~/Pages/MainPage.aspx");
                Response.Redirect(Response.ApplyAppPathModifier(url));
            }
            catch (InsufficientStockException ex)
            {
                url = String.Format("./StockErrorPage.aspx?productName=" + ex.Name + "&stock=" + ex.Stock);
                Response.Redirect(Response.ApplyAppPathModifier(url));
            }
            
        }
    }
}