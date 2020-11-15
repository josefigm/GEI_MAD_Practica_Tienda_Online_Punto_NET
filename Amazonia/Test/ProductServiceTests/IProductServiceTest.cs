using System.Transactions;
using System.Collections.Generic;
using Ninject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Es.Udc.DotNet.Amazonia.Model.ProductServiceImp;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CategoryDao;
using Es.Udc.DotNet.Amazonia.Model;
using Es.Udc.DotNet.Amazonia.Model.DAOs.ProductDao;
using Es.Udc.DotNet.Amazonia.Model.ProductServiceImp.DTOs;

namespace Test.ProductService
{
    /// <summary>
    /// Descripción resumida de IProductServiceTest
    /// </summary>
    [TestClass]
    public class IProductServiceTest
    {

        private static IKernel kernel;
        private static IProductService productService;
        private static ICategoryDao categoryDao;
        private static IProductDao productDao;

        private TransactionScope transactionScope;

        public TestContext TestContext { get; set; }

        public IProductServiceTest()
        {

        }

        #region Atributos de prueba adicionales
        //
        // Puede usar los siguientes atributos adicionales conforme escribe las pruebas:
        //
        // Use ClassInitialize para ejecutar el código antes de ejecutar la primera prueba en la clase
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup para ejecutar el código una vez ejecutadas todas las pruebas en una clase
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Usar TestInitialize para ejecutar el código antes de ejecutar cada prueba 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup para ejecutar el código una vez ejecutadas todas las pruebas
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestFindCategories()
        {
            Category c1 = new Category();
            c1.name = "c1";
            Category c2 = new Category();
            c2.name = "c2";

            categoryDao.Create(c1);
            categoryDao.Create(c2);

            List<Category> categoriesExpected = new List<Category>(2);

            categoriesExpected.Add(c1);
            categoriesExpected.Add(c2);
            
            List<Category> categoriesFound = productService.FindCategories();

            Assert.AreEqual(2, categoriesFound.Count);
            CollectionAssert.AreEqual(categoriesExpected, categoriesFound);
        }


        [TestMethod]
        public void TestCreateProduct()
        {
            #region Declaracion de variables   
            Category c1 = new Category();
            c1.name = "Bicicletas";
            categoryDao.Create(c1);

            Product biciCarretera = new Product();

            double price = 1200;
            System.DateTime date = System.DateTime.Now;
            long stock = 5;
            string image = "ccc";
            string description = "Bicicleta";
            long categoryIdBicicleta = c1.id;

            biciCarretera.name = "Bicicleta Felt FZ85";
            biciCarretera.price = price;
            biciCarretera.entryDate = date;
            biciCarretera.stock = stock;
            biciCarretera.image = image;
            biciCarretera.description = description;
            biciCarretera.categoryId = categoryIdBicicleta;
            #endregion

            #region Persistencia
            productDao.Create(biciCarretera);
            #endregion

            Product retrievedProduct = productDao.Find(biciCarretera.id);
            Assert.AreEqual(biciCarretera, retrievedProduct);

        }




        [TestMethod]
        public void TestFindByKeywordEmpty()
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
            #endregion

            #region Persistencia
            productDao.Create(biciCarretera);
            productDao.Create(portatil);
            productDao.Create(biciMontaña);
            #endregion


            List<ProductDTO> listaRecuperada = productService.FindProductByWordAndCategory("Cacahuete", null);

            Assert.IsTrue(listaRecuperada.Count == 0);
        }

        [TestMethod]
        public void TestFindByKeywordOneResultAndTestKeywordTrimAndNoCaseSensitive()
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
            #endregion

            #region Persistencia
            productDao.Create(biciCarretera);
            productDao.Create(portatil);
            productDao.Create(biciMontaña);
            #endregion

            List<ProductDTO> listaEsperadaOrdenador = new List<ProductDTO>(1);
            listaEsperadaOrdenador.Add(ProductMapper.ProductToProductDto(portatil));
            List<ProductDTO> listaRecuperadaOrdenador = productService.FindProductByWordAndCategory("     ordenaDoReS    ", null);

            Assert.IsTrue(listaRecuperadaOrdenador.Count == 1);
            CollectionAssert.AreEqual(listaEsperadaOrdenador, listaRecuperadaOrdenador);

            List<ProductDTO> listaRecuperadaBicicletas = productService.FindProductByWordAndCategory("bicicleta", null);

            Assert.IsTrue(listaRecuperadaBicicletas.Count == 2);
        }

        [TestMethod]
        public void TestFindByKeywordMultipleResultsAndTestKeywordTrimAndNoCaseSensitive()
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
            #endregion

            #region Persistencia
            productDao.Create(biciCarretera);
            productDao.Create(portatil);
            productDao.Create(biciMontaña);
            #endregion

            List<ProductDTO> listaEsperadaBicicletas = new List<ProductDTO>(2);
            listaEsperadaBicicletas.Add(ProductMapper.ProductToProductDto(biciCarretera));
            listaEsperadaBicicletas.Add(ProductMapper.ProductToProductDto(biciMontaña));

            List<ProductDTO> listaRecuperadaBicicletas = productService.FindProductByWordAndCategory("bicicleta", null);

            Assert.IsTrue(listaRecuperadaBicicletas.Count == 2);
            CollectionAssert.AreEqual(listaEsperadaBicicletas, listaRecuperadaBicicletas);

        }

        [TestMethod]
        public void TestFindByKeywordAndCategoryMultipleResultsAndTestKeywordTrimAndNoCaseSensitive()
        {
            #region Declaracion de variables   
            Category c1 = new Category();
            c1.name = "Bicicletas";
            Category c2 = new Category();
            c2.name = "Bicicletas Outlet";

            categoryDao.Create(c1);
            categoryDao.Create(c2);

            Product biciCarretera = new Product();
            Product biciMontaña = new Product();

            double price = 1200;
            System.DateTime date = System.DateTime.Now;
            long stock = 5;
            string image = "ccc";
            string description = "Bicicleta";
            long categoryIdBicicleta = c1.id;
            long categoryIdBicicletaOutlet = c2.id;


            biciCarretera.name = "Bicicleta Felt FZ85";
            biciCarretera.price = price;
            biciCarretera.entryDate = date;
            biciCarretera.stock = stock;
            biciCarretera.image = image;
            biciCarretera.description = description;
            biciCarretera.categoryId = categoryIdBicicleta;

            biciMontaña.name = "Bicicleta MMR";
            biciMontaña.price = price;
            biciMontaña.entryDate = date;
            biciMontaña.stock = stock;
            biciMontaña.image = image;
            biciMontaña.description = description;
            biciMontaña.categoryId = categoryIdBicicleta;
            #endregion

            #region Persistencia
            productDao.Create(biciCarretera);
            productDao.Create(biciMontaña);
            #endregion

            List<ProductDTO> listaEsperadaBicicletas = new List<ProductDTO>(1);
            listaEsperadaBicicletas.Add(ProductMapper.ProductToProductDto(biciCarretera));

            List<ProductDTO> listaRecuperadaBicicletas = productService.FindProductByWordAndCategory("   bicicLeta  ", c1);

            Assert.IsTrue(listaRecuperadaBicicletas.Count == 1);
            CollectionAssert.AreEqual(listaEsperadaBicicletas, listaRecuperadaBicicletas);

        }

        #region Additional test attributes

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();

            categoryDao = kernel.Get<ICategoryDao>();
            productService = kernel.Get<IProductService>();
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
