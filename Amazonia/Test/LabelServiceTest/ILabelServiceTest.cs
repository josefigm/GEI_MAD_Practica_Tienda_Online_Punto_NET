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
using System;
using Es.Udc.DotNet.Amazonia.Model.LabelServiceImp.DTOs;
using Es.Udc.DotNet.Amazonia.Model.ClientServiceImp;
using System.Management.Instrumentation;
using Es.Udc.DotNet.Amazonia.Model.DAOs.LabelDao;

namespace Test.LabelServiceTest
{
    [TestClass]
    public class ILabelServiceTest
    {
        private static IKernel kernel;
        private static ICommentDao commentDao;
        private static ICommentService commentService;
        private static ICategoryDao categoryDao;
        private static IProductDao productDao;
        private static ILabelDao labelDao;
        private static ILabelService labelService;
        private static IProductService productService;
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
        private const string LANGUAGUE = "en";
        private const string COUNTRY = "en";

        private TransactionScope transactionScope;

        public TestContext TestContext { get; set; }

        public ILabelServiceTest()
        {

        }

        // Utility method to create an user
        private Client registerUser(string login)
        {
            Client clientBd = clientService.RegisterClient(login, CLEAR_PASSWORD,
                new ClientDTO(FIRST_NAME, LAST_NAME, ADDRESS, EMAIL, ROLE, LANGUAGUE, COUNTRY));

            return clientBd;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateLabelValueNullTest()
        {
            labelService.CreateLabel("");
        }

        [TestMethod]
        public void CreateLabelTest()
        {
            using (var scope = new TransactionScope())
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

                Client cliente = registerUser(LOGIN);

                long newCommentId = commentService.AddComment("Review 1", "Muy buena bicicleta", biciCarretera.id, cliente.id);

                LabelDTO label = labelService.CreateLabel("Genial");

                #endregion

                List<LabelDTO> labels = labelService.FindAllLabels();

                Assert.AreEqual(labels.Count, 1);
                Assert.AreEqual(label, labels[0]);
            }

        }

        [TestMethod]
        public void CreateMultipleLabelsTest()
        {
            using (var scope = new TransactionScope())
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
                Client cliente = registerUser(LOGIN);
                long newCommentId = commentService.AddComment("Review 1", "Muy buena bicicleta", biciCarretera.id, cliente.id);

                LabelDTO label = labelService.CreateLabel("Genial");
                LabelDTO label2 = labelService.CreateLabel("Malo");

                #endregion

                List<LabelDTO> labels = labelService.FindAllLabels();

                Assert.IsTrue(labels.Contains(label));
                Assert.IsTrue(labels.Contains(label2));
            }

        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void AssignLabelsToNonExistentCommentTest()
        {
            using (var scope = new TransactionScope())
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

                Client cliente = registerUser(LOGIN);

                long newCommentId = commentService.AddComment("Review 1", "Muy buena bicicleta", biciCarretera.id, cliente.id);

                LabelDTO label = labelService.CreateLabel("Genial");

                List<long> labelIds = new List<long>();
                labelIds.Add(label.id);

                labelService.AssignLabelsToComment(newCommentId, labelIds);

                // Non existent comment id
                labelService.AssignLabelsToComment(99999L, labelIds);
            }


        }

