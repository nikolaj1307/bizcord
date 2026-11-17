using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Models;

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
        await channelContext.Channels.AddAsync(entity);
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

    public async Task<string> AddUserToChannel(ChannelMember entity)
    {
        await channelContext.ChannelMembers.AddAsync(entity);
        await channelContext.SaveChangesAsync();
        return entity.UserId;
    }

    public async Task RemoveChannelMember(ChannelMember entity)
    {
        channelContext.ChannelMembers.Remove(entity);
        await channelContext.SaveChangesAsync();
    }

    public async Task<List<ChannelMember>> GetChannelMembers(string channelId)
    {
        return await channelContext.ChannelMembers
            .Where(cm => cm.ChannelId == channelId)
            .ToListAsync();
    }

    public async Task<List<ChannelMember>> RemoveUserFromAllChannels(string userId)
    {
        var channelMembers = channelContext.ChannelMembers
            .Where(cm => cm.UserId == userId);

        channelContext.ChannelMembers.RemoveRange(channelMembers);
        await channelContext.SaveChangesAsync();
        
        return channelMembers.ToList();
    }

    public async Task ReAddUserToChannels(List<ChannelMember> channelMembers)
    {
        foreach (var member in channelMembers)
        {
            await channelContext.ChannelMembers.AddAsync(member);
        }
        await channelContext.SaveChangesAsync();
    }
}