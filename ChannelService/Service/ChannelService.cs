using ChannelService.Data;
using ChannelService.Infrastructure;
using ChannelService.Models;
using SharedModels;
using SharedModels.Dtos;

namespace ChannelService.Service;

public class ChannelService(IChannelRepository channelRepository, IMessagePublisher messagePublisher) : IChannelService
{
    public async Task<List<ChannelResponseDto>> GetAll()
    {
        var channels = await channelRepository.GetAll();

        if (channels.Count == 0)
            throw new ChannelException.ChannelNotFoundException("No channels found");

        return channels.Select(c => new ChannelResponseDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            CreatedAt = c.CreatedAt,
            UpdatedAt = c.UpdatedAt
        }).ToList();
    }

    public async Task<ChannelResponseDto?> GetById(string id)
    {
        var channel = await channelRepository.GetById(id);

        if (channel == null)
            throw new ChannelException.ChannelNotFoundException("No channel found with the given id");

        return new ChannelResponseDto
        {
            Id = channel.Id,
            Name = channel.Name,
            Description = channel.Description,
            CreatedAt = channel.CreatedAt,
            UpdatedAt = channel.UpdatedAt
        };
    }

    public async Task<ChannelResponseDto> Create(CreateChannelDto createDto)
    {
        var channel = new Channel
        {
            Id = Guid.NewGuid().ToString(),
            Name = createDto.Name,
            Description = createDto.Description
        };

        var createdChannel = await channelRepository.Create(channel);

        return new ChannelResponseDto
        {
            Id = createdChannel.Id,
            Name = createdChannel.Name,
            Description = createdChannel.Description,
            CreatedAt = createdChannel.CreatedAt,
            UpdatedAt = createdChannel.UpdatedAt
        };
    }

    public async Task<ChannelResponseDto?> Update(UpdateChannelDto updateDto)
    {
        if (updateDto.Id == null)
            throw new ChannelException.ChannelNotFoundException("Channel id is required for update");

        var channel = new Channel
        {
            Id = updateDto.Id,
            Name = updateDto.Name,
            Description = updateDto.Description,
            UpdatedAt = DateTime.UtcNow
        };

        var updatedChannel = await channelRepository.Update(channel);

        if (updatedChannel == null)
            throw new ChannelException.ChannelNotFoundException("No channel found with the given id");

        await messagePublisher.PublishChannelMessage(updatedChannel);

        return new ChannelResponseDto
        {
            Id = updatedChannel.Id,
            Name = updatedChannel.Name,
            Description = updatedChannel.Description,
            CreatedAt = updatedChannel.CreatedAt,
            UpdatedAt = updatedChannel.UpdatedAt
        };
    }

    public async Task<ChannelResponseDto?> Delete(string id)
    {
        var deletedChannel = await channelRepository.Delete(id);

        if (deletedChannel == null)
            throw new ChannelException.ChannelNotFoundException("No channel found with the given id");

        return new ChannelResponseDto
        {
            Id = deletedChannel.Id,
            Name = deletedChannel.Name,
            Description = deletedChannel.Description,
            CreatedAt = deletedChannel.CreatedAt,
            UpdatedAt = deletedChannel.UpdatedAt
        };
    }
}