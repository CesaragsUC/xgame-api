using MediatR;

namespace Application.API.Messages
{
    public class Event : Message, INotification
    {

        public DateTime Timestamp { get; set; }

        public Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}
