namespace SharedModels.Dtos;

public class UpdateChannelDto
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}