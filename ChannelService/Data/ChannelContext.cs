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

    public virtual DbSet<Channel> Channels { get; set; }

    public virtual DbSet<ChannelMember> ChannelMembers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Channel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("channels_pkey");

            entity.ToTable("channels");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.OwnerId).HasColumnName("owner_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<ChannelMember>(entity =>
        {
            entity.HasKey(e => new { e.ChannelId, e.UserId }).HasName("channel_members_pkey");

            entity.ToTable("channel_members");

            entity.Property(e => e.ChannelId).HasColumnName("channel_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.JoinedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("joined_at");

            entity.HasOne(d => d.Channel).WithMany(p => p.ChannelMembers)
                .HasForeignKey(d => d.ChannelId)
                .HasConstraintName("channel_members_channel_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
