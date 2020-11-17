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
        public long Buy(List<SaleLineDTO> saleLines, String creditCardNumber, String descName, String address, String clientLogin)
        {
            Sale sale = new Sale();
            DateTime date = DateTime.Now;
            double totalPrice = 0;

            foreach (SaleLineDTO line in saleLines)
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

            sale.cardNumber = creditCardNumber;
            sale.clientLogin = clientLogin;
            sale.totalPrice = totalPrice;
            sale.address = address;
            sale.descName = descName;
            sale.date = date;

            SaleDao.Create(sale);

            foreach (SaleLineDTO line in saleLines)
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