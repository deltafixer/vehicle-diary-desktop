namespace VehicleDiary.Authenticate.Messages
{
    public sealed class AuthenticationNavigationMessage
    {
        public AuthenticationNavigationMessage(AuthenticationNavigationMessages navigationOption)
        {
            NavigateTo = navigationOption;
        }

        public AuthenticationNavigationMessages NavigateTo { get; }
    }

    public enum AuthenticationNavigationMessages
    {
        LOGIN,
        REGISTER_TYPE,
        REGISTER_PERSON,
        REGISTER_SERVICE
    }
}
