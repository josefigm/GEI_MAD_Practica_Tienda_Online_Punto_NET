using Es.Udc.DotNet.Amazonia.Model;
using Es.Udc.DotNet.Amazonia.Model.CommentServiceImp;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CategoryDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CommentDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.ProductDao;
using Es.Udc.DotNet.Amazonia.Model.LabelServiceImp;
using Es.Udc.DotNet.Amazonia.Model.ProductServiceImp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Collections.Generic;
using System.Transactions;

namespace Test.LabelServiceTest
{
    [TestClass]
    public class LabelServiceTest
    {
        private static IKernel kernel;
        private static ICommentDao commentDao;
        private static ICommentService commentService;
        private static ICategoryDao categoryDao;
        private static IProductDao productDao;
        private static ILabelService labelService;
        private static IProductService productService;



        private TransactionScope transactionScope;

        public TestContext TestContext { get; set; }

        public LabelServiceTest()
        {

        }

        [TestMethod]
        public void CreateLabelTest()
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

            Label label = labelService.CreateLabel("Genial", newComment.id);

            #endregion

            List<Label> labels = labelService.FindLabelsByComment(newComment.id);

            Assert.AreEqual(labels.Count, 1);
            Assert.AreEqual(label, labels[0]);

        }

        [TestMethod]
        public void CreateMultipleLabelsTest()
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

            Label label = labelService.CreateLabel("Genial", newComment.id);
            Label label2 = labelService.CreateLabel("Malo", newComment.id);

            #endregion

            List<Label> labels = labelService.FindAllLabels();

            Assert.AreEqual(labels.Count, 2);
            Assert.IsTrue(labels.Contains(label));
            Assert.IsTrue(labels.Contains(label2));

        }

        [TestMethod]
        public void DeleteLabelTest()
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

            Label label = labelService.CreateLabel("Genial", newComment.id);

            labelService.DeleteLabel(label.id);

            #endregion

            List<Label> labels = labelService.FindLabelsByComment(newComment.id);

            Assert.AreEqual(labels.Count, 0);
        }

        #region Additional test attributes

        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();

            commentDao = kernel.Get<ICommentDao>();
            commentService = kernel.Get<ICommentService>();
            categoryDao = kernel.Get<ICategoryDao>();
            productDao = kernel.Get<IProductDao>();
            labelService = kernel.Get<ILabelService>();
            productService = kernel.Get<IProductService>();
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
