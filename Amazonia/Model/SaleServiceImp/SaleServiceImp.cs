using Es.Udc.DotNet.Amazonia.Model.DAOs.ProductDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.SaleDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.SaleLineDao;
using Es.Udc.DotNet.Amazonia.Model.SaleServiceImp.DTOs;
using Es.Udc.DotNet.Amazonia.Model.SaleServiceImp.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.Amazonia.Model.SaleServiceImp
{
    public class SaleServiceImp : ISaleService
    {
        public SaleServiceImp()
        {
        }

        [Inject]
        public IProductDao ProductDao { private get; set; }

        [Inject]
        public ISaleDao SaleDao { private get; set; }

        [Inject]
        public ISaleLineDao SaleLineDao { private get; set; }

        [Transactional]
        public ShoppingCart AddToShoppingCart(ShoppingCart shoppingCart, long productId, long units, bool gift)
        {
            Product product = ProductDao.Find(productId);
            if (!shoppingCart.items.Exists(x => x.productId == productId))
            {
                if (ProductDao.Exists(productId))
                {
                    ShoppingCartItem item = new ShoppingCartItem(units, gift, productId, product.name);
                    item.price = (item.units * product.price);
                    shoppingCart.items.Add(item);

                    shoppingCart.totalPrice += item.price;
                }
                return shoppingCart;
            }
            else
            {
                ShoppingCartItem item = shoppingCart.items.Find(x => x.productId == productId);

                return ModifyShoppingCartItem(shoppingCart, productId, (item.units + units), gift);
            }
        }

        public ShoppingCart DeleteFromShoppingCart(ShoppingCart shoppingCart, long productId)
        {
            ShoppingCartItem item = shoppingCart.items.Find(x => x.productId == productId);
            if (shoppingCart.items.Remove(item))
            {
                shoppingCart.totalPrice -= item.price;
                return shoppingCart;
            }
            else
            {
                return shoppingCart;
            }
        }

        [Transactional]
        public ShoppingCart ModifyShoppingCartItem(ShoppingCart shoppingCart, long productId, long units, bool gift)
        {
            foreach (ShoppingCartItem item in shoppingCart.items)
            {
                if (item.productId == productId)
                {
                    Product product = ProductDao.Find(productId);
                    item.units = units;
                    item.gift = gift;
                    double oldPrice = item.price;
                    item.price = (units * product.price);

                    shoppingCart.totalPrice = (shoppingCart.totalPrice - oldPrice + item.price);
                    break;
                }
            }

            return shoppingCart;
        }

        [Transactional]
        public long Buy(ShoppingCart shoppingCart, String descName, String address, String cardNumber, String clientLogin)
        {
            Sale sale = new Sale();
            DateTime date = DateTime.Now;
            double totalPrice = 0;

            if (shoppingCart.items.Count == 0)
            {
                throw new EmptyShoppingCartException();
            }

            foreach (ShoppingCartItem line in shoppingCart.items)
            {
                Product lineProduct = ProductDao.Find(line.productId);
                if (lineProduct.stock >= line.units)
                {
                    lineProduct.stock -= line.units;
                    ProductDao.Update(lineProduct);

                    totalPrice += (line.price * line.units);
                }
                else
                {
                    throw new InsufficientStockException(lineProduct.stock, line.units);
                }
            }

            sale.cardNumber = cardNumber;
            sale.clientLogin = clientLogin;
            sale.totalPrice = totalPrice;
            sale.address = address;
            sale.descName = descName;
            sale.date = date;

            SaleDao.Create(sale);

            foreach (ShoppingCartItem line in shoppingCart.items)
            {
                SaleLine saleLine = new SaleLine();
                saleLine.units = line.units;
                saleLine.price = line.price;
                saleLine.gift = line.gift;
                saleLine.Product = ProductDao.Find(line.productId);
                saleLine.Sale = sale;
                SaleLineDao.Create(saleLine);
            }

            return sale.id;
        }

        [Transactional]
        public SaleDTO ShowSaleDetails(long saleId)
        {
            Sale sale = SaleDao.Find(saleId);
            SaleDTO saleDetails = new SaleDTO();

            saleDetails.id = sale.id;
            saleDetails.date = sale.date;
            saleDetails.address = sale.address;
            saleDetails.totalPrice = sale.totalPrice;
            saleDetails.cardNumber = sale.cardNumber;
            saleDetails.clientLogin = sale.clientLogin;

            List<SaleLineDTO> saleLines = new List<SaleLineDTO>();
            foreach (SaleLine line in sale.SaleLines)
            {
                saleLines.Add(
                    new SaleLineDTO(line.units, line.price, line.gift, line.productId));
            }

            saleDetails.saleLines = saleLines;

            return saleDetails;
        }

        [Transactional]
        public List<SaleListItemDTO> ShowClientSaleList(String clientLogin, int startIndex, int count)
        {
            List<SaleListItemDTO> saleList = new List<SaleListItemDTO>();

            List<Sale> clientSalesFound = SaleDao.FindByClientLogin(clientLogin, startIndex, count);

            foreach (Sale sale in clientSalesFound)
            {
                saleList.Add(
                    new SaleListItemDTO(sale.id, sale.date, sale.descName, sale.totalPrice)
                    );
            }

            return saleList;
        }
    }
}