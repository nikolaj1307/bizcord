using MessagingService.Models;

namespace MessagingService.Infrastructure;

public interface IMessagePublisher
{
    public Task PublishMessage(Message message);
}