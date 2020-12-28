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
using Es.Udc.DotNet.Amazonia.Model.CardServiceImp;
using Es.Udc.DotNet.Amazonia.Model;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CardDao;

namespace Test.ClientServiceTests
{
    /// <summary>
    /// Conjunto de tests de las operaciones de ClientService
    /// </summary>
    [TestClass]
    public class IClientServiceTest
    {

        // Variables used in several tests are initialized here
        private const string LOGIN = "LOGINTestprueba";
        private const string CLEARPASSWORD = "password";
        private const string FIRSTNAME = "name";
        private const string LASTNAME = "LASTNAME";
        private const string EMAIL = "EMAIL@testing.net";
        private const string ADDRESS = "ADDRESS";
        private const byte ROLE = 1;
        private const string LANGUAGE = "en";
        private const string COUNTRY = "en";
        private const string CARDNUMBER = "1111111111111111";


        private static IKernel kernel;
        private static IClientService clientService;
        private static ICardService cardService;
        private static IClientDao clientDao;
        private static ICardDao cardDao;

        private TransactionScope transactionScope;

        public TestContext TestContext { get; set; }

        public IClientServiceTest()
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
        /// A test for RegisterClient
        /// </summary>
        [TestMethod]
        public void TestRegisterClient()
        {

            using (var scope = new TransactionScope())
            {

                Client clientBd = clientService.RegisterClient(LOGIN, CLEARPASSWORD,
                        new ClientDTO(FIRSTNAME, LASTNAME, ADDRESS, EMAIL, ROLE, LANGUAGE, COUNTRY));

                // Check data
                Assert.AreEqual(LOGIN, clientBd.login);
                Assert.AreEqual(PasswordEncrypter.Crypt(CLEARPASSWORD), clientBd.password);
                Assert.AreEqual(FIRSTNAME, clientBd.firstName);
                Assert.AreEqual(LASTNAME, clientBd.lastName);
                Assert.AreEqual(ADDRESS, clientBd.address);
                Assert.AreEqual(EMAIL, clientBd.email);
                Assert.AreEqual(ROLE, clientBd.role);
                Assert.AreEqual(LANGUAGE, clientBd.language);
                Assert.AreEqual(COUNTRY, clientBd.country);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test for UpdateUserProfileDetails
        /// </summary>
        [TestMethod]
        public void TestUpdateUserProfileDetails()
        {
            using (var scope = new TransactionScope())
            {

                // Register user and update profile details
                Client client = clientService.RegisterClient(LOGIN, CLEARPASSWORD,
                        new ClientDTO(FIRSTNAME, LASTNAME, ADDRESS, EMAIL, ROLE, LANGUAGE, COUNTRY));

                var update =
                    new ClientDTO(FIRSTNAME + "X", LASTNAME + "X", ADDRESS + "X",
                        EMAIL + "X", 5, "es", "mx");

                clientService.UpdateUserProfileDetails(client.id, update);

                var clientUpdated = clientDao.FindByLogin(LOGIN);

                // Check changes
                Assert.AreEqual(FIRSTNAME + "X", clientUpdated.firstName);
                Assert.AreEqual(LASTNAME + "X", clientUpdated.lastName);
                Assert.AreEqual(ADDRESS + "X", clientUpdated.address);
                Assert.AreEqual(EMAIL + "X", clientUpdated.email);
                Assert.AreEqual(5, clientUpdated.role);
                Assert.AreEqual("es", clientUpdated.language);
                Assert.AreEqual("mx", clientUpdated.country);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test for LOGIN with clear password
        /// </summary>
        [TestMethod]
        public void LOGINCLEARPASSWORDTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user
                Client client = clientService.RegisterClient(LOGIN, CLEARPASSWORD,
                        new ClientDTO(FIRSTNAME, LASTNAME, ADDRESS, EMAIL, ROLE, LANGUAGE, COUNTRY));

                // Create expected LOGINDetails
                var expected = new LoginDTO(client.id, LOGIN, FIRSTNAME,
                    PasswordEncrypter.Crypt(CLEARPASSWORD), ROLE, ADDRESS, LANGUAGE, COUNTRY);

                // LOGIN with clear password
                var realLOGINService =
                    clientService.Login(LOGIN, CLEARPASSWORD, false);

                // Check data
                Assert.AreEqual(expected, realLOGINService);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test for LOGIN with encrypted password
        /// </summary>
        [TestMethod]
        public void LOGINEncryptedPasswordTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user
                Client client = clientService.RegisterClient(LOGIN, CLEARPASSWORD,
                        new ClientDTO(FIRSTNAME, LASTNAME, ADDRESS, EMAIL, ROLE, LANGUAGE, COUNTRY));

                // Create expected LOGINDetails
                var expected = new LoginDTO(client.id, LOGIN, FIRSTNAME,
                    PasswordEncrypter.Crypt(CLEARPASSWORD), ROLE, ADDRESS, LANGUAGE, COUNTRY);

                // LOGIN with encrypted password
                var real =
                    clientService.Login(LOGIN,
                        PasswordEncrypter.Crypt(CLEARPASSWORD), true);

                // Check data
                Assert.AreEqual(expected, real);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test for LOGIN with incorrect password
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(IncorrectPasswordException))]
        public void LOGINIncorrectPasswordTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user
                clientService.RegisterClient(LOGIN, CLEARPASSWORD,
                        new ClientDTO(FIRSTNAME, LASTNAME, ADDRESS, EMAIL, ROLE, LANGUAGE, COUNTRY));

                // LOGIN with incorrect (clear) password
                var real =
                    clientService.Login(LOGIN, CLEARPASSWORD + "jaja", false);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// Añadir tarjeta a un usuario test
        /// </summary>
        [TestMethod]
        public void TestListCardsOfClient()
        {

            using (var scope = new TransactionScope())
            {

                // Creamos cliente
                clientService.RegisterClient(LOGIN, CLEARPASSWORD,
                        new ClientDTO(FIRSTNAME, LASTNAME, ADDRESS, EMAIL, ROLE, LANGUAGE, COUNTRY));

                Client client = clientDao.FindByLogin(LOGIN);

                // Creamos CardForm
                CardDTO cardForm =
                    new CardDTO(CARDNUMBER, "123",
                        new DateTime(2025, 1, 1), false, false);

                // Llamamos al servicio asociando la tarjeta al cliente
                cardService.CreateCardToClient(cardForm, client.login);

                // Listamos tarjetas del cliente
                List<Card> listaCards = clientService.ListCardsByClientLogin(LOGIN);

                // Boolean -> está card creada en la lista de tarjetas del client
                Boolean tarjetaEncontrada = listaCards.Contains(cardDao.FindByNumber(CARDNUMBER));

                // Comprobar que sí está
                Assert.AreEqual(true, tarjetaEncontrada);
            }
        }


        /// <summary>
        /// A test for set a default card for a user
        /// </summary>
        [TestMethod]
        public void SetDefaultCardTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user
                Client client = clientService.RegisterClient(LOGIN, CLEARPASSWORD,
                        new ClientDTO(FIRSTNAME, LASTNAME, ADDRESS, EMAIL, ROLE, LANGUAGE, COUNTRY));

                // Creamos CardForm
                CardDTO cardForm = 
                    new CardDTO(CARDNUMBER, "123", 
                        new DateTime(2025, 1, 1), false);

                // Creamos tarjeta y la asignamos a usuario
                cardService.CreateCardToClient(cardForm, LOGIN);

                // Establecemos por defecto
                clientService.SetDefaultCard(cardForm.Number);

                // Recuperamos tarjeta
                Card cardBD = cardDao.FindByNumber(cardForm.Number);

                // Comprobamos que la tarjeta esté por defecto
                Assert.AreEqual(true, cardBD.defaultCard);

            }
        }

