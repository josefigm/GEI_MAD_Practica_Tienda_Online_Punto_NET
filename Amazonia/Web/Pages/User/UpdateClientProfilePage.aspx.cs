using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.Amazonia.Model.ClientServiceImp;
using Es.Udc.DotNet.Amazonia.Web.HTTP.Session;
using Es.Udc.DotNet.Amazonia.Web.HTTP.View.ApplicationObjects;
using Es.Udc.DotNet.Amazonia.Web.Pages.User.Util;

namespace Es.Udc.DotNet.Amazonia.Web.Pages.User
{
    public partial class UpdateClientProfilePage : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ClientDTO clientDTO = SessionManager.FindClientProfileDetails(Context);

                tbFirstName.Text = clientDTO.FirstName;
                tbSurname.Text = clientDTO.LastName;
                tbAddress.Text = clientDTO.Address;
                tbEmail.Text = clientDTO.Email;

                /* Combo box initialization */
                LanguagesAndCountriesUtil.UpdateComboLanguage(clientDTO.Language, comboLanguage);

                LanguagesAndCountriesUtil.UpdateComboCountry(clientDTO.Language, clientDTO.Country, comboCountry);
            }   

        }

        protected void ComboLanguageSelectedIndexChanged(object sender, EventArgs e)
        {
            /* After a language change, the countries are printed in the
             * correct language.
             */
            LanguagesAndCountriesUtil.UpdateComboCountry(comboLanguage.SelectedValue, comboCountry.SelectedValue, comboCountry);
        }

        protected void BtnUpdateClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                ClientDTO clientDTO =
                    new ClientDTO(tbFirstName.Text, tbSurname.Text, tbAddress.Text,
                        tbEmail.Text, comboLanguage.SelectedValue,
                        comboCountry.SelectedValue);
     
                SessionManager.UpdateUserProfileDetails(Context,
                    clientDTO);
     
                Response.Redirect(
                    Response.ApplyAppPathModifier("~/Pages/MainPage.aspx"));
     
            }
        }




    }
}