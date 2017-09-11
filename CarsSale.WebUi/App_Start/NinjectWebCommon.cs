using CarsSale.DataAccess;
using CarsSale.DataAccess.Repositories.Interfaces;
using CarsSale.DataAccess.Services;
using CarsSale.DataAccess.Services.Interfaces;

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
            kernel.Bind<CarsSaleEntities>().ToSelf().InRequestScope();

            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IRoleRepository>().To<RoleRepository>();
            kernel.Bind<IUserService>().To<UserService>();

            kernel.Bind<IVehiclRepository>().To<VehiclRepository>();
            kernel.Bind<IBrandRepository>().To<BrandRepository>();
            kernel.Bind<IVehiclTypeRepository>().To<VehiclTypeRepository>();
            kernel.Bind<ICompleteSetRepository>().To<CompleteSetRepository>();
            kernel.Bind<IEngineFuelRepository>().To<EngineFuelRepository>();
            kernel.Bind<IEngineRepository>().To<EngineRepository>();
            kernel.Bind<IFuelRepository>().To<FuelRepository>();
            kernel.Bind<ITransmissionTypeRepository>().To<TransmissionTypeRepository>();
            kernel.Bind<IAdvertisementRepository>().To<AdvertisementRepository>();
            kernel.Bind<IRegionRepository>().To<RegionRepository>();

            kernel.Bind<IAdvertisementService>().To<AdvertisementService>();
        }        
    }
}
