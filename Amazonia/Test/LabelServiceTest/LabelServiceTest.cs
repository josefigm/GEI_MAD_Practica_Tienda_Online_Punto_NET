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
        [ExpectedException(typeof(ArgumentException))]
        public void CreateLabelValueNullTest()
        {
            Label label = labelService.CreateLabel("", 1L);
        }

        [TestMethod]
        [ExpectedException(typeof(Es.Udc.DotNet.ModelUtil.Exceptions.InstanceNotFoundException))]
        public void CreateLabelWithNoExistentCommentTest()
        {
            Label label = labelService.CreateLabel("Hola", 9999L);
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
                Comment newComment = commentService.AddComment("Review 1", "Muy buena bicicleta", biciCarretera.id);

                Label label = labelService.CreateLabel("Genial", newComment.id);

                #endregion

                List<Label> labels = labelService.FindLabelsByComment(newComment.id);

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
                Comment newComment = commentService.AddComment("Review 1", "Muy buena bicicleta", biciCarretera.id);

                Label label = labelService.CreateLabel("Genial", newComment.id);
                Label label2 = labelService.CreateLabel("Malo", newComment.id);

                #endregion

                List<Label> labels = labelService.FindAllLabels();

                Assert.AreEqual(labels.Count, 2);
                Assert.IsTrue(labels.Contains(label));
                Assert.IsTrue(labels.Contains(label2));
            }

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AssignLabelsToCommentEmptyLabelsListTest()
        {
            labelService.AssignLabelsToComment(1L, new List<long>());
        }

        [TestMethod]
        [ExpectedException(typeof(Es.Udc.DotNet.ModelUtil.Exceptions.InstanceNotFoundException))]
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

                Comment newComment = commentService.AddComment("Review 1", "Muy buena bicicleta", biciCarretera.id);

                Label label = labelService.CreateLabel("Genial", newComment.id);

                List<long> labelIds = new List<long>();
                labelIds.Add(label.id);

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

                Comment newComment = commentService.AddComment("Review 1", "Muy buena bicicleta", biciCarretera.id);
                Comment newComment2 = commentService.AddComment("Review 2", "Muy mala bicicleta", biciCarretera.id);

                Label label = labelService.CreateLabel("Genial", newComment.id);
                Label label2 = labelService.CreateLabel("Ridicula", newComment.id);
                Label label3 = labelService.CreateLabel("Espectacular", newComment.id);

                List<long> labelIds = new List<long>();
                labelIds.Add(label.id);
                labelIds.Add(label2.id);
                labelIds.Add(label3.id);

                labelService.AssignLabelsToComment(newComment2.id, labelIds);

                Assert.IsTrue(label.Comments.Contains(newComment2));
                Assert.IsTrue(label2.Comments.Contains(newComment2));
                Assert.IsTrue(label3.Comments.Contains(newComment2));
            }
        }

        [TestMethod]
        public void UpdateLabelTest()
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
                Comment newComment = commentService.AddComment("Review 1", "Muy buena bicicleta", biciCarretera.id);

                Label label = labelService.CreateLabel("Genial", newComment.id);

                #endregion

                labelService.UpdateLabel(label.id, "Bien");

                Assert.IsTrue(label.value.Equals("Bien"));
            }
        }

        [TestMethod]
        public void DeleteLabelTest()
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
                Comment newComment = commentService.AddComment("Review 1", "Muy buena bicicleta", biciCarretera.id);

                Label label = labelService.CreateLabel("Genial", newComment.id);

                labelService.DeleteLabel(label.id);

                #endregion

                List<Label> labels = labelService.FindLabelsByComment(newComment.id);

                Assert.AreEqual(labels.Count, 0);
            }
        }

        [TestMethod]
        public void DeleteLabelsFromCommentTest()
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

                Comment newComment = commentService.AddComment("Review 1", "Muy buena bicicleta", biciCarretera.id);

                Label label = labelService.CreateLabel("Genial", newComment.id);
                Label label2 = labelService.CreateLabel("Ridicula", newComment.id);
                Label label3 = labelService.CreateLabel("Espectacular", newComment.id);

                List<long> labelIds = new List<long>();
                labelIds.Add(label.id);
                labelIds.Add(label2.id);
                labelIds.Add(label3.id);

                labelService.AssignLabelsToComment(newComment.id, labelIds);
                labelService.DeleteLabelsFromComment(newComment.id, labelIds);

                Assert.IsTrue(!label.Comments.Contains(newComment));
                Assert.IsTrue(!label2.Comments.Contains(newComment));
                Assert.IsTrue(!label3.Comments.Contains(newComment));
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

            Comment newComment1 = commentService.AddComment("Review 1", "Muy buena bicicleta", biciCarretera.id);
            Comment newComment2 = commentService.AddComment("Review 2", "Buena bicicleta", biciCarretera.id);
            Comment newComment3 = commentService.AddComment("Review 3", "Mejorable", biciCarretera.id);

            Label label = labelService.CreateLabel("Genial", newComment1.id);
            Label label2 = labelService.CreateLabel("Ridicula", newComment3.id);

            List<long> labelIds = new List<long>();
            labelIds.Add(label.id);

            List<long> label2Ids = new List<long>();
            label2Ids.Add(label.id);
            label2Ids.Add(label2.id);

            labelService.AssignLabelsToComment(newComment2.id, label2Ids);
            labelService.AssignLabelsToComment(newComment3.id, labelIds);

            List<int> numberOfCommentsForLabel = labelService.GetNumberOfComments(label2Ids);

            List<int>.Enumerator commentsEnum = numberOfCommentsForLabel.GetEnumerator();
            commentsEnum.MoveNext();
            int n = commentsEnum.Current;

            Assert.AreEqual(3, n);

            commentsEnum.MoveNext();
            n = commentsEnum.Current;

            Assert.AreEqual(2, n);

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

            Comment newComment1 = commentService.AddComment("Review 1", "Muy buena bicicleta", biciCarretera.id);
            Comment newComment2 = commentService.AddComment("Review 2", "Buena bicicleta", biciCarretera.id);
            Comment newComment3 = commentService.AddComment("Review 3", "Mejorable", biciCarretera.id);

            Label label = labelService.CreateLabel("Genial", newComment1.id);
            // Label 1 is assigned to comment 2
            labelService.AssignLabelsToComment(newComment2.id, new List<long> { label.id });
            // Label 1 is assigned to comment 3
            labelService.AssignLabelsToComment(newComment3.id, new List<long> { label.id });

            Label label2 = labelService.CreateLabel("Ridicula", newComment2.id);
            // Label 2 is assigned to comment 1
            labelService.AssignLabelsToComment(newComment1.id, new List<long> { label2.id });

            Label label3 = labelService.CreateLabel("Penosa", newComment3.id);

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

            Comment newComment1 = commentService.AddComment("Review 1", "Muy buena bicicleta", biciCarretera.id);
            Comment newComment2 = commentService.AddComment("Review 2", "Buena bicicleta", biciCarretera.id);
            Comment newComment3 = commentService.AddComment("Review 3", "Mejorable", biciCarretera.id);

            Label label = labelService.CreateLabel("Genial", newComment1.id);
            // Label 1 is assigned to comment 2
            labelService.AssignLabelsToComment(newComment2.id, new List<long> { label.id });
            // Label 1 is assigned to comment 3
            labelService.AssignLabelsToComment(newComment3.id, new List<long> { label.id });

            Label label2 = labelService.CreateLabel("Ridicula", newComment2.id);
            // Label 2 is assigned to comment 1
            labelService.AssignLabelsToComment(newComment1.id, new List<long> { label2.id });

            Label label3 = labelService.CreateLabel("Penosa", newComment3.id);

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
