[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(BookStore.App.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(BookStore.App.App_Start.NinjectWebCommon), "Stop")]

namespace BookStore.App.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Services.Interfaces;
    using Services;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IAdminService>().To<AdminService>();
            kernel.Bind<IAuthorService>().To<AuthorService>();
            kernel.Bind<IBasketService>().To<BasketService>();
            kernel.Bind<IBookService>().To<BookService>();
            kernel.Bind<ICategoryService>().To<CategoryService>();
            kernel.Bind<IHomeService>().To<HomeService>();
            kernel.Bind<IPromotionService>().To<PromotionService>();
            kernel.Bind<IPurchaseService>().To<PurchaseService>();
            kernel.Bind<IRatingService>().To<RatingService>();
            kernel.Bind<IReviewService>().To<ReviewService>();
            kernel.Bind<IUserService>().To<UserService>();
        }        
    }
}
