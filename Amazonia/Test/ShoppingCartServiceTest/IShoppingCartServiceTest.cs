using Es.Udc.DotNet.Amazonia.Model;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CategoryDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.ProductDao;
using Es.Udc.DotNet.Amazonia.Model.ShoppingCartServiceImp;
using Es.Udc.DotNet.Amazonia.Model.ShoppingCartServiceImp.DTOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Transactions;

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

            #endregion Declaracion de variables

            ShoppingCart returnedShoppingCart = shoppingCartService.AddToShoppingCart(shoppingCart, product.id, 3, false);
            returnedShoppingCart = shoppingCartService.AddToShoppingCart(returnedShoppingCart, product2.id, 1, false);

            Assert.AreEqual(2, returnedShoppingCart.items.Count);
            Assert.AreEqual(82, returnedShoppingCart.totalPrice);
        }

        [TestMethod]
        public void TestAddToShoppingCartExistentProduct()
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

            #endregion Declaracion de variables

            ShoppingCart returnedShoppingCart = shoppingCartService.AddToShoppingCart(shoppingCart, product.id, 3, false);
            returnedShoppingCart = shoppingCartService.AddToShoppingCart(returnedShoppingCart, product.id, 1, false);

            Assert.AreEqual(1, returnedShoppingCart.items.Count);
            Assert.AreEqual(96, returnedShoppingCart.totalPrice);
        }

        [TestMethod]
        public void TestDeleteFromShoppingCart()
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

            #endregion Declaracion de variables

            ShoppingCart returnedShoppingCart = shoppingCartService.AddToShoppingCart(shoppingCart, product.id, 3, false);
            returnedShoppingCart = shoppingCartService.AddToShoppingCart(returnedShoppingCart, product2.id, 1, false);

            Assert.AreEqual(2, returnedShoppingCart.items.Count);
            Assert.AreEqual(82, returnedShoppingCart.totalPrice);

            returnedShoppingCart = shoppingCartService.DeleteFromShoppingCart(returnedShoppingCart, product2.id);

            Assert.AreEqual(1, returnedShoppingCart.items.Count);
            Assert.AreEqual(72, returnedShoppingCart.totalPrice);
        }

        [TestMethod]
        public void TestModifyShoppingCartItem()
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

            #endregion Declaracion de variables

            ShoppingCart returnedShoppingCart = shoppingCartService.AddToShoppingCart(shoppingCart, product.id, 3, false);
            returnedShoppingCart = shoppingCartService.AddToShoppingCart(returnedShoppingCart, product2.id, 1, false);

            Assert.AreEqual(2, returnedShoppingCart.items.Count);
            Assert.AreEqual(82, returnedShoppingCart.totalPrice);

            returnedShoppingCart = shoppingCartService.ModifyShoppingCartItem(returnedShoppingCart, product2.id, 10, true);

            Assert.AreEqual(2, returnedShoppingCart.items.Count);
            Assert.AreEqual(172, returnedShoppingCart.totalPrice);

            ShoppingCartItem modifiedItem = returnedShoppingCart.items.Find(x => x.productId == product2.id);

            Assert.AreEqual(10, modifiedItem.units);
            Assert.IsTrue(modifiedItem.gift);
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