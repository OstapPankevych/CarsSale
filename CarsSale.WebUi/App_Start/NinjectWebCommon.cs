using System.Web.Mvc;
using CarsSale.DataAccess.Providers.Content;
using CarsSale.DataAccess.Repositories.Interfaces;
using CarsSale.DataAccess.Repositories.QueryBuilders;
using CarsSale.DataAccess.Servicess.Content;
using CarsSale.WebUi.Filters;
using CarsSale.WebUi.Support;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject.Web.Mvc.FilterBindingSyntax;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(CarsSale.WebUi.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(CarsSale.WebUi.App_Start.NinjectWebCommon), "Stop")]

namespace CarsSale.WebUi.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using CarsSale.DataAccess.Repositories;
    using Microsoft.AspNet.Identity;

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
            kernel.Bind<IContentProvider>().To<AzureProvider>();

            kernel.Bind<IBrandRepository>().To<BrandRepository>()
                .WithConstructorArgument("connectionString", ConnectionStringBuilder.ConnectionString);
            kernel.Bind<IVehiclTypeRepository>().To<VehiclTypeRepository>()
                .WithConstructorArgument("connectionString", ConnectionStringBuilder.ConnectionString);
            kernel.Bind<IFuelRepository>().To<FuelRepository>()
                .WithConstructorArgument("connectionString", ConnectionStringBuilder.ConnectionString);
            kernel.Bind<ITransmissionTypeRepository>().To<TransmissionTypeRepository>()
                .WithConstructorArgument("connectionString", ConnectionStringBuilder.ConnectionString);
            kernel.Bind<IAdvertisementRepository>().To<AdvertisementRepository>()
                .WithConstructorArgument("connectionString", ConnectionStringBuilder.ConnectionString);
            kernel.Bind<IRegionRepository>().To<RegionRepository>()
                .WithConstructorArgument("connectionString", ConnectionStringBuilder.ConnectionString);
            kernel.Bind<ICurrencyRepository>().To<CurrencyRepository>()
                .WithConstructorArgument("connectionString", ConnectionStringBuilder.ConnectionString);

            kernel.Bind<IUserStore<IdentityUser>>().To<UserStore<IdentityUser>>();

            kernel.Bind<IAdvertisementSearchQueryBuilder>().To<AdvertismentSearchQueryBuilder>();

            kernel.BindFilter<ExceptionLoggingFilterAttribute>(FilterScope.Global, 0);
        }        
    }
}
