using System;
using System.Text;
using System.Collections.Generic;
using System.Transactions;
using Ninject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Es.Udc.DotNet.Amazonia.Model.ClientServiceImp;
using Es.Udc.DotNet.Amazonia.Model.ClientServiceImp.Util;
using Es.Udc.DotNet.Amazonia.Model.DAOs.ClientDao;
using Es.Udc.DotNet.Amazonia.Model.ClientServiceImp.Exceptions;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CardDao;
using Es.Udc.DotNet.Amazonia.Model.CardServiceImp;
using Es.Udc.DotNet.Amazonia.Model;

namespace Test.CardServiceTests
{
    /// <summary>
    /// Descripción resumida de IProductServiceTest
    /// </summary>
    [TestClass]
    public class ICardServiceTest
    {

        // Variables used in several tests are initialized here
        private const string login = "aloginTest";
        private const string login2 = "aloginTest2";
        private const string login3 = "aloginTest3";
        private const string login4 = "aloginTest4";
        private const string login5 = "aloginTest5";
        private const string clearPassword = "password";
        private const string firstName = "name";
        private const string lastName = "lastName";
        private const string email = "email@testing.net";
        private const string address = "address";
        private const byte role = 1;
        private const byte language = 5;


        private static IKernel kernel;
        private static ICardService cardService;
        private static ICardDao cardDao;
        private static IClientService clientService;
        private static IClientDao clientDao;

        private TransactionScope transactionScope;

        public TestContext TestContext { get; set; }

        public ICardServiceTest()
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


        /// <summary>
        /// Añadir tarjeta a un usuario test
        /// </summary>
        [TestMethod]
        public void TestAddCardToClient()
        {

            using (var scope = new TransactionScope())
            {

                // Creamos cliente
                clientService.RegisterClient(login, clearPassword,
                        new ClientDetails(firstName, lastName, address, email, role, language));

                Client client = clientDao.FindByLogin(login);

                // Creamos tarjeta
                Card card = new Card();
                card.number = "1111222233334444";
                card.cvv = "123";
                card.expireDate = new DateTime(2025, 1, 1);
                card.name = "Client Name";
                card.type = true;
                cardDao.Create(card);

                // Llamamos al servicio asociando la tarjeta al cliente
                cardService.CreateCardToClient(card, login);

                // Listamos tarjetas del cliente
                List<Card> listaCards = clientService.ListCardsByClientLogin(login);

                Boolean tarjetaEncontrada = listaCards.Contains(card);

                Assert.AreEqual(true, tarjetaEncontrada);

            }
        }


        #region Additional test attributes

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();

            cardDao = kernel.Get<ICardDao>();
            cardService = kernel.Get<ICardService>();
            clientDao = kernel.Get<IClientDao>();
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
