using Es.Udc.DotNet.Amazonia.Model;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CategoryDao;
using Es.Udc.DotNet.Amazonia.Model.ProductServiceImp;
using Es.Udc.DotNet.Amazonia.Model.ProductServiceImp.DTOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Collections.Generic;
using System.Transactions;

namespace Test.ProductService
{
    [TestClass]
    public class CacheTest
    {
        private static IKernel kernel;
        private static IProductService productService;
        private static ICategoryDao categoryDao;

        private TransactionScope transactionScope;

        public TestContext TestContext { get; set; }

        public CacheTest()
        {
        }

        [TestMethod]
        public void TestCache()
        {
            #region Declaracion de variables

            Category c1 = new Category();
            c1.name = "Bicicletas";
            Category c2 = new Category();
            c2.name = "Ordenadores";

            categoryDao.Create(c1);
            categoryDao.Create(c2);

            Product biciCarretera = new Product();
            Product biciMontaña = new Product();
            Product portatil = new Product();

            double price = 1200;
            System.DateTime date = System.DateTime.Now;
            long stock = 5;
            string image = "ccc";
            string description = "Bicicleta";
            long categoryIdBicicleta = c1.id;
            long categoryIdPortatil = c2.id;

            biciCarretera.name = "Bicicleta Felt FZ85";
            biciCarretera.price = price;
            biciCarretera.entryDate = date;
            biciCarretera.stock = stock;
            biciCarretera.image = image;
            biciCarretera.description = description;
            biciCarretera.categoryId = categoryIdBicicleta;

            biciMontaña.name = "Bicicleta Felt FZ85";
            biciMontaña.price = price;
            biciMontaña.entryDate = date;
            biciMontaña.stock = stock;
            biciMontaña.image = image;
            biciMontaña.description = description;
            biciMontaña.categoryId = categoryIdBicicleta;

            portatil.name = "Portatil Dell XPS";
            portatil.price = price;
            portatil.entryDate = date;
            portatil.stock = stock;
            portatil.image = image;
            portatil.description = description;
            portatil.categoryId = categoryIdPortatil;

            #endregion Declaracion de variables

            #region Persistencia

            productService.CreateProduct(biciCarretera);
            productService.CreateProduct(portatil);
            productService.CreateProduct(biciMontaña);

            #endregion Persistencia

            List<ProductDTO> listaEsperadaBicicletas = new List<ProductDTO>(2);
            listaEsperadaBicicletas.Add(ProductMapper.ProductToProductDto(biciCarretera));
            listaEsperadaBicicletas.Add(ProductMapper.ProductToProductDto(biciMontaña));

            Assert.IsFalse(productService.Cache.Contains("bicicleta"));

            List<ProductDTO> listaRecuperadaBicicletas = productService.FindProductByWord("bicicleta", 0, 2);

            Assert.IsTrue(listaRecuperadaBicicletas.Count == 2);
            CollectionAssert.AreEqual(listaEsperadaBicicletas, listaRecuperadaBicicletas);

            Assert.IsTrue(productService.Cache.Contains("bicicleta"));

            List<ProductDTO> resultadoCache = productService.FindProductByWord("bicicleta", 0, 2);
            CollectionAssert.AreEqual(listaEsperadaBicicletas, resultadoCache);
        }


        #region Additional test attributes

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();

            productService = kernel.Get<IProductService>();
            categoryDao = kernel.Get<ICategoryDao>();
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