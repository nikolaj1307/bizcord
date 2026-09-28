namespace SharedModels.Dtos.Message;

public class CreateMessageRequest
{
    public Guid ChannelId { get; set; }
    public Guid AuthorId { get; set; }
    public string Content { get; set; }
}