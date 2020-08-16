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
            builder.RegisterType<UniversalCRUDService<UserModel>>().InstancePerDependency();
            builder.RegisterType<UserService>().InstancePerDependency();
            builder.RegisterType<UniversalCRUDService<PersonUserModel>>().InstancePerDependency();
            builder.RegisterType<UniversalCRUDService<ServiceUserModel>>().InstancePerDependency();

            // AUTHENTICATE
            builder.RegisterType<AuthenticationConductorViewModel>().SingleInstance();
            builder.RegisterType<LoginViewModel>().SingleInstance();
            builder.RegisterType<RegisterTypeViewModel>().SingleInstance();
            builder.RegisterType<RegisterPersonViewModel>().SingleInstance();
            builder.RegisterType<RegisterServiceViewModel>().SingleInstance();

            // MAIN
            builder.RegisterType<MainConductorViewModel>().SingleInstance();
            builder.RegisterType<HeaderViewModel>().SingleInstance();
            builder.RegisterType<HomeViewModel>().SingleInstance();
        }
    }
}
