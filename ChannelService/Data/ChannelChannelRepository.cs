using ChannelService.Models;
using Microsoft.EntityFrameworkCore;

namespace ChannelService.Data;

public class ChannelChannelRepository(ChannelContext channelContext) : IChannelRepository
{
    public async Task<List<Channel>> GetAll()
    {
        return await channelContext.Channels.ToListAsync();
    }

    public async Task<Channel?> GetById(string id)
    {
        return await channelContext.Channels.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Channel> Create(Channel entity)
    {
        channelContext.Channels.Add(entity);
        await channelContext.SaveChangesAsync();
        return entity;
    }

    public async Task<Channel?> Update(Channel entity)
    {
        var existingChannel = await channelContext.Channels.FirstOrDefaultAsync(c => c.Id == entity.Id);
        if (existingChannel == null)
            return null;

        existingChannel.Name = entity.Name;
        existingChannel.Description = entity.Description;
        existingChannel.UpdatedAt = entity.UpdatedAt;

        var updatedChannel = channelContext.Channels.Update(existingChannel);
        await channelContext.SaveChangesAsync();
        return updatedChannel.Entity;
    }

    public async Task<Channel?> Delete(string id)
    {
        var channel = await channelContext.Channels.FirstOrDefaultAsync(c => c.Id == id);
        if (channel == null)
            return null;

        channelContext.Channels.Remove(channel);
        await channelContext.SaveChangesAsync();
        return channel;
    }
}