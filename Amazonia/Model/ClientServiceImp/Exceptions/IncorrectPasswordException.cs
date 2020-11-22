using System;
using Es.Udc.DotNet.ModelUtil.Log;

namespace Es.Udc.DotNet.Amazonia.Model.ClientServiceImp.Exceptions
{

    /// <summary>
    /// Public <c>ModelException</c> which captures the error 
    /// with the passwords of the clients.
    /// </summary>
    [Serializable]
    public class IncorrectPasswordException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="IncorrectPasswordException"/> class.
        /// </summary>
        /// <param name="login"><c>login</c> that causes the error.</param>
        public IncorrectPasswordException(String login)
            : base("Incorrect password exception => login = " + login)
        {
            this.Login = login;
        }

        /// <summary>
        /// Stores the User login name of the exception
        /// </summary>
        /// <value>The name of the login.</value>
        public String Login { get; private set; }

        #region Test Code Region. Uncomment for testing.

        //public static void Main(String[] args)
        //{

        //    try
        //    {

        //        throw new IncorrectPasswordException("jsmith");

        //    }
        //    catch (Exception e)
        //    {

        //        LogManager.RecordMessage("Message: " + e.Message +
        //            "  Stack Trace: " + e.StackTrace, MessageType.Info);

        //        Console.ReadLine();

        //    }
        //}

        #endregion
    }
}
