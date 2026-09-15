using ChannelService.Models;

namespace ChannelService.Data;

public interface IChannelRepository
{
    Task<List<Channel>> GetAll();
    Task<Channel?> GetById(string id);
    Task<Channel> Create(Channel entity);
    Task<Channel?> Update(Channel entity);
    Task<Channel?> Delete(string id);
}