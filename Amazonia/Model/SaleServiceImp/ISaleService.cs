using Es.Udc.DotNet.Amazonia.Model.DAOs.ProductDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.SaleDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.SaleLineDao;
using Es.Udc.DotNet.Amazonia.Model.SaleServiceImp.DTOs;
using Es.Udc.DotNet.Amazonia.Model.ShoppingCartServiceImp.DTOs;
using Es.Udc.DotNet.ModelUtil.Transactions;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.Amazonia.Model.SaleServiceImp
{
    public interface ISaleService
    {
        IProductDao ProductDao { set; }

        ISaleDao SaleDao { set; }

        ISaleLineDao SaleLineDao { set; }

        /// <exception cref="Es.Udc.DotNet.Amazonia.Model.SaleServiceImp.Exceptions.InsufficientStockException"></exception>
        [Transactional]
        long Buy(ShoppingCart shoppingCart, String descName, String address, String cardNumber, String clientLogin);

        [Transactional]
        SaleDTO ShowSaleDetails(long saleId);

        [Transactional]
        List<SaleListItemDTO> ShowClientSaleList(String clientLogin, int startIndex, int count);

    }
}
