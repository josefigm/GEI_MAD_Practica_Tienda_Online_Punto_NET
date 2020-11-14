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

        [Transactional]
        long buy(List<SaleLineDTO> saleLines, String creditCardNumber, String descName, String address, String clientLogin);

        [Transactional]
        SaleDTO showSaleDetails(long saleId);

        [Transactional]
        List<SaleListItemDTO> showClientSaleList(String clientLogin, int startIndex, int count);

    }
}
