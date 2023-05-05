using Infrastructure.Enums;

namespace Infrastructure.Models
{
    public class KafkaMessage
    {
        public MessageTypesEnum Type { get; }
        public string Message { get; }

        public KafkaMessage(MessageTypesEnum type, string message)
        {
            Type = type;
            Message = message;
        }
    }
}
