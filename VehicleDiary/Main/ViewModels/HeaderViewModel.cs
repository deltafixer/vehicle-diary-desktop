using Caliburn.Micro;
using VehicleDiary.Main.Messages;

namespace VehicleDiary.Main.ViewModels
{
    public class HeaderViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        public HeaderViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Home() => _eventAggregator.PublishOnUIThread(new NavigationMessage(NavigationOptions.Home));
    }
}
