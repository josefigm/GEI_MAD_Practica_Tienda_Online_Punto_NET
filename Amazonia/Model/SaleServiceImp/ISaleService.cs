using Es.Udc.DotNet.Amazonia.Model.DAOs.ProductDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.SaleDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.SaleLineDao;
using Es.Udc.DotNet.Amazonia.Model.SaleServiceImp.DTOs;
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

        /// <summary>Adds to shopping cart.</summary>
        /// <param name="shoppingCart">The shopping cart.</param>
        /// <param name="productId">The product identifier.</param>
        /// <param name="units">The units.</param>
        /// <param name="gift">if set to <c>true</c> [gift].</param>
        /// <exception cref="InstanceNotFoundException"/>
        /// <returns>The sopping cart with new item added </returns>
        [Transactional]
        ShoppingCart AddToShoppingCart(ShoppingCart shoppingCart, long productId, long units, bool gift);

        /// <summary>Deletes from shopping cart.</summary>
        /// <param name="shoppingCart">The shopping cart.</param>
        /// <param name="productId">The product identifier.</param>
        /// <returns>The shopping cart without the item of product</returns>
        ShoppingCart DeleteFromShoppingCart(ShoppingCart shoppingCart, long productId);

        /// <summary>Modifies the shopping cart item.</summary>
        /// <param name="shoppingCart">The shopping cart.</param>
        /// <param name="productId">The product identifier.</param>
        /// <param name="units">The units.</param>
        /// <param name="gift">if set to <c>true</c> [gift].</param>
        /// <exception cref="InstanceNotFoundException"/>
        /// <returns>The shopping cart with the item of product modified</returns>
        [Transactional]
        ShoppingCart ModifyShoppingCartItem(ShoppingCart shoppingCart, long productId, long units, bool gift);

        /// <summary>Buy shopping cart products</summary>
        /// <param name="shoppingCart"></param>
        /// <param name="descName"></param>
        /// <param name="address"></param>
        /// <param name="cardNumber"></param>
        /// <param name="clientLogin"></param>
        /// <exception cref="Es.Udc.DotNet.Amazonia.Model.SaleServiceImp.Exceptions.EmptyShoppingCartException">Carrito sin items</exception>
        /// <exception cref="Es.Udc.DotNet.Amazonia.Model.SaleServiceImp.Exceptions.InsufficientStockException">Stock de producto insuficiente</exception>
        [Transactional]
        long Buy(ShoppingCart shoppingCart, String descName, String address, String cardNumber, String clientLogin);

        /// <summary>Shows the sale details.</summary>
        /// <param name="saleId">The sale identifier.</param>
        /// <exception cref="InstanceNotFoundException"/>
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