        [TestMethod]
        public void AssignLabelsToCommentTest()
        {
            using (var scope = new TransactionScope())
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

                Client cliente = registerUser(LOGIN);
                Client cliente2 = registerUser(LOGIN2);

                long newCommentId = commentService.AddComment("Review 1", "Muy buena bicicleta", biciCarretera.id, cliente.id);
                long newComment2Id = commentService.AddComment("Review 2", "Muy mala bicicleta", biciCarretera.id, cliente2.id);

                LabelDTO label = labelService.CreateLabel("Genial");
                LabelDTO label2 = labelService.CreateLabel("Ridicula");
                LabelDTO label3 = labelService.CreateLabel("Espectacular");

                List<long> labelIds = new List<long>();
                labelIds.Add(label.id);
                labelIds.Add(label2.id);
                labelIds.Add(label3.id);

                labelService.AssignLabelsToComment(newCommentId, labelIds);
                labelService.AssignLabelsToComment(newComment2Id, labelIds);

                Comment newComment2 = commentDao.Find(newComment2Id);

                Label label1Found = labelDao.Find(label.id);
                Label label2Found = labelDao.Find(label2.id);
                Label label3Found = labelDao.Find(label3.id);

                Assert.IsTrue(label1Found.Comments.Contains(newComment2));
                Assert.IsTrue(label2Found.Comments.Contains(newComment2));
                Assert.IsTrue(label3Found.Comments.Contains(newComment2));
            }
        }

        [TestMethod]
        public void GetNumberOfCommentsTest()
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

            Client cliente = registerUser(LOGIN);
            Client cliente2 = registerUser(LOGIN2);
            Client cliente3 = registerUser(LOGIN3);

            long newCommentId = commentService.AddComment("Review 1", "Muy buena bicicleta", biciCarretera.id, cliente.id);
            long newComment2Id = commentService.AddComment("Review 2", "Buena bicicleta", biciCarretera.id, cliente2.id);
            long newComment3Id = commentService.AddComment("Review 3", "Mejorable", biciCarretera.id, cliente3.id);

            LabelDTO label = labelService.CreateLabel("Genial");
            LabelDTO label2 = labelService.CreateLabel("Ridicula");

            List<long> labelIds = new List<long>();
            labelIds.Add(label.id);

            List<long> label2Ids = new List<long>();
            label2Ids.Add(label.id);
            label2Ids.Add(label2.id);

            labelService.AssignLabelsToComment(newCommentId, labelIds);
            labelService.AssignLabelsToComment(newComment2Id, label2Ids);
            labelService.AssignLabelsToComment(newComment3Id, labelIds);

            List<int> numberOfCommentsForLabel = labelService.GetNumberOfComments(label2Ids);

            List<int>.Enumerator commentsEnum = numberOfCommentsForLabel.GetEnumerator();
            commentsEnum.MoveNext();
            int n = commentsEnum.Current;

            Assert.AreEqual(3, n);

            commentsEnum.MoveNext();
            n = commentsEnum.Current;

            Assert.AreEqual(1, n);

        }

        [TestMethod]
        public void findMostUsedLabelsTest()
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

            Client cliente = registerUser(LOGIN);
            Client cliente2 = registerUser(LOGIN2);
            Client cliente3 = registerUser(LOGIN3);

            long newCommentId = commentService.AddComment("Review 1", "Muy buena bicicleta", biciCarretera.id, cliente.id);
            long newComment2Id = commentService.AddComment("Review 2", "Buena bicicleta", biciCarretera.id, cliente2.id);
            long newComment3Id = commentService.AddComment("Review 3", "Mejorable", biciCarretera.id, cliente3.id);

            LabelDTO label = labelService.CreateLabel("Genial");
            LabelDTO label2 = labelService.CreateLabel("Ridicula");
            LabelDTO label3 = labelService.CreateLabel("Penosa");

            labelService.AssignLabelsToComment(newCommentId, new List<long> { label.id, label2.id });
            labelService.AssignLabelsToComment(newComment2Id, new List<long> { label.id, label2.id });
            labelService.AssignLabelsToComment(newComment3Id, new List<long> { label.id, label3.id });

            // At this point, label is assigned to comments 1, 2 & 3
            // At this point, label 2 is assigned to comments 1 & 2
            // At this point, label 3 is assigned to comments 3

            List<LabelDTO> labelIds = labelService.FindMostUsedLabels(3);

            Assert.IsTrue(labelIds.Count == 3);
            Assert.IsTrue(labelIds[0].id == label.id);
            Assert.IsTrue(labelIds[1].id == label2.id);
            Assert.IsTrue(labelIds[2].id == label3.id);
        }

        [TestMethod]
        public void findMostUsedLabelsLimitedTest()
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
            Client cliente = registerUser(LOGIN);
            Client cliente2 = registerUser(LOGIN2);
            Client cliente3 = registerUser(LOGIN3);

            long newCommentId = commentService.AddComment("Review 1", "Muy buena bicicleta", biciCarretera.id, cliente.id);
            long newComment2Id = commentService.AddComment("Review 2", "Buena bicicleta", biciCarretera.id, cliente2.id);
            long newComment3Id = commentService.AddComment("Review 3", "Mejorable", biciCarretera.id, cliente3.id);

            LabelDTO label = labelService.CreateLabel("Genial");
            LabelDTO label2 = labelService.CreateLabel("Ridicula");
            LabelDTO label3 = labelService.CreateLabel("Penosa");

            labelService.AssignLabelsToComment(newCommentId, new List<long> { label.id, label2.id });
            labelService.AssignLabelsToComment(newComment2Id, new List<long> { label.id, label2.id });
            labelService.AssignLabelsToComment(newComment3Id, new List<long> { label.id, label3.id });

            // At this point, label is assigned to comments 1, 2 & 3
            // At this point, label 2 is assigned to comments 1 & 2
            // At this point, label 3 is assigned to comments 3

            List<LabelDTO> labelIds = labelService.FindMostUsedLabels(2);

            Assert.IsTrue(labelIds.Count == 2);
            Assert.IsTrue(labelIds[0].id == label.id);
            Assert.IsTrue(labelIds[1].id == label2.id);
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
            labelDao = kernel.Get<ILabelDao>();
            labelService = kernel.Get<ILabelService>();
            productService = kernel.Get<IProductService>();
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
