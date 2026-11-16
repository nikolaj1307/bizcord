using ChannelService.Service;
using SharedModels.Dtos;

namespace ChannelService.GraphQL;

public class ChannelQuery
{
    public async Task<List<ChannelResponseDto>> GetAllChannels(
        [Service] IChannelService channelService)
    {
        return await channelService.GetAll();
    }
}