using SharedModels.Dtos;

namespace SharedModels.Events;

// Step 1: User Management
public class UserBanned
{
    public Guid SagaId { get; set; } = Guid.NewGuid();
    public string UserId { get; set; } = null!;
}

public class UserBanSucceeded
{
    public Guid SagaId { get; set; } = Guid.NewGuid();
    public string UserId { get; set; } = null!;
}

public class UserBanFailed
{
    public Guid SagaId { get; set; } = Guid.NewGuid();
    public string UserId { get; set; } = null!;
    public string Reason { get; set; } = null!;
}

public class UserUnbanned
{
    public Guid SagaId { get; set; } = Guid.NewGuid();
    public string UserId { get; set; } = null!;
}

// Step 2: Channel
public class BanFromChannelsSucceeded
{
    public Guid SagaId { get; set; } = Guid.NewGuid();
    public string UserId { get; set; } = null!;
    public List<ChannelMemberDto> ChannelMembers { get; set; } = new();
}

public class BanFromChannelsFailed
{
    public Guid SagaId { get; set; } = Guid.NewGuid();
    public string UserId { get; set; } = null!;
    public string Reason { get; set; } = null!;
}

public class ReaddedToChannels
{
    public Guid SagaId { get; set; } = Guid.NewGuid();
    public string UserId { get; set; } = null!;
}

// Step 3: Streaming / VOIP
public class SessionTerminatedSucceeded
{
    public Guid SagaId { get; set; } = Guid.NewGuid();
    public string UserId { get; set; } = null!;
}

public class SessionTerminatedFailed
{
    public Guid SagaId { get; set; } = Guid.NewGuid();
    public string UserId { get; set; } = null!;
    public List<ChannelMemberDto> ChannelMembers { get; set; } = new();
    public string Reason { get; set; } = null!;
}

public class SessionRestored
{
    public Guid SagaId { get; set; } = Guid.NewGuid();
    public string UserId { get; set; } = null!;

}