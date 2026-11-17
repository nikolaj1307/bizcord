using Models;
using SharedModels;
using SharedModels.Dtos;

namespace ChannelService.Service;

public interface IChannelService
{
    Task<List<ChannelResponseDto>> GetAll();
    Task<ChannelResponseDto?> GetById(string id);
    Task<ChannelResponseDto> Create(CreateChannelDto createDto);
    Task<ChannelResponseDto?> Update(UpdateChannelDto updateDto);
    Task<ChannelResponseDto?> Delete(string id);
    Task AddUserToChannel(ChannelMemberDto addUserMemberDto);
    Task RemoveUserFromChannel(ChannelMemberDto removeUserMemberDto);
    Task<List<ChannelMemberDto>> RemoveUserFromAllChannels(string userId);
    Task ReAddUserToChannels(List<ChannelMemberDto> channelMembers);
}