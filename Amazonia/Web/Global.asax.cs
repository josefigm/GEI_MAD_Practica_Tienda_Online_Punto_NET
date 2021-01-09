using Es.Udc.DotNet.Amazonia.Model.ProductServiceImp.Cache;
using Es.Udc.DotNet.Amazonia.Web.HTTP.Util.IoC;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.ModelUtil.Log;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Es.Udc.DotNet.Amazonia.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Application.Lock();

            IIoCManager IoCManager = new IoCManagerNinject();
            IoCManager.Configure();

            Application["managerIoC"] = IoCManager;
            LogManager.RecordMessage("NInject kernel container configured and started", MessageType.Info);

            Application.UnLock();
        }

        //protected void Session_Start(object sender, EventArgs e)
        //{
        //    SessionManager.TouchSession(Context);
        //}

        protected void Application_End(object sender, EventArgs e)
        {
            ((IKernel)Application["kernelIoC"]).Dispose();

            LogManager.RecordMessage("NInject kernel container disposed", MessageType.Info);
        }
    }
}