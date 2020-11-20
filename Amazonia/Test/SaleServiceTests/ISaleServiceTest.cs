using Es.Udc.DotNet.Amazonia.Model;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CardDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CategoryDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.ClientDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.ProductDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.SaleDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.SaleLineDao;
using Es.Udc.DotNet.Amazonia.Model.SaleServiceImp;
using Es.Udc.DotNet.Amazonia.Model.SaleServiceImp.DTOs;
using Es.Udc.DotNet.Amazonia.Model.SaleServiceImp.Exceptions;
using Es.Udc.DotNet.Amazonia.Model.ShoppingCartServiceImp.DTOs;
using Es.Udc.DotNet.Amazonia.Model.ShoppingCartServiceImp.Exceptions;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace Test.SaleServiceTests
{
    /// <summary>
    /// Descripción resumida de ISaleServiceTest
    /// </summary>
    [TestClass]
    public class ISaleServiceTest
    {
        private static IKernel kernel;
        private static ISaleService saleService;
        private static ISaleDao saleDao;
        private static IClientDao clientDao;
        private static ICardDao cardDao;
        private static ICategoryDao categoryDao;
        private static ISaleLineDao saleLineDao;
        private static IProductDao productDao;

        private String address = "Direccion de entrega";
        private String descName = "Test sale";
        private const long NON_EXISTENT_SALE_ID = -1;

        private TransactionScope transactionScope;

        public TestContext TestContext { get; set; }

        public ISaleServiceTest()
        {
        }

        [TestMethod]
        public void TestBuy()
        {
            #region Declaracion de variables

            Client client = new Client();
            client.login = "client";
            client.password = "password";
            client.firstName = "firstName";
            client.lastName = "lastName";
            client.address = "adress";
            client.email = "email";
            client.role = 1;
            client.language = 1;
            clientDao.Create(client);

            Card card = new Card();
            card.number = "1111222233334444";
            card.cvv = "123";
            card.expireDate = new DateTime(2025, 1, 1);
            card.name = "Client Name";
            card.type = true;
            cardDao.Create(card);

            client.Cards.Add(card);
            clientDao.Update(client);

            Category category = new Category();
            category.name = "category";
            categoryDao.Create(category);

            Product product = new Product();
            product.name = "TestProduct";
            product.price = 24;
            product.entryDate = new DateTime(2020, 1, 1);
            product.stock = 200;
            product.Category = category;
            productDao.Create(product);

            List<ShoppingCartItem> lines = new List<ShoppingCartItem>();

            ShoppingCartItem line1 = new ShoppingCartItem(3, false, product.id, product.name);

            lines.Add(line1);

            ShoppingCart shoppingCart = new ShoppingCart(72, lines);

            #endregion Declaracion de variables

            long saleId = saleService.Buy(shoppingCart, descName, address, card.number, client.login);

            Sale sale = saleDao.Find(saleId);

            Assert.AreEqual(1, sale.SaleLines.Count);
            Assert.AreEqual(card.number, sale.cardNumber);
            Assert.AreEqual(client.login, sale.clientLogin);
            Assert.AreEqual((line1.price * line1.units), sale.totalPrice);
        }

        [TestMethod]
        [ExpectedException(typeof(InsufficientStockException))]
        public void TestBuyMoreThanStock()
        {
            #region Declaracion de variables

            Client client = new Client();
            client.login = "client1";
            client.password = "password";
            client.firstName = "firstName";
            client.lastName = "lastName";
            client.address = "adress";
            client.email = "email";
            client.role = 1;
            client.language = 1;
            clientDao.Create(client);

            Card card = new Card();
            card.number = "111122223333555";
            card.cvv = "123";
            card.expireDate = new DateTime(2025, 1, 1);
            card.name = "Client Name";
            card.type = true;
            cardDao.Create(card);

            client.Cards.Add(card);
            clientDao.Update(client);

            Category category = new Category();
            category.name = "category";
            categoryDao.Create(category);

            Product product = new Product();
            product.name = "TestProduct";
            product.price = 24;
            product.entryDate = new DateTime(2020, 1, 1);
            product.stock = 2;
            product.Category = category;
            productDao.Create(product);

            List<ShoppingCartItem> lines = new List<ShoppingCartItem>();

            ShoppingCartItem line1 = new ShoppingCartItem(3, false, product.id, product.name);

            lines.Add(line1);

            ShoppingCart shoppingCart = new ShoppingCart(72, lines);

            #endregion Declaracion de variables

            long saleId = saleService.Buy(shoppingCart, descName, address, card.number, client.login);
        }

        [TestMethod]
        public void TestShowSaleDetails()
        {
            #region Declaracion de variables

            Client client = new Client();
            client.login = "client2";
            client.password = "password";
            client.firstName = "firstName";
            client.lastName = "lastName";
            client.address = "adress";
            client.email = "email";
            client.role = 1;
            client.language = 1;
            clientDao.Create(client);

            Card card = new Card();
            card.number = "4444333322221111";
            card.cvv = "123";
            card.expireDate = new DateTime(2025, 1, 1);
            card.name = "Client Name";
            card.type = true;
            cardDao.Create(card);

            client.Cards.Add(card);
            clientDao.Update(client);

            Category category = new Category();
            category.name = "category";
            categoryDao.Create(category);

            Product product = new Product();
            product.name = "TestProduct";
            product.price = 24;
            product.entryDate = new DateTime(2020, 1, 1);
            product.stock = 200;
            product.Category = category;
            productDao.Create(product);

            List<ShoppingCartItem> lines = new List<ShoppingCartItem>();

            ShoppingCartItem line1 = new ShoppingCartItem(3, false, product.id, product.name);

            lines.Add(line1);

            ShoppingCart shoppingCart = new ShoppingCart(72, lines);

            #endregion Declaracion de variables

            long saleId = saleService.Buy(shoppingCart, descName, address, card.number, client.login);

            SaleDTO sale = saleService.ShowSaleDetails(saleId);

            Assert.AreEqual(1, sale.saleLines.Count);
            Assert.AreEqual(card.number, sale.cardNumber);
            Assert.AreEqual(client.login, sale.clientLogin);
            Assert.AreEqual((line1.price * line1.units), sale.totalPrice);
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void TestShowSaleDetailsNonExistentSale()
        {
            SaleDTO sale = saleService.ShowSaleDetails(NON_EXISTENT_SALE_ID);
        }

        [TestMethod]
        public void TestShowClientSaleList()
        {
            #region Declaracion de variables

            Client client = new Client();
            client.login = "client3";
            client.password = "password";
            client.firstName = "firstName";
            client.lastName = "lastName";
            client.address = "adress";
            client.email = "email";
            client.role = 1;
            client.language = 1;
            clientDao.Create(client);

            Client client2 = new Client();
            client2.login = "client3bis";
            client2.password = "password";
            client2.firstName = "firstName";
            client2.lastName = "lastName";
            client2.address = "adress";
            client2.email = "email";
            client2.role = 1;
            client2.language = 1;
            clientDao.Create(client2);

            Card card = new Card();
            card.number = "5555333322221111";
            card.cvv = "123";
            card.expireDate = new DateTime(2025, 1, 1);
            card.name = "Client Name";
            card.type = true;
            cardDao.Create(card);

            client.Cards.Add(card);
            client2.Cards.Add(card);
            clientDao.Update(client);
            clientDao.Update(client2);

            Category category = new Category();
            category.name = "category";
            categoryDao.Create(category);

            Product product = new Product();
            product.name = "TestProduct";
            product.price = 24;
            product.entryDate = new DateTime(2020, 1, 1);
            product.stock = 200;
            product.Category = category;
            productDao.Create(product);

            List<ShoppingCartItem> lines = new List<ShoppingCartItem>();

            ShoppingCartItem line1 = new ShoppingCartItem(3, false, product.id, product.name);

            lines.Add(line1);

            ShoppingCart shoppingCart = new ShoppingCart(72, lines);

            #endregion Declaracion de variables

            long saleId = saleService.Buy(shoppingCart, descName, address, card.number, client.login);

            saleService.Buy(shoppingCart, descName, address, card.number, client2.login);

            List<SaleListItemDTO> saleList = saleService.ShowClientSaleList(client.login, 0, 1);

            Assert.AreEqual(1, saleList.Count);

            List<SaleListItemDTO>.Enumerator saleEnum = saleList.GetEnumerator();
            saleEnum.MoveNext();
            SaleListItemDTO sale = saleEnum.Current;

            Assert.AreEqual(saleId, sale.id);
            Assert.AreEqual(descName, sale.descName);
        }

        [TestMethod]
        public void TestShowClientEmptySaleList()
        {
            #region Declaracion de variables

            Client client = new Client();
            client.login = "client4";
            client.password = "password";
            client.firstName = "firstName";
            client.lastName = "lastName";
            client.address = "adress";
            client.email = "email";
            client.role = 1;
            client.language = 1;
            clientDao.Create(client);

            #endregion Declaracion de variables

            List<SaleListItemDTO> saleList = saleService.ShowClientSaleList(client.login, 0, 1);

            Assert.AreEqual(0, saleList.Count);
        }

        #region Additional test attributes

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();

            saleDao = kernel.Get<ISaleDao>();
            saleLineDao = kernel.Get<ISaleLineDao>();
            saleService = kernel.Get<ISaleService>();
            clientDao = kernel.Get<IClientDao>();
            cardDao = kernel.Get<ICardDao>();
            categoryDao = kernel.Get<ICategoryDao>();
            productDao = kernel.Get<IProductDao>();
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup]
        public static void MyClassCleanup()
        {
            TestManager.ClearNInjectKernel(kernel);
        }

        //Use TestInitialize to run code before running each test
        [TestInitialize]
        public void MyTestInitialize()
        {
            transactionScope = new TransactionScope();
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup]
        public void MyTestCleanup()
        {
            transactionScope.Dispose();
        }

        #endregion Additional test attributes
    }
}