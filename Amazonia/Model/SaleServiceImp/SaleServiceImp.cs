using Es.Udc.DotNet.Amazonia.Model.DAOs.ProductDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.SaleDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.SaleLineDao;
using Es.Udc.DotNet.Amazonia.Model.SaleServiceImp.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.Amazonia.Model.SaleServiceImp
{
    public class SaleServiceImp : ISaleService
    {
        public SaleServiceImp() { }

        [Inject]
        public IProductDao ProductDao { private get; set; }

        [Inject]
        public ISaleDao SaleDao { private get; set; }

        [Inject]
        public ISaleLineDao SaleLineDao { private get; set; }

        [Transactional]
        public long buy(List<SaleLine> saleLines, String creditCardNumber, String address, String clientLogin)
        {

            Sale sale = new Sale();
            DateTime date = DateTime.Now;
            double totalPrice = 0;

            foreach (SaleLine line in saleLines)
            {
                Product lineProduct = line.Product;
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

            sale.cardNumber= creditCardNumber;
            sale.clientLogin = clientLogin;
            sale.totalPrice = totalPrice;
            sale.address = address;
            sale.date = date;

            SaleDao.Create(sale);

            foreach (SaleLine line in saleLines)
            {
                line.Sale = sale;
                SaleLineDao.Create(line);
            }


            return sale.id;
        }
    }
}
