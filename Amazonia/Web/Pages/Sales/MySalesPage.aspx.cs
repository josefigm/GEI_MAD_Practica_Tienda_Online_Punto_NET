using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.Amazonia.Model.SaleServiceImp;
using Es.Udc.DotNet.Amazonia.Model.SaleServiceImp.DTOs;
using Es.Udc.DotNet.Amazonia.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.IoC;

namespace Es.Udc.DotNet.Amazonia.Web.Pages.Sales
{
    public partial class MySalesPage : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            int startIndex, count;

            lnkPrevious.Visible = false;
            lnkNext.Visible = false;

            /* Get Start Index */
            try
            {
                startIndex = Int32.Parse(Request.Params.Get("startIndex"));
            }
            catch (ArgumentNullException)
            {
                startIndex = 0;
            }

            /* Get Count */
            try
            {
                count = Int32.Parse(Request.Params.Get("count"));
            }
            catch (ArgumentNullException)
            {
                count = 3;
            }

            long userId = SessionManager.GetUserSession(Context).UserProfileId;

            SaleBlock saleBlock = GetSales(userId, startIndex, count);

            if (saleBlock.Sales.Count == 0)
            {
                lblNoResults.Visible = true;
            }

            if (!IsPostBack)
            {
                GvListSales.DataSource = saleBlock.Sales;
                GvListSales.DataBind();
            }

            /* "Previous" link */
            if ((startIndex - count) >= 0)
            {
                String url = String.Format("./MySalesPage.aspx?startIndex={0}&count={1}", (startIndex - count), count);

                this.lnkPrevious.NavigateUrl = Response.ApplyAppPathModifier(url);
                this.lnkPrevious.Visible = true;
            }

            /* "Next" link */
            if (saleBlock.ExistMoreSales)
            {
                String url = String.Format("./MySalesPage.aspx?startIndex={0}&count={1}", (startIndex + count), count);

                this.lnkNext.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkNext.Visible = true;
            }

        }


        /// <summary>
        /// Finds the client sales with the id stored in the session.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        private SaleBlock GetSales(long userId, int startIndex, int count)
        {

            IIoCManager iiocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ISaleService saleService = iiocManager.Resolve<ISaleService>();

            // Revisar esto para paginación
            SaleBlock saleBlock = saleService.ShowClientSaleList(userId, startIndex, count);

            return saleBlock;

        }


    }
}