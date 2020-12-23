using Es.Udc.DotNet.Amazonia.Model;
using Es.Udc.DotNet.Amazonia.Model.ClientServiceImp;
using Es.Udc.DotNet.Amazonia.Model.CommentServiceImp;
using Es.Udc.DotNet.Amazonia.Model.CommentServiceImp.Exceptions;
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
    public class ICommentServiceTest
    {
        private static IKernel kernel;
        private static ICommentDao commentDao;
        private static ICommentService commentService;
        private static ICategoryDao categoryDao;
        private static IProductDao productDao;
        private static ILabelService labelService;
        private static IClientService clientService;

        // Variables used in several tests are initialized here
        private const string LOGIN = "loginTestprueba";
        private const string LOGIN2 = "loginTestprueba2";
        private const string LOGIN3 = "loginTestprueba3";
        private const string CLEAR_PASSWORD = "password";
        private const string FIRST_NAME = "name";
        private const string LAST_NAME = "lastName";
        private const string EMAIL = "email@testing.net";
        private const string ADDRESS = "address";
        private const byte ROLE = 1;
        private const byte LANGUAGUE = 5;

        private TransactionScope transactionScope;

        public TestContext TestContext { get; set; }

        public ICommentServiceTest()
        {

        }

        // Utility method to create an user
        private Client registerUser(string login)
        {
            Client clientBd = clientService.RegisterClient(login, CLEAR_PASSWORD,
                new ClientDetailsDTO(FIRST_NAME, LAST_NAME, ADDRESS, EMAIL, ROLE, LANGUAGUE));

            return clientBd;
        }

        [TestMethod]
        public void CreateCommentTest()
        {
            using (var scope = new TransactionScope())
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

                Client cliente = registerUser(LOGIN);

                Comment newComment = new Comment();
                newComment.title = "Review bicicleta Felt FZ85";
                newComment.value = "Muy buena bici";
                newComment.productId = biciCarretera.id;
                newComment.clientId = cliente.id;

                commentDao.Create(newComment);

                Comment retrievedComment = commentDao.Find(newComment.id);

                Assert.AreEqual(newComment, retrievedComment);
            }
        }

        [TestMethod]
        public void AssignCommentToAdvertTest()
        {
            using (var scope = new TransactionScope())
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

                Client cliente = registerUser(LOGIN);

                Comment newComment = new Comment();
                newComment.title = "Review bicicleta Felt FZ85";
                newComment.value = "Las ruedas son mejorables, por lo demas excelente bici de iniciación.";
                newComment.productId = biciCarretera.id;
                newComment.clientId = cliente.id;

                commentDao.Create(newComment);

                List<Comment> retrievedComments = commentService.FindCommentsOfProduct(biciCarretera.id);

                Assert.AreEqual(retrievedComments.Count, 1);
                Assert.AreEqual(newComment, retrievedComments[0]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(AlreadyCommentedThisProduct))]
        public void AssignCommentToAdvertUserAlreadyCommentedTest()
        {
            using (var scope = new TransactionScope())
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

                Client cliente = registerUser(LOGIN);

                Comment newComment = commentService.AddComment("Review 1", "Muy buena bicicleta", biciCarretera.id, cliente.id);
                Comment newComment2 = commentService.AddComment("Review 2", "Muy mala bicicleta", biciCarretera.id, cliente.id);

                List<Comment> retrievedComments = commentService.FindCommentsOfProduct(biciCarretera.id);

                Assert.AreEqual(retrievedComments.Count, 1);
                Assert.AreEqual(newComment, retrievedComments[0]);
            }
        }

        [TestMethod]
        public void FindCommentsByLabelTest()
        {
            using (var scope = new TransactionScope())
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

                #region Create 3 Comments to a label

                Client cliente = registerUser(LOGIN);
                Client cliente2 = registerUser(LOGIN2);
                Client cliente3 = registerUser(LOGIN3);

                Comment newComment1 = new Comment();
                newComment1.title = "Review bicicleta Felt FZ85";
                newComment1.value = "Muy buena bici";
                newComment1.productId = biciCarretera.id;
                newComment1.clientId = cliente.id;

                commentDao.Create(newComment1);
                Label label = labelService.CreateLabel("Bicicletas Molonas", newComment1.id);

                List<long> labelIds = new List<long>();
                labelIds.Add(label.id);

                Comment newComment2 = new Comment();
                newComment2.title = "Review bicicleta Felt FZ85";
                newComment2.value = "Menudo pepino";
                newComment2.productId = biciCarretera.id;
                newComment2.clientId = cliente2.id;

                commentDao.Create(newComment2);
                labelService.AssignLabelsToComment(newComment2.id, labelIds);

                Comment newComment3 = new Comment();
                newComment3.title = "Review bicicleta Felt FZ85";
                newComment3.value = "Menudo maquinote";
                newComment3.productId = biciCarretera.id;
                newComment3.clientId = cliente3.id;

                commentDao.Create(newComment3);
                labelService.AssignLabelsToComment(newComment3.id, labelIds);

                #endregion

                List<Comment> result = commentService.FindCommentsByLabel(label.id);

                Assert.AreEqual(true, result.Contains(newComment1));
                Assert.AreEqual(true, result.Contains(newComment2));
                Assert.AreEqual(true, result.Contains(newComment3));
            }

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
            clientService = kernel.Get<IClientService>();
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


