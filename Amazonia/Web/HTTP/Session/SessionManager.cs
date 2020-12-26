using System;
using System.Web;
using Es.Udc.DotNet.Amazonia.Web.HTTP.View.ApplicationObjects;

namespace Es.Udc.DotNet.Amazonia.Web.HTTP.Session
{
    /// <summary>
    /// Encapsules access to session data
    /// </summary>
    public class SessionManager
    {
        public const String LOCALE_SESSION_ATTRIBUTE = "locale";

        private SessionManager()
        {
        }

        public static void SetLocale(HttpContext context, Locale locale)
        {
            context.Session[LOCALE_SESSION_ATTRIBUTE] = locale;
        }

        public static Locale GetLocale(HttpContext context)
        {
            Locale locale = (Locale)context.Session[LOCALE_SESSION_ATTRIBUTE];

            return (locale);
        }

        public static Boolean IsLocaleDefined(HttpContext context)
        {
            if (context.Session == null)
                return false;
            else
                return (context.Session[LOCALE_SESSION_ATTRIBUTE] != null);
        }
    }
}