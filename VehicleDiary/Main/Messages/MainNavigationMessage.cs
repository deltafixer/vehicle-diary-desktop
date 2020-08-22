namespace VehicleDiary.Main.Messages
{
    public sealed class MainNavigationMessage
    {
        public MainNavigationMessage(MainNavigationMessages navigationOption)
        {
            NavigateTo = navigationOption;
        }
        public MainNavigationMessages NavigateTo { get; }
    }
    public enum MainNavigationMessages
    {
        PROFILE,
    }
}