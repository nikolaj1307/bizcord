using ChannelService.Service;
using SharedModels.Dtos;

namespace ChannelService.GraphQL;

public class ChannelMutation
{
    public async Task<ChannelResponseDto> CreateChannel(
        CreateChannelDto request,
        [Service] IChannelService channelService)
    {
        return await channelService.Create(request);
    }
}