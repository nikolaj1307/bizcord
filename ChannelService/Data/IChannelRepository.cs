using Models;

namespace ChannelService.Data;

public interface IChannelRepository
{
    Task<List<Channel>> GetAll();
    Task<Channel?> GetById(string id);
    Task<Channel> Create(Channel entity);
    Task<Channel?> Update(Channel entity);
    Task<Channel?> Delete(string id);
    Task<string> AddUserToChannel(ChannelMember entity);
    Task RemoveChannelMember(ChannelMember entity);
    Task<List<ChannelMember>> GetChannelMembers(string channelId);
    Task<List<ChannelMember>> RemoveUserFromAllChannels(string userId);
    Task ReAddUserToChannels(List<ChannelMember> channelMembers);
}