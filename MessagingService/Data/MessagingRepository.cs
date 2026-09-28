using MessagingService.Models;

namespace MessagingService.Data;

public class MessagingRepository(MessageContext dbContext) : IMessagingRepository
{
    public async Task<Message> CreateAsync(Message message)
    {
        var savedMessage = await dbContext.Messages.AddAsync(message);
        await dbContext.SaveChangesAsync();
        return savedMessage.Entity;
    }
}