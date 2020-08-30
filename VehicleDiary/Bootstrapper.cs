using Autofac;
using Caliburn.Micro.Autofac;
using System.Windows;
using VehicleDiary.Authenticate.ViewModels;
using VehicleDiary.Main.ViewModels;
using VehicleDiary.Models;
using VehicleDiary.Services;
using VehicleDiary.ViewModels;

namespace VehicleDiary
{
    public class Bootstrapper : AutofacBootstrapper<RootViewModel>
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<RootViewModel>();
        }

        protected override void ConfigureBootstrapper()
        {
            base.ConfigureBootstrapper();
        }

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<RootViewModel>().SingleInstance();
            builder.RegisterType<VehicleDiaryDbContext>().InstancePerDependency();

            // SERVICES
            builder.RegisterType<UserService>().SingleInstance();
            builder.RegisterType<PersonUserService>().SingleInstance();
            builder.RegisterType<ServiceUserService>().SingleInstance();
            builder.RegisterType<SaleListingService>().SingleInstance();
            builder.RegisterType<VehicleService>().SingleInstance();

            builder.RegisterType<UniversalCRUDService<UserModel>>().InstancePerDependency();
            builder.RegisterType<UniversalCRUDService<PersonUserModel>>().InstancePerDependency();
            builder.RegisterType<UniversalCRUDService<ServiceUserModel>>().InstancePerDependency();
            builder.RegisterType<UniversalCRUDService<VehicleModel>>().InstancePerDependency();
            builder.RegisterType<UniversalCRUDService<PersonUserVehicleModel>>().InstancePerDependency();
            builder.RegisterType<UniversalCRUDService<VehicleSpecificationModel>>().InstancePerDependency();
            
            // AUTHENTICATE
            builder.RegisterType<AuthenticationConductorViewModel>().SingleInstance();
            builder.RegisterType<LoginViewModel>().SingleInstance();
            builder.RegisterType<RegisterTypeViewModel>().SingleInstance();
            builder.RegisterType<RegisterPersonViewModel>().SingleInstance();
            builder.RegisterType<RegisterServiceViewModel>().SingleInstance();

            // MAIN
            builder.RegisterType<MainConductorViewModel>().SingleInstance();
            builder.RegisterType<HeaderViewModel>().SingleInstance();
            builder.RegisterType<ProfilePanelViewModel>().SingleInstance();
            builder.RegisterType<VinCheckViewModel>().SingleInstance();
            builder.RegisterType<MyVehiclesViewModel>().SingleInstance();
            builder.RegisterType<MarketViewModel>().SingleInstance();
            builder.RegisterType<ProfileConductorViewModel>().SingleInstance();
            builder.RegisterType<PersonUserProfileViewModel>().SingleInstance();
            builder.RegisterType<ServiceUserProfileViewModel>().SingleInstance();
            builder.RegisterType<CreateSaleListingViewModel>().SingleInstance();
        }
    }
}
