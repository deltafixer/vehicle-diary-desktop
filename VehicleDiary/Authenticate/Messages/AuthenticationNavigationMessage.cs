namespace VehicleDiary.Authenticate.Messages
{
    public sealed class AuthenticationNavigationMessage
    {
        public AuthenticationNavigationMessage(AuthenticationNavigationOptions navigationOption)
        {
            NavigateTo = navigationOption;
        }

        public AuthenticationNavigationOptions NavigateTo { get; }
    }

    public enum AuthenticationNavigationOptions
    {
        Login,
        RegisterType,
        RegisterPerson,
        RegisterService
    }
}
