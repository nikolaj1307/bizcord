using ChannelService.Controllers;
using ChannelService.Data;
using ChannelService.Models;
using MessageClient.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SharedModels;
using SharedModels.Dtos;

namespace ChannelService.Tests;

public class TestChannelServiceUpdate
{
    [Fact]
    public async Task UpdateShould_CallPublish()
    {
        //Arrange
        var currentChannel = new Channel
        {
            Id = "1234",
            Name = "CurrentChannel",
            Description = "CurrentDescription",
            CreatedAt = new DateTime(2024, 1, 1),
            UpdatedAt = new DateTime(2024, 1, 1)
        };
        
        var updatedChannel = new Channel
        {
            Id = "1234",
            Name = "UpdatedChannel",
            Description = "UpdatedDescription",
            CreatedAt = new DateTime(2024, 1, 1),
            UpdatedAt = DateTime.UtcNow,
        };
        
        var channelResponseDto = new ChannelResponseDto()
        {
            Id = updatedChannel.Id,
            Name = updatedChannel.Name,
            Description = updatedChannel.Description,
            CreatedAt = updatedChannel.CreatedAt,
            UpdatedAt = updatedChannel.UpdatedAt
        };
        
        var updatedMessage = new ChannelUpdatedMessage
        {
            ChannelId = "1234",
            ChannelName = "UpdatedChannel"
        };
        
        
        var mockMessageClient = new Mock<IMessageClient>();
        var mockResiliencePipeline = new Mock<Polly.ResiliencePipeline>();
        var mockRepo = new Mock<IChannelRepository>();
        mockMessageClient.Setup(client => client.PublishAsync("channel-updated", updatedMessage));
        mockRepo
            .Setup(repo => repo.GetById("1234"))
            .ReturnsAsync(currentChannel);
        mockRepo
            .Setup(repo => repo.Update(It.IsAny<Channel>()))
            .ReturnsAsync(updatedChannel);

        var service = new Service.ChannelService(mockRepo.Object, mockMessageClient.Object, mockResiliencePipeline.Object);

        var controller = new ChannelController(service);
        //Act

        var result = await controller.Update(new UpdateChannelDto
        {
            Id = updatedChannel.Id,
            Name = updatedChannel.Name,
            Description = updatedChannel.Description
        });
        
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var response = Assert.IsType<ChannelResponseDto>(okResult.Value);

        //Assert
        Assert.Equal(channelResponseDto.Id, response.Id);
        Assert.Equal(channelResponseDto.Name, response.Name);
        Assert.Equal(channelResponseDto.Description, response.Description);
        Assert.Equal(channelResponseDto.CreatedAt, response.CreatedAt);
        Assert.Equal(channelResponseDto.UpdatedAt, response.UpdatedAt);

        mockMessageClient.Verify(
            client => client.PublishAsync(
                "channel-updated",
                It.IsAny<ChannelUpdatedMessage>()),
            Times.AtMostOnce);
    }
}