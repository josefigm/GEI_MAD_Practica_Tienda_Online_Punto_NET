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
            ProductBlock productBlock = getData(0, 5);
            setRows(productBlock.products);
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

        private ProductBlock getData(int startIndex, int count)
        {
            ProductBlock productBlock;
            IIoCManager iiocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IProductService productService = iiocManager.Resolve<IProductService>();

            // Only one word is necesary, so the first one is retrieved.
            // Split by whitespaces
            String[] inputKeywords = tbSearchProduct.Text.Trim().Split();
            // The first one is retrieved.
            String keyword = inputKeywords[0];


            // We retrieve the category id
            String categoryId = comboCategory.SelectedValue;

            // If the user selected a category the value will be different of -1

            
            if (categoryId != "-1")
            {
                productBlock = productService.FindProductByWordAndCategory(keyword, Convert.ToInt64(categoryId), startIndex, count);
            }
            else
            {
                productBlock = productService.FindProductByWord(keyword, startIndex, count);
            }

            // Return the product block

            return productBlock;
        }

        private void setRows(List<ProductDTO> products)
        {
            DataTable dt = new DataTable();

            DataRow dr = dt.NewRow();
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Category", typeof(string)));
            dt.Columns.Add(new DataColumn("Entry date", typeof(string)));
            dt.Columns.Add(new DataColumn("Price", typeof(string)));
            dt.Columns.Add(new DataColumn("Link add to cart", typeof(string)));
            
            for (int i = 0; i < products.Count; i++)
            {
                dr = dt.NewRow();
                dr["Name"] = products[i].productTitle;
                dr["Category"] = products[i].category.id;
                dr["Entry date"] = products[i].entryDate.ToShortDateString();
                dr["Price"] = products[i].price.ToString();
                dr["Link add to cart"] = string.Empty;
                dt.Rows.Add(dr);
            }
            
            gvProducts.DataSource = dt;
            gvProducts.DataBind();
            
        }
    }
}