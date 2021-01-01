using Es.Udc.DotNet.Amazonia.Model.CardServiceImp;
using Es.Udc.DotNet.Amazonia.Model.ClientServiceImp;
using Es.Udc.DotNet.Amazonia.Model.CommentServiceImp;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CardDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CategoryDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.ClientDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CommentDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.LabelDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.ProductDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.SaleDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.SaleLineDao;
using Es.Udc.DotNet.Amazonia.Model.LabelServiceImp;
using Es.Udc.DotNet.Amazonia.Model.ProductServiceImp;
using Es.Udc.DotNet.Amazonia.Model.SaleServiceImp;
using Es.Udc.DotNet.ModelUtil.IoC;
using Ninject;
using System.Configuration;
using System.Data.Entity;

namespace Es.Udc.DotNet.Amazonia.Web.HTTP.Util.IoC
{
    internal class IoCManagerNinject : IIoCManager
    {
        private static IKernel kernel;
        private static NinjectSettings settings;

        public void Configure()
        {
            settings = new NinjectSettings() { LoadExtensions = true };
            kernel = new StandardKernel(settings);

            kernel.Bind<IProductService>().
                To<ProductServiceImp>();

            kernel.Bind<IClientService>().
                To<ClientServiceImp>();

            kernel.Bind<ICardService>().
                To<CardServiceImp>();

            kernel.Bind<ISaleService>().
                To<SaleServiceImp>();

            kernel.Bind<ICommentService>().
                To<CommentServiceImp>();

            kernel.Bind<ILabelService>().
                To<LabelServiceImp>();

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

            kernel.Bind<ICommentDao>().
                To<CommentDaoEntityFramework>();
                
            kernel.Bind<ILabelDao>().
                To<LabelDaoEntityFramework>();

            /*** DbContext ***/
            string connectionString = ConfigurationManager.ConnectionStrings["amazoniaEntities"].ConnectionString;

            kernel.Bind<DbContext>().ToSelf().InSingletonScope().WithConstructorArgument("nameOrConnectionString", connectionString);
        }

        public T Resolve<T>()
        {
            return kernel.Get<T>();
        }

    }
}