using MessageClient.Interfaces;
using MessagingService.Models;
using SharedModels.Events;

namespace MessagingService.Infrastructure;

public class MessagePublisher(IMessageClient messageClient) : IMessagePublisher
{
    public async Task PublishMessage(Message message)
    {
        if (message.Id == null)
        {
            throw new ArgumentNullException(nameof(message));
        }
        await messageClient.PublishAsync(new MessagePostedEvent(
        {
           MessageId = message.Id,
              ChannelId = message.ChannelId,
                AuthorId = message.AuthorId,
                Content = message.Content,
                PostedAt = message.PostedAt
        });
        
    }
}