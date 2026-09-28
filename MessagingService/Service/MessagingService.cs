using MessagingService.Data;
using MessagingService.Infrastructure;
using MessagingService.Models;
using SharedModels.Dtos.Message;

namespace MessagingService.Service;

public class MessagingService(IMessagingRepository messagingRepository, IMessagePublisher messagePublisher) : IMessagingService
{
    public async Task<Message> PostMessageAsync(CreateMessageRequest createMessageRequest)
    {
        var message = new Message()
        {
            Id = Guid.NewGuid(),
            ChannelId = createMessageRequest.ChannelId,
            AuthorId = createMessageRequest.AuthorId,
            Content = createMessageRequest.Content,
            PostedAt = DateTime.UtcNow
        };
        
        await messagingRepository.CreateAsync(message);
        
        await messagePublisher.PublishMessage(message);

        return CreatedAtAction(nameof(GetMessage), new { id = message.Id }, message);
    }
}