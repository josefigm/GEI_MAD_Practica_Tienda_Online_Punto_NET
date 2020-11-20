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

        /// <summary>Buy shopping cart products</summary>
        /// <param name="shoppingCart"></param>
        /// <param name="descName"></param>
        /// <param name="address"></param>
        /// <param name="cardNumber"></param>
        /// <param name="clientLogin"></param>
        /// /// <exception cref="Es.Udc.DotNet.Amazonia.Model.SaleServiceImp.Exceptions.EmptyShoppingCartException"></exception>
        /// <exception cref="Es.Udc.DotNet.Amazonia.Model.SaleServiceImp.Exceptions.InsufficientStockException">Stock de producto insuficiente</exception>
        [Transactional]
        long Buy(ShoppingCart shoppingCart, String descName, String address, String cardNumber, String clientLogin);

        /// <summary>Shows the sale details.</summary>
        /// <param name="saleId">The sale identifier.</param>
        /// <returns>A DTO with sale details</returns>
        [Transactional]
        SaleDTO ShowSaleDetails(long saleId);

        /// <summary>Shows the client sale list.</summary>
        /// <param name="clientLogin">The client login.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns>Client sale list</returns>
        [Transactional]
        List<SaleListItemDTO> ShowClientSaleList(String clientLogin, int startIndex, int count);
    }
}