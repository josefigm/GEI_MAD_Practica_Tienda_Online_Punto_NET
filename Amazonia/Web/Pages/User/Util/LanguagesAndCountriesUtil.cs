using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.Amazonia.Web.HTTP.View.ApplicationObjects;

namespace Es.Udc.DotNet.Amazonia.Web.Pages.User.Util
{
    public class LanguagesAndCountriesUtil
    {

        /// <summary>
        /// Loads the languages in the comboBox in the *selectedLanguage*. 
        /// Also, the selectedLanguage will appear selected in the 
        /// ComboBox
        /// </summary>
        public static void UpdateComboLanguage(String selectedLanguage, DropDownList comboLanguage)
        {
            comboLanguage.DataSource = Languages.GetLanguages(selectedLanguage);
            comboLanguage.DataTextField = "text";
            comboLanguage.DataValueField = "value";
            comboLanguage.DataBind();
            comboLanguage.SelectedValue = selectedLanguage;
        }

        /// <summary>
        /// Loads the countries in the comboBox in the *selectedLanguage*. 
        /// Also, the *selectedCountry* will appear selected in the 
        /// ComboBox
        /// </summary>
        public static void UpdateComboCountry(String selectedLanguage, String selectedCountry, DropDownList comboCountry)
        {
            comboCountry.DataSource = Countries.GetCountries(selectedLanguage);
            comboCountry.DataTextField = "text";
            comboCountry.DataValueField = "value";
            comboCountry.DataBind();
            comboCountry.SelectedValue = selectedCountry;
        }
    }
}