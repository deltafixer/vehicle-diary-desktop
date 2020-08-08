using Autofac;
using Caliburn.Micro.Autofac;
using System.Windows;
using VehicleDiary.Main.ViewModels;
using VehicleDiary.Login.ViewModels;
using VehicleDiary.Providers;
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

            builder.RegisterType<UserProvider>().SingleInstance();

            // LOGIN
            builder.RegisterType<LoginConductorViewModel>().SingleInstance();
            builder.RegisterType<LoginViewModel>().SingleInstance();
            builder.RegisterType<RegisterViewModel>().SingleInstance();

            // MAIN
            builder.RegisterType<MainConductorViewModel>().SingleInstance();
            builder.RegisterType<HeaderViewModel>().SingleInstance();
            builder.RegisterType<HomeViewModel>().SingleInstance();
        }
    }
}
