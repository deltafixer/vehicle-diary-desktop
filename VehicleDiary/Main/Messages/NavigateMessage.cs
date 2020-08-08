namespace VehicleDiary.Main.Messages
{
    public sealed class NavigateMessage
    {
        public NavigateMessage(NavigationOptions navigationOption)
        {
            NavigateTo = navigationOption;
        }
        public NavigationOptions NavigateTo { get; }
    }
    public enum NavigationOptions
    {
        Home
    }
}
