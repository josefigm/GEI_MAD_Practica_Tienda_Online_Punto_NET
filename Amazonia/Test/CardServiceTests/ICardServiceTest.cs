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
using Es.Udc.DotNet.Amazonia.Model.CardServiceImp.DTOs;
using Es.Udc.DotNet.Amazonia.Model;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Test.CardServiceTests
{
    /// <summary>
    /// Descripción resumida de IProductServiceTest
    /// </summary>
    [TestClass]
    public class ICardServiceTest
    {

        // Variables used in several tests are initialized here
        private const string LOGIN = "aloginTest";
        private const string CLEARPASSWORD= "password";
        private const string FIRSTNAME = "name";
        private const string LASTNAME = "lastName";
        private const string EMAIL = "email@testing.net";
        private const string ADDRESS = "address";
        private const byte ROLE = 0;
        private const string LANGUAGE = "es";
        private const string COUNTRY = "es";

        private const string NUMBER = "1111222233334441";
        private const string OTHER_NUMBER = "1111222233334442";
        private const string REPEAT_NUMBER = "1111222239334442";


        private const string CVV = "123";
        private DateTime EXPIREDATE = new DateTime(2025, 1, 1);
        private const bool TYPE = true;
        private const bool DEFAULTCARD = false;

        private const string CVV2 = "987";
        private DateTime EXPIREDATE2 = new DateTime(2029, 1, 1);
        private const bool TYPE2 = false;



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

        public static bool SameCard(Card obj)
        {
            return obj.number==NUMBER && obj.cvv==CVV;
        }

        Predicate<Card> predicate = SameCard;

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


        // <summary>
        // Añadir tarjeta a un usuario test
        // </summary>
        [TestMethod]
        public void TestAddCardToClient()
        {

            using (var scope = new TransactionScope())
            {

                // Creamos DTO tarjeta
                CardDTO cardDTO = new CardDTO(NUMBER, CVV, EXPIREDATE, TYPE, DEFAULTCARD);

                // Creamos cliente
                Client client = clientService.RegisterClient(LOGIN, CLEARPASSWORD,
                        new ClientDTO(FIRSTNAME, LASTNAME, ADDRESS, EMAIL, ROLE, LANGUAGE, COUNTRY));

                // Llamamos al servicio asociando la tarjeta al cliente
                Card card = cardService.CreateCardToClient(cardDTO, client.id);

                // Listamos tarjetas del cliente
                List<Card> listaCards = clientDao.FindCardsOfClient(client);

                // Comprobamos números de tarjetas
                int numTarjetas = listaCards.Count;
                Assert.AreEqual(1, numTarjetas);

                // Comprobamos que Client.Cards contenga la card creada
                Card firstSameCard = listaCards.Find(predicate);
                Assert.AreEqual(card, firstSameCard);

                // Comprobamos que card tenga asignado client
                Card cardBd = cardDao.Find(card.id);
                Assert.AreEqual(client.id, cardBd.clientId);
                Assert.AreEqual(client, cardBd.Client);

                // Comprobamos que al ser la primera, será por defecto
                Assert.AreEqual(true, cardBd.defaultCard);

                // Creamos nueva tarjeta y la asociamos al cliente
                CardDTO cardDTO2 = new CardDTO(OTHER_NUMBER, CVV, EXPIREDATE, TYPE, true);
                Card card2 = cardService.CreateCardToClient(cardDTO2, client.id);

                // Comprobamos que al crearla no se definiera por defecto y no afectara a la original
                Assert.AreEqual(false, card2.defaultCard);
                Assert.AreEqual(true, card.defaultCard);

            }
        }

        // <summary>
        // Añadir tarjeta a un usuario con cardNumber repetido
        // </summary>
        [TestMethod]
        [ExpectedException(typeof(DuplicateInstanceException))]
        public void TestAddCardWhitSameNumberToClient()
        {

            using (var scope = new TransactionScope())
            {

                // Creamos DTO tarjeta
                CardDTO cardDTO = new CardDTO(REPEAT_NUMBER, CVV, EXPIREDATE, TYPE, DEFAULTCARD);

                // Creamos cliente
                Client client = clientService.RegisterClient(LOGIN, CLEARPASSWORD,
                        new ClientDTO(FIRSTNAME, LASTNAME, ADDRESS, EMAIL, ROLE, LANGUAGE, COUNTRY));

                // Llamamos al servicio asociando la tarjeta al cliente
                Card card = cardService.CreateCardToClient(cardDTO, client.id);

                // Creamos DTO tarjeta con el mismo número
                CardDTO cardDTO2 = new CardDTO(REPEAT_NUMBER, CVV, EXPIREDATE, TYPE, DEFAULTCARD);

                // La asignamos al cliente y nos salta la exepción "DuplicateInstanceException"
                Card card2 = cardService.CreateCardToClient(cardDTO2, client.id); 

            }
        }

        /// <summary>
        /// A test for UpdateUserProfileDetails
        /// </summary>
        [TestMethod]
        public void TestUpdateCardDetails()
        {
            using (var scope = new TransactionScope())
            {

                // Creamos DTO tarjeta
                CardDTO cardDTO = new CardDTO(NUMBER, CVV, EXPIREDATE, TYPE, DEFAULTCARD);

                // Creamos cliente
                Client client = clientService.RegisterClient(LOGIN, CLEARPASSWORD,
                        new ClientDTO(FIRSTNAME, LASTNAME, ADDRESS, EMAIL, ROLE, LANGUAGE, COUNTRY));

                // Llamamos al servicio asociando la tarjeta al cliente
                Card card = cardService.CreateCardToClient(cardDTO, client.id);

                // Creamos nuevo dto a actualizar (cambiando lo que no debería actualizar)
                CardDTO newCardDTO = new CardDTO(NUMBER, CVV2, EXPIREDATE2, TYPE2, false);

                // Actualizamos tarjeta
                cardService.UpdateCardDetails(newCardDTO);

                Card cardBD = cardDao.Find(card.id);

                Assert.AreEqual(NUMBER, cardBD.number);
                Assert.AreEqual(CVV2, cardBD.cvv);
                Assert.AreEqual(EXPIREDATE2, cardBD.expireDate);
                Assert.AreEqual(TYPE2, cardBD.type);
                // Es true porque es la única
                Assert.AreEqual(true, cardBD.defaultCard);


                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test for get CardDTO of Debit Card (bool Type a true)
        /// </summary>
        [TestMethod]
        public void GetDebitCardDTOTest()
        {
            using (var scope = new TransactionScope())
            {

                // Creamos DTO tarjeta
                CardDTO refCardDTO = new CardDTO(NUMBER, CVV, EXPIREDATE, TYPE, DEFAULTCARD);

                // Creamos cliente
                Client client = clientService.RegisterClient(LOGIN, CLEARPASSWORD,
                        new ClientDTO(FIRSTNAME, LASTNAME, ADDRESS, EMAIL, ROLE, LANGUAGE, COUNTRY));

                // Llamamos al servicio asociando la tarjeta al cliente
                Card card = cardService.CreateCardToClient(refCardDTO, client.id);

                // Recuperamos el DTO
                CardDTO realCardDTO = cardService.GetCardDTO(card.id);

                Assert.AreEqual(NUMBER, realCardDTO.Number);
                Assert.AreEqual(CVV, realCardDTO.CVV);
                Assert.AreEqual(EXPIREDATE, realCardDTO.ExpireDate);
                Assert.AreEqual("Debit Card", realCardDTO.Type);
                // Es true porque es la única
                Assert.AreEqual(true, realCardDTO.DefaultCard);


                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test for get CardDTO of Debit Card (bool Type a true)
        /// </summary>
        [TestMethod]
        public void GetCreditCardDTOTest()
        {
            using (var scope = new TransactionScope())
            {

                // Creamos DTO tarjeta
                CardDTO refCardDTO = new CardDTO(NUMBER, CVV, EXPIREDATE, false, DEFAULTCARD);

                // Creamos cliente
                Client client = clientService.RegisterClient(LOGIN, CLEARPASSWORD,
                        new ClientDTO(FIRSTNAME, LASTNAME, ADDRESS, EMAIL, ROLE, LANGUAGE, COUNTRY));

                // Llamamos al servicio asociando la tarjeta al cliente
                Card card = cardService.CreateCardToClient(refCardDTO, client.id);

                // Recuperamos el DTO
                CardDTO realCardDTO = cardService.GetCardDTO(card.id);

                Assert.AreEqual(NUMBER, realCardDTO.Number);
                Assert.AreEqual(CVV, realCardDTO.CVV);
                Assert.AreEqual(EXPIREDATE, realCardDTO.ExpireDate);
                Assert.AreEqual("Credit Card", realCardDTO.Type);
                // Es true porque es la única
                Assert.AreEqual(true, realCardDTO.DefaultCard);


                // transaction.Complete() is not called, so Rollback is executed.
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
