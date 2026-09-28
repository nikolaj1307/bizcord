using Channel = ChannelService.Models.Channel;

namespace ChannelService.Infrastructure;

public interface IMessagePublisher
{
    public Task PublishChannelMessage(Channel channel);
}