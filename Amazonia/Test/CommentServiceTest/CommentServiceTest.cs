using Es.Udc.DotNet.Amazonia.Model;
using Es.Udc.DotNet.Amazonia.Model.CommentServiceImp;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CategoryDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CommentDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.ProductDao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Transactions;

namespace Test.CommentServiceTest
{
    class CommentServiceTest
    {
        private static IKernel kernel;
        private static ICommentDao commentDao;
        private static ICommentService commentService;
        private static ICategoryDao categoryDao;
        private static IProductDao productDao;

        private TransactionScope transactionScope;

        public TestContext TestContext { get; set; }

        public CommentServiceTest()
        {

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

        [TestMethod]
        public void CreateCommentTest()
        {
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



            Comment newComment = new Comment();
            newComment.title = "Review bicicleta Felt FZ85";
            newComment.value = "Muy buena bici";
            newComment.productId = biciCarretera.id;

            commentDao.Create(newComment);

            Comment retrievedComment = commentDao.Find(biciCarretera.id);

            Assert.AreEqual(newComment, retrievedComment);

        }

    }
}


