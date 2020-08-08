using Caliburn.Micro;
using System.Windows;
using VehicleDiary.ViewModels;

namespace VehicleDiary
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<RootViewModel>();
        }
    }
}