        /// <summary>
        /// A test for set a default card for a user
        /// </summary>
        [TestMethod]
        public void InitializeDefaultCardTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user
                Client client = clientService.RegisterClient(LOGIN, CLEARPASSWORD,
                        new ClientDTO(FIRSTNAME, LASTNAME, ADDRESS, EMAIL, ROLE, LANGUAGE, COUNTRY));

                // Creamos CardForm
                CardDTO cardForm =
                    new CardDTO(CARDNUMBER, "123",
                        new DateTime(2025, 1, 1), false, true);

                // Creamos tarjeta y la asignamos a usuario
                cardService.CreateCardToClient(cardForm, LOGIN);

                // Recuperamos tarjeta
                Card cardBD = cardDao.FindByNumber(cardForm.Number);

                // Comprobamos que la tarjeta esté por defecto
                Assert.AreEqual(true, cardBD.defaultCard);

            }
        }

        /// <summary>
        /// A test for get a default card for a user
        /// </summary>
        [TestMethod]
        public void GetDefaultCardTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user
                Client client = clientService.RegisterClient(LOGIN, CLEARPASSWORD,
                        new ClientDTO(FIRSTNAME, LASTNAME, ADDRESS, EMAIL, ROLE, LANGUAGE, COUNTRY));

                // Creamos CardForm
                CardDTO cardForm =
                    new CardDTO(CARDNUMBER, "123",
                        new DateTime(2025, 1, 1), false);

                // Creamos tarjeta y la asignamos a usuario
                cardService.CreateCardToClient(cardForm, LOGIN);

                // Establecemos por defecto
                clientService.SetDefaultCard(cardForm.Number);

                // Recuperamos tarjeta por defecto
                Card cardBD = clientService.GetDefaultCard(LOGIN);

                // Comprobamos que la tarjeta esté por defecto
                Assert.AreEqual(true, cardBD.defaultCard);

            }
        }


        #region Additional test attributes

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();

            clientDao = kernel.Get<IClientDao>();
            cardDao = kernel.Get<ICardDao>();
            clientService = kernel.Get<IClientService>();
            cardService = kernel.Get<ICardService>();
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
