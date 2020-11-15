using System;
using System.Text;
using System.Collections.Generic;
using System.Transactions;
using Ninject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Es.Udc.DotNet.Amazonia.Model.ClientServiceImp;
using Es.Udc.DotNet.Amazonia.Model.ClientServiceImp.Util;
using Es.Udc.DotNet.Amazonia.Model.DAOs.ClientDao;

namespace Test.ClientServiceTests
{
    /// <summary>
    /// Descripción resumida de IProductServiceTest
    /// </summary>
    [TestClass]
    public class IProductServiceTest
    {

        // Variables used in several tests are initialized here
        private const string login = "loginTest";
        private const string login2 = "loginTest2";
        private const string clearPassword = "password";
        private const string firstName = "name";
        private const string lastName = "lastName";
        private const string email = "email@testing.net";
        private const string address = "address";
        private const byte role = 1;
        private const byte language = 5;


        private static IKernel kernel;
        private static IClientService clientService;
        private static IClientDao clientDao;

        private TransactionScope transactionScope;

        public TestContext TestContext { get; set; }

        public IProductServiceTest()
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

        [TestMethod]
        public void TestRegisterClient()
        {

            using (var scope = new TransactionScope())
            {




                clientService.RegisterClient(login, clearPassword,
                        new ClientDetails(firstName, lastName, address, email, role, language));

                var clientBd = clientDao.FindByLogin(login);

                // Check data
                Assert.AreEqual(login, clientBd.login);
                Assert.AreEqual(PasswordEncrypter.Crypt(clearPassword), clientBd.password);
                Assert.AreEqual(firstName, clientBd.firstName);
                Assert.AreEqual(lastName, clientBd.lastName);
                Assert.AreEqual(address, clientBd.address);
                Assert.AreEqual(email, clientBd.email);
                Assert.AreEqual(role, clientBd.role);
                Assert.AreEqual(language, clientBd.language);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test for UpdateUserProfileDetails
        /// </summary>
        [TestMethod]
        public void UpdateUserProfileDetailsTest()
        {
            using (var scope = new TransactionScope())
            {

                

                // Register user and update profile details
                clientService.RegisterClient(login2, clearPassword,
                        new ClientDetails(firstName, lastName, address, email, role, language));

                var expected =
                    new ClientDetails(firstName + "X", lastName + "X", address + "X",
                        email + "X", 5, 5);

                clientService.UpdateUserProfileDetails(login2, expected);

                var clientUpdated = clientDao.FindByLogin(login2);

                // Check changes
                Assert.AreEqual(firstName + "X", clientUpdated.firstName);
                Assert.AreEqual(lastName + "X", clientUpdated.lastName);
                Assert.AreEqual(address + "X", clientUpdated.address);
                Assert.AreEqual(email + "X", clientUpdated.email);
                Assert.AreEqual(5, clientUpdated.role);
                Assert.AreEqual(5, clientUpdated.language);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }


        #region Additional test attributes

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();

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
