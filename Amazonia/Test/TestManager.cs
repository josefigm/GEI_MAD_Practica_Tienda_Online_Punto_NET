using Es.Udc.DotNet.Amazonia.Model.DAOs.CardDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CategoryDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.ClientDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.ProductDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.SaleDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.SaleLineDao;
using Es.Udc.DotNet.Amazonia.Model.ProductServiceImp;
using Es.Udc.DotNet.Amazonia.Model.SaleServiceImp;
using Ninject;
using System.Configuration;
using System.Data.Entity;

namespace Test
{
    public class TestManager
    {
        /// <summary>
        /// Configures and populates the Ninject kernel
        /// </summary>
        /// <returns>The NInject kernel</returns>
        public static IKernel ConfigureNInjectKernel()
        {
            NinjectSettings settings = new NinjectSettings() { LoadExtensions = true };

            IKernel kernel = new StandardKernel(settings);

            kernel.Bind<ICategoryDao>().
                To<CategoryDaoEntityFramework>();

            kernel.Bind<IClientDao>().
                To<ClientDaoEntityFramework>();

            kernel.Bind<ICardDao>().
                To<CardDaoEntityFramework>();

            kernel.Bind<ISaleDao>().
                To<SaleDaoEntityFramework>();

            kernel.Bind<ISaleLineDao>().
                To<SaleLineDaoEntityFramework>();

            kernel.Bind<IProductDao>().
                To<ProductDaoEntityFramework>();

            kernel.Bind<IProductService>().
                To<ProductServiceImp>();

            kernel.Bind<ISaleService>().
                To<SaleServiceImp>();

            string connectionString =
                ConfigurationManager.ConnectionStrings["amazoniaEntities"].ConnectionString;

            kernel.Bind<DbContext>().
                ToSelf().
                InSingletonScope().
                WithConstructorArgument("nameOrConnectionString", connectionString);

            return kernel;
        }

        /// <summary>
        /// Configures the Ninject kernel from an external module file.
        /// </summary>
        /// <param name="moduleFilename">The module filename.</param>
        /// <returns>The NInject kernel</returns>
        public static IKernel ConfigureNInjectKernel(string moduleFilename)
        {
            NinjectSettings settings = new NinjectSettings() { LoadExtensions = true };
            IKernel kernel = new StandardKernel(settings);

            kernel.Load(moduleFilename);

            return kernel;
        }

        public static void ClearNInjectKernel(IKernel kernel)
        {
            kernel.Dispose();
        }
    }
}
