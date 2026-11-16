using ChannelService.Data;
using MessageClient.Interfaces;
using Models;
using Polly;
using SharedModels;
using SharedModels.Dtos;


namespace ChannelService.Service;
public class ChannelService(IChannelRepository channelRepository, IMessageClient messageClient, ResiliencePipeline retryPipeline, ILogger<ChannelService> logger) : IChannelService
{
    public async Task<List<ChannelResponseDto>> GetAll()
    {
        var channels = await channelRepository.GetAll();

        if (channels.Count == 0)
            throw new ChannelException.ChannelNotFoundException("No channels found");

        logger.LogInformation("Retrieved {Count} channels", channels.Count);

        foreach (var channel in channels)
        { 
            channel.ChannelMembers = await channelRepository.GetChannelMembers(channel.Id);
        }        
        
        return channels.Select(c => new ChannelResponseDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            CreatedAt = c.CreatedAt,
            UpdatedAt = c.UpdatedAt,
            Members = c.ChannelMembers.Select(cm => cm.UserId).ToList()
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
            Description = createDto.Description,
            OwnerId = createDto.UserId
        };

        var createdChannel = await channelRepository.Create(channel);
        
        var channelMember = new ChannelMember
        {
            ChannelId = createdChannel.Id,
            UserId = createDto.UserId
        };
        
        var userId = await channelRepository.AddUserToChannel(channelMember);

        return new ChannelResponseDto
        {
            Id = createdChannel.Id,
            Name = createdChannel.Name,
            Description = createdChannel.Description,
            CreatedAt = createdChannel.CreatedAt,
            UpdatedAt = createdChannel.UpdatedAt,
            Members = new List<string> { userId }
        };
    }

    public async Task<ChannelResponseDto?> Update(UpdateChannelDto updateDto)
    {
        if (updateDto.Id == null || updateDto.Id.Trim() == "")
            throw new ChannelException.ChannelIdNotNullOrEmptyException("Channel id is required for update");

        var channel = new Channel
        {
            Id = updateDto.Id,
            Name = updateDto.Name,
            Description = updateDto.Description,
            UpdatedAt = DateTime.UtcNow,
        };

        var updatedChannel = await channelRepository.Update(channel);

        if (updatedChannel == null)
            throw new ChannelException.ChannelNotFoundException("No channel found with the given id");

        var updatedMessage = new ChannelUpdatedMessage
        {
            ChannelId = updatedChannel.Id,
            ChannelName = updatedChannel.Name
        };
        
        await retryPipeline.ExecuteAsync(async cancellationToken =>
        {
            await messageClient.PublishAsync("channel-updated", updatedMessage, cancellationToken);
        });

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

    public async Task AddUserToChannel(ChannelMemberDto addUserMemberDto)
    {
        var channelMember = new ChannelMember
        {
            ChannelId = addUserMemberDto.ChannelId,
            UserId = addUserMemberDto.UserId
        };
        
        await channelRepository.AddUserToChannel(channelMember);
    }

    public async Task RemoveUserFromChannel(ChannelMemberDto removeUserMemberDto)
    {
        var channelMember = new ChannelMember
        {
            ChannelId = removeUserMemberDto.ChannelId,
            UserId = removeUserMemberDto.UserId
        };
        
        await channelRepository.RemoveChannelMember(channelMember);
    }
}