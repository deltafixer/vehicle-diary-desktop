namespace VehicleDiary.Main.Messages
{
    public sealed class DataMessage
    {
        public DataMessage(DataMessages message, object data = null)
        {
            Message = message;
            Data = data;
        }
        public DataMessages Message { get; }
        public object Data { get; }
    }

    public enum DataMessages
    {
        CLEAR_ALL,
        USER,
        HEADER_LOADED,
        SALE_LISTING_REMOVED,
        SALE_LISTING_CREATED,
    }
}