using System.Transactions;
using System.Collections.Generic;
using Ninject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Es.Udc.DotNet.Amazonia.Model.ProductServiceImp;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CategoryDao;
using Es.Udc.DotNet.Amazonia.Model;
using Es.Udc.DotNet.Amazonia.Model.DAOs.ProductDao;
using Es.Udc.DotNet.Amazonia.Model.CommentServiceImp;
using Es.Udc.DotNet.Amazonia.Model.LabelServiceImp;

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
        private static ICommentService commentService;
        private static ILabelService labelService;
        private static IProductDao productDao;
        private static ICategoryDao categoryDao;

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
        public void TestMethod1()
        {
            Category c1 = new Category();
            c1.name = "c1";
            Category c2 = new Category();
            c2.name = "c2";

            categoryDao.Create(c1);
            categoryDao.Create(c2);

            List<Category> categoriesFound = productService.FindCategories();

            Assert.AreEqual(2, categoriesFound.Count);
        }

        [TestMethod]
        public void RetrieveProductsWithLabelEmptyTest()
        {
            #region Needed variables
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

            #region Comment and label section
            Comment newComment = commentService.AddComment("Review 1", "Muy buena bicicleta", biciCarretera.id);

            labelService.CreateLabel("Genial", newComment.id);

            #endregion

            List<Product> productsWithLabel = productService.RetrieveProductsWithLabel("Malo");

            Assert.AreEqual(productsWithLabel.Capacity, 0);
        
        }

        [TestMethod]
        public void RetrieveProductWithLabelTest()
        {
            #region Needed variables
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

            #region Comment and label section
            Comment newComment = commentService.AddComment("Review 1", "Muy buena bicicleta", biciCarretera.id);
            labelService.CreateLabel("Genial", newComment.id);

            #endregion

            List<Product> productsWithLabel = productService.RetrieveProductsWithLabel("Genial");

            Assert.AreEqual(productsWithLabel.Capacity, 1);
            Assert.AreEqual(productsWithLabel[0], biciCarretera);
        }

        [TestMethod]
        public void RetrieveProductsWithLabelTest()
        {
            #region Needed variables
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

            #region Comment and label section
            Comment newComment = commentService.AddComment("Review 1", "Muy buena bicicleta", biciCarretera.id);
            Comment newComment2 = commentService.AddComment("Review 1", "Muy mala bicicleta", biciMontaña.id);
            Comment newComment3 = commentService.AddComment("Review 1", "Muy buen portátil", portatil.id);

            labelService.CreateLabel("Genial", newComment.id);
            labelService.CreateLabel("Mala", newComment2.id);
            labelService.CreateLabel("Genial", newComment3.id);

            #endregion

            List<Product> productsWithLabel = productService.RetrieveProductsWithLabel("Genial");

            Assert.AreEqual(productsWithLabel.Capacity, 2);
            Assert.IsTrue(productsWithLabel.Contains(portatil));
            Assert.IsTrue(productsWithLabel.Contains(biciCarretera));
        }

        #region Additional test attributes

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();

            categoryDao = kernel.Get<ICategoryDao>();
            productService = kernel.Get<IProductService>();
            commentService = kernel.Get<ICommentService>();
            labelService = kernel.Get<ILabelService>();
            productDao = kernel.Get<IProductDao>();
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
