using MessagingService.Models;

namespace MessagingService.Data;

public interface IMessagingRepository
{
    Task<Message> CreateAsync(Message message);
}