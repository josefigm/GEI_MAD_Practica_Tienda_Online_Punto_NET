using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Es.Udc.DotNet.Amazonia.Model.DAOs.ProductDao;
using System.Transactions;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CategoryDao;
using Es.Udc.DotNet.Amazonia.Model;
using Es.Udc.DotNet.Amazonia.Model.ShoppingCartServiceImp.DTOs;
using Es.Udc.DotNet.Amazonia.Model.ShoppingCartServiceImp.Exceptions;
using Es.Udc.DotNet.Amazonia.Model.ShoppingCartServiceImp;

namespace Test.ShoppingCartServiceTest
{
    /// <summary>
    /// Descripción resumida de IShoppingCartServiceTest
    /// </summary>
    [TestClass]
    public class IShoppingCartServiceTest
    {
        private static IKernel kernel;
        private static IShoppingCartService shoppingCartService;
        private static IProductDao productDao;
        private static ICategoryDao categoryDao;

        private TransactionScope transactionScope;

        public TestContext TestContext { get; set; }

        public IShoppingCartServiceTest()
        {
        }

        [TestMethod]
        public void TestAddToShoppingCart()
        {
            #region Declaracion de variables
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

            Product product2 = new Product();
            product2.name = "TestProduct2";
            product2.price = 10;
            product2.entryDate = new DateTime(2020, 1, 1);
            product2.stock = 200;
            product2.Category = category;
            productDao.Create(product2);

            ShoppingCart shoppingCart = new ShoppingCart();

            ShoppingCartItem item1 = new ShoppingCartItem(3, false, product.id);
            ShoppingCartItem item2 = new ShoppingCartItem(1, false, product2.id);

            #endregion Declaracion de variables

            ShoppingCart returnedShoppingCart = shoppingCartService.AddToShoppingCart(shoppingCart, item1);
            returnedShoppingCart = shoppingCartService.AddToShoppingCart(returnedShoppingCart, item2);

            Assert.AreEqual(2, returnedShoppingCart.items.Count);
            Assert.AreEqual(82, returnedShoppingCart.totalPrice);

        }

        [TestMethod]
        [ExpectedException(typeof(ProductAlreadyOnShoppingCartException))]
        public void TestAddToShoppingCartException()
        {
            #region Declaracion de variables
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

            ShoppingCart shoppingCart = new ShoppingCart();

            ShoppingCartItem item1 = new ShoppingCartItem(3, false, product.id);
            ShoppingCartItem item2 = new ShoppingCartItem(3, false, product.id);

            #endregion Declaracion de variables

            ShoppingCart returnedShoppingCart = shoppingCartService.AddToShoppingCart(shoppingCart, item1);
            returnedShoppingCart = shoppingCartService.AddToShoppingCart(returnedShoppingCart, item2);

        }

        #region Additional test attributes

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();

            categoryDao = kernel.Get<ICategoryDao>();
            productDao = kernel.Get<IProductDao>();
            shoppingCartService = kernel.Get<IShoppingCartService>();
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
