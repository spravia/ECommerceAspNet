namespace ECommerce.Util
{
    public class Message
    {
        public Message(string type, string description)
        {
            Type = type;
            Description = description;
        }

        public string Type { get; private set; }
        public string Description { get; private set; }
    }

}
