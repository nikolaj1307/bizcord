using ChannelService.Service;
using MessageClient.Interfaces;
using SharedModels.Events;

namespace ChannelService.Infrastructure;

public class ChannelHandler(IChannelService channelService, IMessageClient messageClient) 
{
    public async Task Handle(UserBanSucceeded message, CancellationToken ct)
    {
        try
        {
            var channelMembers = await channelService.RemoveUserFromAllChannels(message.UserId);

            await messageClient.PublishAsync("BanFromChannelsSucceeded", new BanFromChannelsSucceeded
            {
                SagaId = message.SagaId,
                UserId = message.UserId,
                ChannelMembers = channelMembers
            });
        }
        catch (Exception ex)
        {
            await messageClient.PublishAsync("BanFromChannelsFailed", new BanFromChannelsFailed
            {
                SagaId = message.SagaId,
                UserId = message.UserId,
                Reason = ex.Message
            });
        }
    }
    
    public async Task Handle(SessionTerminatedFailed message, CancellationToken ct)
    {
        await channelService.ReAddUserToChannels(message.ChannelMembers);

        await messageClient.PublishAsync("ReaddedToChannels", new ReaddedToChannels
        {
            UserId = message.UserId,
            SagaId = message.SagaId,
        });
    }
}