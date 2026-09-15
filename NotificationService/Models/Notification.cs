using System;
using System.Collections.Generic;

namespace Models;

public partial class Notification
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Message { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }
}
