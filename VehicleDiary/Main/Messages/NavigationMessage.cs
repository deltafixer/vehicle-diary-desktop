namespace VehicleDiary.Main.Messages
{
    public sealed class NavigationMessage
    {
        public NavigationMessage(NavigationOptions navigationOption)
        {
            NavigateTo = navigationOption;
        }
        public NavigationOptions NavigateTo { get; }
    }
    public enum NavigationOptions
    {
        Home,
        Authentication
    }
}
