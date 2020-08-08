using Caliburn.Micro;
using VehicleDiary.Login.ViewModels;
using VehicleDiary.Main.Messages;
using VehicleDiary.Main.ViewModels;

namespace VehicleDiary.ViewModels
{
    public class RootViewModel : Conductor<Screen>.Collection.OneActive, IHandle<AuthenticatedMessage>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly LoginConductorViewModel _loginConductorViewModel;
        private readonly MainConductorViewModel _mainConductorViewModel;

        public RootViewModel(IEventAggregator eventAggregator, LoginConductorViewModel loginConductorViewModel, MainConductorViewModel mainConductorViewModel)
        {
            _eventAggregator = eventAggregator;
            _loginConductorViewModel = loginConductorViewModel;
            _mainConductorViewModel = mainConductorViewModel;

            Items.AddRange(new Screen[] { _loginConductorViewModel, _mainConductorViewModel });
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            _eventAggregator.Subscribe(this);
            ActivateItem(_loginConductorViewModel);
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
            _eventAggregator.Unsubscribe(this);
        }

        public void Handle(AuthenticatedMessage message)
        {
            ActivateItem(_mainConductorViewModel);
        }
    }
}
