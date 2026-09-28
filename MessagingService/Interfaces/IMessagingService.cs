using MessagingService.Models;
using SharedModels.Dtos.Message;

namespace MessagingService.Data;

public interface IMessagingService
{
    Task<Message> PostMessageAsync(CreateMessageRequest createMessageRequest);
}