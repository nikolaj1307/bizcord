using System;
using System.Collections.Generic;

namespace Models;

public partial class Channel
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string OwnerId { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<ChannelMember> ChannelMembers { get; set; } = new List<ChannelMember>();
}
