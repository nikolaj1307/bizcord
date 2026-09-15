using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Infrastructure;

public partial class ChannelContext : DbContext
{
    public ChannelContext(DbContextOptions<ChannelContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
