namespace MessagingService.Models;

public class Message
{
    public Guid Id { get; set; }

    public Guid ChannelId { get; set; }

    public Guid AuthorId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime PostedAt { get; set; }
}