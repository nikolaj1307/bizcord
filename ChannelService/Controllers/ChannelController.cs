using ChannelService.Service;
using Microsoft.AspNetCore.Mvc;
using SharedModels;
using SharedModels.Dtos;

namespace ChannelService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChannelController(IChannelService channelService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<ChannelResponseDto>>> GetAll()
    {
        var channels = await channelService.GetAll();
        return Ok(channels);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ChannelResponseDto>> GetById(string id)
    {
        var channel = await channelService.GetById(id);
        if (channel == null) return NotFound();
        return Ok(channel);
    }

    [HttpPost]
    public async Task<ActionResult<ChannelResponseDto>> Create(CreateChannelDto createChannelDto)
    {
        var createdChannel = await channelService.Create(createChannelDto);
        return Ok(createdChannel);
    }

    [HttpPut]
    public async Task<ActionResult<ChannelResponseDto>> Update(UpdateChannelDto updateChannelDto)
    {
        var updatedChannel = await channelService.Update(updateChannelDto);
        if (updatedChannel == null) return NotFound();
        return Ok(updatedChannel);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ChannelResponseDto>> Delete(string id)
    {
        var deletedChannel = await channelService.Delete(id);
        if (deletedChannel == null) return NotFound();
        return Ok(deletedChannel);
    }
}