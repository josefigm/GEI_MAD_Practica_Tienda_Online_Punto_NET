using Es.Udc.DotNet.Amazonia.Model.DAOs.CardDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.ClientDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.ProductDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.SaleDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.SaleLineDao;
using Es.Udc.DotNet.Amazonia.Model.ProductServiceImp.DTOs;
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

        [Inject]
        public ICardDao CardDao { private get; set; }

        [Inject]
        public IClientDao ClientDao { private get; set; }

        [Transactional]
        public ShoppingCart AddToShoppingCart(ShoppingCart shoppingCart, long productId, long units, bool gift)
        {
            Product product = ProductDao.Find(productId);
            if (!shoppingCart.items.Exists(x => x.product.id == productId))
            {
                if (product.stock >= units)
                {
                    ShoppingCartItem item = new ShoppingCartItem(units, gift, ProductMapper.ProductToProductDto(product));
                    item.price = (item.units * product.price);
                    shoppingCart.items.Add(item);

                    shoppingCart.totalPrice += item.price;
                    return shoppingCart;
                }
                else
                {
                    throw new InsufficientStockException(product.stock, units);
                }
                
            }
            else
            {
                ShoppingCartItem item = shoppingCart.items.Find(x => x.product.id == productId);

                return ModifyShoppingCartItem(shoppingCart, productId, (item.units + units), gift);
            }
        }

        public ShoppingCart DeleteFromShoppingCart(ShoppingCart shoppingCart, long productId)
        {
            ShoppingCartItem item = shoppingCart.items.Find(x => x.product.id == productId);
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
                if (item.product.id == productId)
                {
                    Product product = ProductDao.Find(productId);
                    if (product.stock >= (units + item.units))
                    {
                        item.units = units;
                        item.gift = gift;
                        double oldPrice = item.price;
                        item.price = (units * product.price);

                        shoppingCart.totalPrice = (shoppingCart.totalPrice - oldPrice + item.price);
                        break;
                    }
                    else
                    {
                        throw new InsufficientStockException(product.stock, units);
                    }
                    
                }
            }

            return shoppingCart;
        }

        public List<ShoppingCartItem> ShowShoppingCartItems(ShoppingCart shoppingCart)
        {
            return shoppingCart.items;
        }

        [Transactional]
        public long Buy(ShoppingCart shoppingCart, String descName, String address, long cardId, long clientId)
        {
            Client client;
            Card card;
            Sale sale;
            DateTime date = DateTime.Now;
            double totalPrice = 0;

            client = ClientDao.Find(clientId);
            card = CardDao.Find(cardId);

            if (card.clientId != client.id)
            {
                throw new Exception("Intento de compra con tarjeta que no le pertenece");
            }

            if (!(address != null))
            {
                address = client.address;
            }

            if (shoppingCart.items.Count == 0)
            {
                throw new EmptyShoppingCartException();
            }

            sale = new Sale();
            foreach (ShoppingCartItem line in shoppingCart.items)
            {
                Product lineProduct = ProductDao.Find(line.product.id);
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

            sale.Card = card;
            sale.Client = client;
            sale.totalPrice = totalPrice;
            sale.address = address;
            sale.descName = descName;
            sale.date = date;

            SaleDao.Create(sale);

            foreach (ShoppingCartItem line in shoppingCart.items)
            {
                SaleLine saleLine = new SaleLine
                {
                    units = line.units,
                    price = line.price,
                    gift = line.gift,
                    Product = ProductDao.Find(line.product.id),
                    Sale = sale,
                };
                SaleLineDao.Create(saleLine);
            }

            shoppingCart.items = new List<ShoppingCartItem>();

            return sale.id;
        }

        [Transactional]
        public SaleDTO ShowSaleDetails(long saleId)
        {
            Sale sale = SaleDao.Find(saleId);
            SaleDTO saleDetails = new SaleDTO
            {
                id = sale.id,
                date = sale.date,
                address = sale.address,
                totalPrice = sale.totalPrice,
                cardNumber = sale.Card.number,
                clientLogin = sale.Client.login,
            };

            Product product;
            List<SaleLineDTO> saleLines = new List<SaleLineDTO>();
            foreach (SaleLine line in sale.SaleLines)
            {
                product = ProductDao.Find(line.productId);
                saleLines.Add(
                    new SaleLineDTO(line.units, line.price, line.gift, product.id, product.name));
            }

            saleDetails.saleLines = saleLines;

            return saleDetails;
        }

        [Transactional]
        public List<SaleListItemDTO> ShowClientSaleList(long clientId, int startIndex, int count)
        {
            List<SaleListItemDTO> saleList = new List<SaleListItemDTO>();

            List<Sale> clientSalesFound = SaleDao.FindByClientId(clientId, startIndex, count);

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