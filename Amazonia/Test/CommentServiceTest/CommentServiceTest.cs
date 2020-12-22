using Es.Udc.DotNet.Amazonia.Model;
using Es.Udc.DotNet.Amazonia.Model.CommentServiceImp;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CategoryDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CommentDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.ProductDao;
using Es.Udc.DotNet.Amazonia.Model.LabelServiceImp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Collections.Generic;
using System.Transactions;

namespace Test.CommentServiceTest
{
    [TestClass]
    public class CommentServiceTest
    {
        private static IKernel kernel;
        private static ICommentDao commentDao;
        private static ICommentService commentService;
        private static ICategoryDao categoryDao;
        private static IProductDao productDao;
        private static ILabelService labelService;

        private TransactionScope transactionScope;

        public TestContext TestContext { get; set; }

        public CommentServiceTest()
        {

        }

        [TestMethod]
        public void CreateCommentTest()
        {
            #region declaration section
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

            productDao.Create(biciCarretera);
            #endregion

            Comment newComment = new Comment();
            newComment.title = "Review bicicleta Felt FZ85";
            newComment.value = "Muy buena bici";
            newComment.productId = biciCarretera.id;

            commentDao.Create(newComment);

            Comment retrievedComment = commentDao.Find(newComment.id);

            Assert.AreEqual(newComment, retrievedComment);
        }

        [TestMethod]
        public void AssignCommentToAdvertTest()
        {
            #region declaration section
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

            productDao.Create(biciCarretera);
            #endregion

            Comment newComment = new Comment();
            newComment.title = "Review bicicleta Felt FZ85";
            newComment.value = "Las ruedas son mejorables, por lo demas excelente bici de iniciación.";
            newComment.productId = biciCarretera.id;

            commentDao.Create(newComment);

            List<Comment> retrievedComments = commentService.FindCommentsOfProduct(biciCarretera.id);

            Assert.AreEqual(retrievedComments.Count, 1);
            Assert.AreEqual(newComment, retrievedComments[0]);
        }

        [TestMethod]
        public void FindCommentsByLabelTest()
        {
                        
            #region Create Category/Product

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

            productDao.Create(biciCarretera);
            #endregion

            #region Create Comment to a label

            Comment newComment1 = new Comment();
            newComment1.title = "Review bicicleta Felt FZ85";
            newComment1.value = "Muy buena bici";
            newComment1.productId = biciCarretera.id;
            
            commentDao.Create(newComment1);

            Label label = labelService.CreateLabel("Bicicletas Molonas", newComment1.id);

            #endregion

            List<Comment> result = commentService.FindCommentsByLabel(label.id);

            Assert.AreEqual(true, result.Contains(newComment1));

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


