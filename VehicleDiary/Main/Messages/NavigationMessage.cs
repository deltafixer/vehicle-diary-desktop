namespace VehicleDiary.Main.Messages
{
    public sealed class NavigationMessage
    {
        public NavigationMessage(NavigationMessages navigationOption)
        {
            NavigateTo = navigationOption;
        }
        public NavigationMessages NavigateTo { get; }
    }
    public enum NavigationMessages
    {
        MAIN,
        AUTHENTICATION
    }
}
