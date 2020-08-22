namespace VehicleDiary.Main.Messages
{
    public sealed class DataMessage
    {
        public DataMessage(DataMessages message)
        {
            Message = message;
        }
        public DataMessages Message { get; }
    }

    public enum DataMessages
    {
        CLEAR_ALL,
        USER,
    }
}