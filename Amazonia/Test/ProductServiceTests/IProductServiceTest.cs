﻿using Es.Udc.DotNet.Amazonia.Model;
using Es.Udc.DotNet.Amazonia.Model.ClientServiceImp;
using Es.Udc.DotNet.Amazonia.Model.CommentServiceImp;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CategoryDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.ProductDao;
using Es.Udc.DotNet.Amazonia.Model.LabelServiceImp;
using Es.Udc.DotNet.Amazonia.Model.LabelServiceImp.DTOs;
using Es.Udc.DotNet.Amazonia.Model.ProductServiceImp;
using Es.Udc.DotNet.Amazonia.Model.ProductServiceImp.DTOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Transactions;

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

        public IProductServiceTest()
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
        public void TestFindCategories()
        {
            using (var scope = new TransactionScope())
            {
                Category c1 = new Category();
                c1.name = "c1";
                Category c2 = new Category();
                c2.name = "c2";

                categoryDao.Create(c1);
                categoryDao.Create(c2);

                List<Category> categoriesexpected = new List<Category>(2);

                categoriesexpected.Add(c1);
                categoriesexpected.Add(c2);

                List<Category> categoriesfound = productService.FindCategories();

                Assert.AreEqual(2, categoriesfound.Count);
                CollectionAssert.AreEqual(categoriesexpected, categoriesfound);
            }
        }

        [TestMethod]
        public void TestUpdateProduct()
        {
            using (var scope = new TransactionScope())
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

                #endregion Declaracion de variables

                #region Persistencia

                productDao.Create(biciCarretera);

                #endregion Persistencia

                //Se cambia biciCarretera y se comprueba que al actualizarla en BBDD son iguales.

                biciCarretera.price = 1500d;

                productService.UpdateProduct(biciCarretera.id, biciCarretera.name, biciCarretera.price, biciCarretera.stock, biciCarretera.description);

                Product retrievedProduct = productDao.Find(biciCarretera.id);
                Assert.AreEqual(biciCarretera, retrievedProduct);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FindByKeywordBadInputTest()
        {
            using (var scope = new TransactionScope())
            {
                List<ProductDTO> listaRecuperada = productService.FindProductByWord("", 0, 10).products;
            }

        }


        [TestMethod]
        [ExpectedException(typeof(Es.Udc.DotNet.ModelUtil.Exceptions.InstanceNotFoundException))]
        public void FindByKeywordCategoryBadInputTest()
        {
            using (var scope = new TransactionScope())
            {
                // Id categoria inexistente...

                List<ProductDTO> listaRecuperada = productService.FindProductByWordAndCategory("Cat", -1L, 0, 10).products;
            }

        }


        [TestMethod]
        public void TestFindByKeywordEmpty()
        {
            using (var scope = new TransactionScope())
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

                productDao.Create(biciCarretera);
                productDao.Create(portatil);
                productDao.Create(biciMontaña);

                #endregion Persistencia

                List<ProductDTO> listaRecuperada = productService.FindProductByWord("Cacahuete", 0, 10).products;

                Assert.IsTrue(listaRecuperada.Count == 0);
            }
        }

        [TestMethod]
        public void TestFindByKeywordOneResultAndTestKeywordTrimAndNoCaseSensitive()
        {
            using (var scope = new TransactionScope())
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
                Computer portatil = new Computer();

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
                portatil.cpuPower = "aaa";
                portatil.memoryRAM = "aaaa";
                portatil.memoryROM = "aaa";
                portatil.model = 1;

                #endregion Declaracion de variables

                #region Persistencia

                productDao.Create(biciCarretera);
                productDao.Create(portatil);
                productDao.Create(biciMontaña);

                #endregion Persistencia

                List<ProductDTO> listaEsperadaOrdenador = new List<ProductDTO>(1);
                listaEsperadaOrdenador.Add(ProductMapper.ProductToProductDto(portatil));
                List<ProductDTO> listaRecuperadaOrdenador = productService.FindProductByWordAndCategory("     ordenaDoReS    ", c2.id, 0, 10).products;
                // Buscando por ordenadores no se debaría encontrar nada.

                Assert.IsTrue(listaRecuperadaOrdenador.Count == 0);

                // Buscando por portátil sí que debería encontrarlo
                listaRecuperadaOrdenador = productService.FindProductByWordAndCategory("     portATIL    ", c2.id, 0, 10).products;

                Assert.IsTrue(listaRecuperadaOrdenador.Count == 1);
                CollectionAssert.AreEqual(listaEsperadaOrdenador, listaRecuperadaOrdenador);

                List<ProductDTO> listaRecuperadaBicicletas = productService.FindProductByWordAndCategory("bicicleta", c1.id, 0, 10).products;

                Assert.IsTrue(listaRecuperadaBicicletas.Count == 2);
            }
        }

        [TestMethod]
        public void TestFindByKeywordMultipleResultsAndTestKeywordTrimAndNoCaseSensitive()
        {
            using (var scope = new TransactionScope())
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

                productDao.Create(biciCarretera);
                productDao.Create(portatil);
                productDao.Create(biciMontaña);

                #endregion Persistencia

                List<ProductDTO> listaEsperadaBicicletas = new List<ProductDTO>(2);
                listaEsperadaBicicletas.Add(ProductMapper.ProductToProductDto(biciCarretera));
                listaEsperadaBicicletas.Add(ProductMapper.ProductToProductDto(biciMontaña));

                List<ProductDTO> listaRecuperadaBicicletas = productService.FindProductByWord("bicicleta", 0, 10).products;

                Assert.IsTrue(listaRecuperadaBicicletas.Count == 2);
                CollectionAssert.AreEqual(listaEsperadaBicicletas, listaRecuperadaBicicletas);
            }
        }

        [TestMethod]
        public void TestFindByKeywordAndCategoryMultipleResultsAndTestKeywordTrimAndNoCaseSensitive()
        {
            using (var scope = new TransactionScope())
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
                biciMontaña.categoryId = categoryIdBicicletaOutlet;

                #endregion Declaracion de variables

                #region Persistencia

                productDao.Create(biciCarretera);
                productDao.Create(biciMontaña);

                #endregion Persistencia

                List<ProductDTO> listaEsperadaBicicletas = new List<ProductDTO>(1);
                listaEsperadaBicicletas.Add(ProductMapper.ProductToProductDto(biciCarretera));

                List<ProductDTO> listaRecuperadaBicicletas = productService.FindProductByWordAndCategory("   bicicLeta  ", c1.id, 0, 10).products;

                Assert.IsTrue(listaRecuperadaBicicletas.Count == 1);
                CollectionAssert.AreEqual(listaEsperadaBicicletas, listaRecuperadaBicicletas);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Es.Udc.DotNet.ModelUtil.Exceptions.InstanceNotFoundException))]
        public void RetrieveProductsWithLabelEmptyTest()
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

                #endregion Needed variables

                #region Persistencia

                productDao.Create(biciCarretera);

                #endregion Persistencia

                #region Comment and label section
                Client cliente = registerUser(LOGIN);

                long newCommentId = commentService.AddComment("Review 1", "Muy buena bicicleta", biciCarretera.id, cliente.id);

                labelService.CreateLabel("Genial");

                #endregion Comment and label section

                List<ProductDTO> productsWithLabel = productService.RetrieveProductsWithLabel(0, 5, -1L).products;

                Assert.AreEqual(productsWithLabel.Count, 0);
            }
        }

        [TestMethod]
        public void RetrieveProductWithLabelTest()
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

                #endregion Needed variables

                #region Persistencia

                productDao.Create(biciCarretera);

                #endregion Persistencia

                #region Comment and label section
                Client cliente = registerUser(LOGIN2);
                long newCommentId = commentService.AddComment("Review 1", "Muy buena bicicleta", biciCarretera.id, cliente.id);
                LabelDTO label = labelService.CreateLabel("Genial");
                labelService.AssignLabelsToComment(newCommentId, new List<long> { label.id });

                #endregion Comment and label section

                List<ProductDTO> productsWithLabel = productService.RetrieveProductsWithLabel(0, 5, label.id).products;

                Assert.AreEqual(productsWithLabel.Count, 1);
                Assert.AreEqual(productsWithLabel[0], ProductMapper.ProductToProductDto(biciCarretera));
            }
        }

        [TestMethod]
        public void RetrieveProductsWithLabelTest()
        {
            using (var scope = new TransactionScope())
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

                #endregion Needed variables

                #region Persistencia

                productDao.Create(biciCarretera);
                productDao.Create(portatil);
                productDao.Create(biciMontaña);

                #endregion Persistencia

                #region Comment and label section
                Client cliente = registerUser(LOGIN);
                Client cliente2 = registerUser(LOGIN2);
                Client cliente3 = registerUser(LOGIN3);

                long newCommentId = commentService.AddComment("Review 1", "Muy buena bicicleta", biciCarretera.id, cliente.id);
                long newComment2Id = commentService.AddComment("Review 1", "Muy mala bicicleta", biciMontaña.id, cliente2.id);
                long newComment3Id = commentService.AddComment("Review 1", "Muy buen portátil", portatil.id, cliente3.id);

                LabelDTO label = labelService.CreateLabel("Genial");
                labelService.AssignLabelsToComment(newCommentId, new List<long> { label.id });

                LabelDTO label2 = labelService.CreateLabel("Mala");
                labelService.AssignLabelsToComment(newComment2Id, new List<long> { label.id });

                LabelDTO label3 = labelService.CreateLabel("Fenomenal");
                labelService.AssignLabelsToComment(newComment3Id, new List<long> { label.id });

                #endregion Comment and label section

                List<ProductDTO> productsWithLabel = productService.RetrieveProductsWithLabel(0, 5, label.id).products;

                Assert.AreEqual(productsWithLabel.Count, 3);
                Assert.IsTrue(productsWithLabel.Contains(ProductMapper.ProductToProductDto(biciCarretera)));
            }
        }

        [TestMethod]
        public void RetrieveProductWithMultipleLabelsTest()
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

                #endregion Needed variables

                #region Persistencia

                productDao.Create(biciCarretera);

                #endregion Persistencia

                #region Comment and label section
                Client cliente = registerUser(LOGIN);

                long newCommentId = commentService.AddComment("Review 1", "Muy buena bicicleta", biciCarretera.id, cliente.id);
                LabelDTO label = labelService.CreateLabel("Genial");
                LabelDTO label2 = labelService.CreateLabel("Buena");
                labelService.AssignLabelsToComment(newCommentId, new List<long> { label.id, label2.id });
                

                #endregion Comment and label section

                // Se busca primero por la primera etiqueta
                List<ProductDTO> productsWithLabel = productService.RetrieveProductsWithLabel(0, 5, label.id).products;

                Assert.AreEqual(productsWithLabel.Count, 1);
                Assert.AreEqual(productsWithLabel[0], ProductMapper.ProductToProductDto(biciCarretera));

                //Se busca luego por la segunda etiqueta
                productsWithLabel = productService.RetrieveProductsWithLabel(0, 5, label2.id).products;

                Assert.AreEqual(productsWithLabel.Count, 1);
                Assert.AreEqual(productsWithLabel[0], ProductMapper.ProductToProductDto(biciCarretera));
            }
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
            commentService = kernel.Get<ICommentService>();
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