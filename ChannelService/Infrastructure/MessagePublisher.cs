using ChannelService.Models;
using MessageClient.Interfaces;
using SharedModels;

namespace ChannelService.Infrastructure;

public class MessagePublisher(IMessageClient messageClient) : IMessagePublisher
{
    
    public async Task PublishChannelMessage(Channel channel)
    {

        if (channel.Id == null) return;
        
        ChannelUpdatedMessage message = new ChannelUpdatedMessage
        {
            ChannelId = channel.Id,
            ChannelName = channel.Name
        };

        await messageClient.PublishAsync<ChannelUpdatedMessage>("channel-updated", message);

    }
}