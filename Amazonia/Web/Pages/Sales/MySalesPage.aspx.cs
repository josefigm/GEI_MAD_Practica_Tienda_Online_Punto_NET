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

            if (!IsPostBack)
            {

                long userId = SessionManager.GetUserSession(Context).UserProfileId;
                List<SaleListItemDTO> clientSales = GetSales(Context, userId);

                GvListSales.DataSource = clientSales;
                GvListSales.DataBind();

            }

        }

        private static ISaleService saleService;
        

        public ISaleService SaleService
        {
            set { saleService = value; }
        }

        /// <summary>
        /// Finds the client sales with the id stored in the session.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static List<SaleListItemDTO> GetSales(HttpContext context, long userId)
        {
            IIoCManager iiocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ISaleService saleService = iiocManager.Resolve<ISaleService>();

            // Revisar esto para paginación
            List<SaleListItemDTO> saleList = saleService.ShowClientSaleList(userId, 0, 10);

            return saleList;

        }
    }
}