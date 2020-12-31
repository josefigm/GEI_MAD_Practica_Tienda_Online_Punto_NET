using Es.Udc.DotNet.Amazonia.Model;
using Es.Udc.DotNet.Amazonia.Model.ProductServiceImp;
using Es.Udc.DotNet.Amazonia.Model.ProductServiceImp.DTOs;
using Es.Udc.DotNet.ModelUtil.IoC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.Amazonia.Web.Pages.Product
{
    public partial class SearchPage : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UpdateComboCategory();
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            // Only one word is necesary, so the first one is retrieved.
            // Split by whitespaces
            String[] inputKeywords = tbSearchProduct.Text.Trim().Split();
            // The first one is retrieved.
            String keyword = inputKeywords[0];

            String url = String.Format("./ResultPage.aspx?keyword={0}&categoryId={1}&startIndex={2}&count={3}", keyword, comboCategory.SelectedValue, 0, 3);
            Response.Redirect(Response.ApplyAppPathModifier(url));
        }

        /// <summary>
        /// Loads the categories in the dropdown list.
        /// A default value will appear selected
        /// </summary>
        private void UpdateComboCategory()
        {
            
            IIoCManager iiocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IProductService productService = iiocManager.Resolve<IProductService>();

            List<Category> categories = productService.FindCategories();

            ArrayList categoriesDropDown = new ArrayList();

            // A default value is added because searching for a product doesn't require a category, it's optional
            categoriesDropDown.Add(new ListItem("-", "-1"));
            // Now, the categories are inserted.
            foreach (var category in categories)
            {
                categoriesDropDown.Add(new ListItem(category.name, category.id.ToString()));
            }

            comboCategory.DataSource = categoriesDropDown;
            comboCategory.DataTextField = "text";
            comboCategory.DataValueField = "value";
            comboCategory.DataBind();
            comboCategory.SelectedIndex = 0;            
        }
    }
}