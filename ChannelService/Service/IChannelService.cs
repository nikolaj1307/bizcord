using SharedModels;

namespace ChannelService.Service;

public interface IChannelService
{
    Task<List<ChannelResponseDto>> GetAll();
    Task<ChannelResponseDto?> GetById(string id);
    Task<ChannelResponseDto> Create(CreateChannelDto createDto);
    Task<ChannelResponseDto?> Update(UpdateChannelDto updateDto);
    Task<ChannelResponseDto?> Delete(string id);
}