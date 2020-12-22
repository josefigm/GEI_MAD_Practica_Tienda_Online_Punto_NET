using Es.Udc.DotNet.Amazonia.Model;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CardDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CategoryDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.ClientDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.ProductDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.SaleDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.SaleLineDao;
using Es.Udc.DotNet.Amazonia.Model.ProductServiceImp.DTOs;
using Es.Udc.DotNet.Amazonia.Model.SaleServiceImp;
using Es.Udc.DotNet.Amazonia.Model.SaleServiceImp.DTOs;
using Es.Udc.DotNet.Amazonia.Model.SaleServiceImp.Exceptions;
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

        private readonly String address = "Direccion de entrega";
        private readonly String descName = "Test sale";
        private const long NON_EXISTENT_SALE_ID = -1;

        private TransactionScope transactionScope;

        public TestContext TestContext { get; set; }

        public ISaleServiceTest()
        {
        }

        [TestMethod]
        public void TestAddToShoppingCart()
        {
            #region Declaracion de variables

            Category category = new Category
            {
                name = "category"
            };
            categoryDao.Create(category);

            Product product = new Product
            {
                name = "TestProduct",
                price = 24,
                entryDate = new DateTime(2020, 1, 1),
                stock = 200,
                Category = category
            };
            productDao.Create(product);

            Product product2 = new Product
            {
                name = "TestProduct2",
                price = 10,
                entryDate = new DateTime(2020, 1, 1),
                stock = 200,
                Category = category
            };
            productDao.Create(product2);

            ShoppingCart shoppingCart = new ShoppingCart();

            #endregion Declaracion de variables

            ShoppingCart returnedShoppingCart = saleService.AddToShoppingCart(shoppingCart, product.id, 3, false);
            returnedShoppingCart = saleService.AddToShoppingCart(returnedShoppingCart, product2.id, 1, false);

            Assert.AreEqual(2, returnedShoppingCart.items.Count);
            Assert.AreEqual(82, returnedShoppingCart.totalPrice);
        }

        [TestMethod]
        public void TestAddToShoppingCartExistentProduct()
        {
            #region Declaracion de variables

            Category category = new Category
            {
                name = "category"
            };
            categoryDao.Create(category);

            Product product = new Product
            {
                name = "TestProduct",
                price = 24,
                entryDate = new DateTime(2020, 1, 1),
                stock = 200,
                Category = category
            };
            productDao.Create(product);

            ShoppingCart shoppingCart = new ShoppingCart();

            #endregion Declaracion de variables

            ShoppingCart returnedShoppingCart = saleService.AddToShoppingCart(shoppingCart, product.id, 3, false);
            returnedShoppingCart = saleService.AddToShoppingCart(returnedShoppingCart, product.id, 1, false);

            Assert.AreEqual(1, returnedShoppingCart.items.Count);
            Assert.AreEqual(96, returnedShoppingCart.totalPrice);
        }

        [TestMethod]
        [ExpectedException(typeof(InsufficientStockException))]
        public void TestAddToShoppingCartMoreThanStock()
        {
            #region Declaracion de variables

            Category category = new Category
            {
                name = "category"
            };
            categoryDao.Create(category);

            Product product = new Product
            {
                name = "TestProduct",
                price = 24,
                entryDate = new DateTime(2020, 1, 1),
                stock = 2,
                Category = category
            };
            productDao.Create(product);

            ShoppingCart shoppingCart = new ShoppingCart();

            #endregion Declaracion de variables

            saleService.AddToShoppingCart(shoppingCart, product.id, 3, false);
        }

        [TestMethod]
        public void TestDeleteFromShoppingCart()
        {
            #region Declaracion de variables

            Category category = new Category
            {
                name = "category"
            };
            categoryDao.Create(category);

            Product product = new Product
            {
                name = "TestProduct",
                price = 24,
                entryDate = new DateTime(2020, 1, 1),
                stock = 200,
                Category = category
            };
            productDao.Create(product);

            Product product2 = new Product
            {
                name = "TestProduct2",
                price = 10,
                entryDate = new DateTime(2020, 1, 1),
                stock = 200,
                Category = category
            };
            productDao.Create(product2);

            ShoppingCart shoppingCart = new ShoppingCart();

            #endregion Declaracion de variables

            ShoppingCart returnedShoppingCart = saleService.AddToShoppingCart(shoppingCart, product.id, 3, false);
            returnedShoppingCart = saleService.AddToShoppingCart(returnedShoppingCart, product2.id, 1, false);

            Assert.AreEqual(2, returnedShoppingCart.items.Count);
            Assert.AreEqual(82, returnedShoppingCart.totalPrice);

            returnedShoppingCart = saleService.DeleteFromShoppingCart(returnedShoppingCart, product2.id);

            Assert.AreEqual(1, returnedShoppingCart.items.Count);
            Assert.AreEqual(72, returnedShoppingCart.totalPrice);
        }

        [TestMethod]
        public void TestModifyShoppingCartItem()
        {
            #region Declaracion de variables

            Category category = new Category
            {
                name = "category"
            };
            categoryDao.Create(category);

            Product product = new Product
            {
                name = "TestProduct",
                price = 24,
                entryDate = new DateTime(2020, 1, 1),
                stock = 200,
                Category = category
            };
            productDao.Create(product);

            Product product2 = new Product
            {
                name = "TestProduct2",
                price = 10,
                entryDate = new DateTime(2020, 1, 1),
                stock = 200,
                Category = category
            };
            productDao.Create(product2);

            ShoppingCart shoppingCart = new ShoppingCart();

            #endregion Declaracion de variables

            ShoppingCart returnedShoppingCart = saleService.AddToShoppingCart(shoppingCart, product.id, 3, false);
            returnedShoppingCart = saleService.AddToShoppingCart(returnedShoppingCart, product2.id, 1, false);

            Assert.AreEqual(2, returnedShoppingCart.items.Count);
            Assert.AreEqual(82, returnedShoppingCart.totalPrice);

            returnedShoppingCart = saleService.ModifyShoppingCartItem(returnedShoppingCart, product2.id, 10, true);

            Assert.AreEqual(2, returnedShoppingCart.items.Count);
            Assert.AreEqual(172, returnedShoppingCart.totalPrice);

            ShoppingCartItem modifiedItem = returnedShoppingCart.items.Find(x => x.product.id == product2.id);

            Assert.AreEqual(10, modifiedItem.units);
            Assert.IsTrue(modifiedItem.gift);
        }

        [TestMethod]
        [ExpectedException(typeof(InsufficientStockException))]
        public void TestModifyShoppingCartItemMoreThanStock()
        {
            #region Declaracion de variables

            Category category = new Category
            {
                name = "category"
            };
            categoryDao.Create(category);

            Product product = new Product
            {
                name = "TestProduct",
                price = 24,
                entryDate = new DateTime(2020, 1, 1),
                stock = 5,
                Category = category
            };
            productDao.Create(product);

            ShoppingCart shoppingCart = new ShoppingCart();

            #endregion Declaracion de variables

            ShoppingCart returnedShoppingCart = saleService.AddToShoppingCart(shoppingCart, product.id, 3, false);

            Assert.AreEqual(1, returnedShoppingCart.items.Count);
            Assert.AreEqual(72, returnedShoppingCart.totalPrice);

            saleService.ModifyShoppingCartItem(returnedShoppingCart, product.id, 10, true);
        }

        [TestMethod]
        public void TestShowShoppingCartItems()
        {
            #region Declaracion de variables

            Category category = new Category
            {
                name = "category"
            };
            categoryDao.Create(category);

            Product product = new Product
            {
                name = "TestProduct",
                price = 24,
                entryDate = new DateTime(2020, 1, 1),
                stock = 200,
                Category = category
            };
            productDao.Create(product);

            Product product2 = new Product
            {
                name = "TestProduct2",
                price = 10,
                entryDate = new DateTime(2020, 1, 1),
                stock = 200,
                Category = category
            };
            productDao.Create(product2);

            ShoppingCart shoppingCart = new ShoppingCart();

            #endregion Declaracion de variables

            ShoppingCart returnedShoppingCart = saleService.AddToShoppingCart(shoppingCart, product.id, 3, false);
            returnedShoppingCart = saleService.AddToShoppingCart(returnedShoppingCart, product2.id, 1, false);

            List<ShoppingCartItem> items = saleService.ShowShoppingCartItems(returnedShoppingCart);
            CollectionAssert.AreEqual(returnedShoppingCart.items, items);
        }

        [TestMethod]
        public void TestBuy()
        {
            #region Declaracion de variables

            Client client = new Client
            {
                login = "client",
                password = "password",
                firstName = "firstName",
                lastName = "lastName",
                address = "adress",
                email = "email",
                role = 1,
                language = 1
            };
            clientDao.Create(client);

            Card card = new Card
            {
                number = "1111222233334444",
                cvv = "123",
                expireDate = new DateTime(2025, 1, 1),
                type = true,
                defaultCard = true,
                Client = client
            };
            cardDao.Create(card);

            Category category = new Category
            {
                name = "category"
            };
            categoryDao.Create(category);

            Product product = new Product
            {
                name = "TestProduct",
                price = 24,
                entryDate = new DateTime(2020, 1, 1),
                stock = 200,
                Category = category
            };
            productDao.Create(product);

            List<ShoppingCartItem> lines = new List<ShoppingCartItem>();

            ShoppingCartItem line1 = new ShoppingCartItem(3, false, ProductMapper.ProductToProductDto(product));

            lines.Add(line1);

            ShoppingCart shoppingCart = new ShoppingCart(72, lines);

            #endregion Declaracion de variables

            long saleId = saleService.Buy(shoppingCart, descName, address, card.id, client.id);

            Sale sale = saleDao.Find(saleId);

            Assert.AreEqual(1, sale.SaleLines.Count);
            Assert.AreEqual(card.id, sale.cardId);
            Assert.AreEqual(client.id, sale.clientId);
            Assert.AreEqual((line1.price * line1.units), sale.totalPrice);
            Assert.AreEqual(0, shoppingCart.items.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InsufficientStockException))]
        public void TestBuyMoreThanStock()
        {
            #region Declaracion de variables

            Client client = new Client
            {
                login = "client",
                password = "password",
                firstName = "firstName",
                lastName = "lastName",
                address = "adress",
                email = "email",
                role = 1,
                language = 1
            };
            clientDao.Create(client);

            Card card = new Card
            {
                number = "1111222233334444",
                cvv = "123",
                expireDate = new DateTime(2025, 1, 1),
                type = true,
                defaultCard = true,
                Client = client
            };
            cardDao.Create(card);

            Category category = new Category
            {
                name = "category"
            };
            categoryDao.Create(category);

            Product product = new Product
            {
                name = "TestProduct",
                price = 24,
                entryDate = new DateTime(2020, 1, 1),
                stock = 20,
                Category = category
            };
            productDao.Create(product);

            List<ShoppingCartItem> lines = new List<ShoppingCartItem>();

            ShoppingCartItem line1 = new ShoppingCartItem(30, false, ProductMapper.ProductToProductDto(product));

            lines.Add(line1);

            ShoppingCart shoppingCart = new ShoppingCart(72, lines);

            #endregion Declaracion de variables

            long saleId = saleService.Buy(shoppingCart, descName, address, card.id, client.id);
        }

        [TestMethod]
        [ExpectedException(typeof(EmptyShoppingCartException))]
        public void TestBuyEmptyShoppingCart()
        {
            #region Declaracion de variables

            Client client = new Client
            {
                login = "client",
                password = "password",
                firstName = "firstName",
                lastName = "lastName",
                address = "adress",
                email = "email",
                role = 1,
                language = 1
            };
            clientDao.Create(client);

            Card card = new Card
            {
                number = "1111222233334444",
                cvv = "123",
                expireDate = new DateTime(2025, 1, 1),
                type = true,
                defaultCard = true,
                Client = client
            };
            cardDao.Create(card);

            ShoppingCart shoppingCart = new ShoppingCart();

            #endregion Declaracion de variables

            long saleId = saleService.Buy(shoppingCart, descName, address, card.id, client.id);
        }

        [TestMethod]
        public void TestShowSaleDetails()
        {
            #region Declaracion de variables

            Client client = new Client
            {
                login = "client",
                password = "password",
                firstName = "firstName",
                lastName = "lastName",
                address = "adress",
                email = "email",
                role = 1,
                language = 1
            };
            clientDao.Create(client);

            Card card = new Card
            {
                number = "1111222233334444",
                cvv = "123",
                expireDate = new DateTime(2025, 1, 1),
                type = true,
                defaultCard = true,
                Client = client
            };
            cardDao.Create(card);

            Category category = new Category
            {
                name = "category"
            };
            categoryDao.Create(category);

            Product product = new Product
            {
                name = "TestProduct",
                price = 24,
                entryDate = new DateTime(2020, 1, 1),
                stock = 200,
                Category = category
            };
            productDao.Create(product);

            List<ShoppingCartItem> lines = new List<ShoppingCartItem>();

            ShoppingCartItem line1 = new ShoppingCartItem(3, false, ProductMapper.ProductToProductDto(product));

            lines.Add(line1);

            ShoppingCart shoppingCart = new ShoppingCart(72, lines);

            #endregion Declaracion de variables

            long saleId = saleService.Buy(shoppingCart, descName, address, card.id, client.id);

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

            Client client = new Client
            {
                login = "client1",
                password = "password",
                firstName = "firstName",
                lastName = "lastName",
                address = "adress",
                email = "email",
                role = 1,
                language = 1
            };
            clientDao.Create(client);

            Client client2 = new Client
            {
                login = "client2",
                password = "password",
                firstName = "firstName",
                lastName = "lastName",
                address = "adress",
                email = "email",
                role = 1,
                language = 1
            };
            clientDao.Create(client2);

            Card card = new Card
            {
                number = "0000222233334444",
                cvv = "123",
                expireDate = new DateTime(2025, 1, 1),
                type = true,
                defaultCard = true,
                Client = client
            };
            cardDao.Create(card);

            Card card2 = new Card
            {
                number = "1111222233334444",
                cvv = "123",
                expireDate = new DateTime(2025, 1, 1),
                type = true,
                defaultCard = true,
                Client = client2
            };
            cardDao.Create(card2);

            Category category = new Category
            {
                name = "category"
            };
            categoryDao.Create(category);

            Product product = new Product
            {
                name = "TestProduct",
                price = 24,
                entryDate = new DateTime(2020, 1, 1),
                stock = 200,
                Category = category
            };
            productDao.Create(product);

            List<ShoppingCartItem> lines = new List<ShoppingCartItem>();

            ShoppingCartItem line1 = new ShoppingCartItem(3, false, ProductMapper.ProductToProductDto(product));

            lines.Add(line1);

            ShoppingCart shoppingCart1 = new ShoppingCart(72, lines);
            ShoppingCart shoppingCart2 = new ShoppingCart(72, lines);

            #endregion Declaracion de variables

            long saleId = saleService.Buy(shoppingCart1, descName, address, card.id, client.id);

            saleService.Buy(shoppingCart2, descName, address, card2.id, client2.id);

            List<SaleListItemDTO> saleList = saleService.ShowClientSaleList(client.id, 0, 1);

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

            Client client = new Client
            {
                login = "client",
                password = "password",
                firstName = "firstName",
                lastName = "lastName",
                address = "adress",
                email = "email",
                role = 1,
                language = 1
            };
            clientDao.Create(client);

            #endregion Declaracion de variables

            List<SaleListItemDTO> saleList = saleService.ShowClientSaleList(client.id, 0, 1);

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