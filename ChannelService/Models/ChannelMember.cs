using System;
using System.Collections.Generic;

namespace Models;

public partial class ChannelMember
{
    public string ChannelId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public DateTime? JoinedAt { get; set; }

    public virtual Channel Channel { get; set; } = null!;
}
