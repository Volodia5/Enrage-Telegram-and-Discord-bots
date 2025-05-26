using System;
using System.Collections.Generic;
using EnrageTgBotILovePchel.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace EnrageTgBotILovePchel.Db.DbConnector;

public partial class EnrageBotVovodyaDbContext : DbContext
{
    public EnrageBotVovodyaDbContext()
    {
    }

    public EnrageBotVovodyaDbContext(DbContextOptions<EnrageBotVovodyaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TournamentsDatum> TournamentsData { get; set; }

    public virtual DbSet<UsersDatum> UsersData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("your connection string");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TournamentsDatum>(entity =>
        {
            entity.HasKey(e => e.TournId).HasName("tournaments_data_pk");

            entity.ToTable("tournaments_data");

            entity.HasIndex(e => e.TournId, "tournaments_data_pk2").IsUnique();

            entity.Property(e => e.TournId).HasColumnName("tourn_id");
            entity.Property(e => e.TournamentsRules)
                .HasMaxLength(250000)
                .HasColumnName("tournaments_rules");
        });

        modelBuilder.Entity<UsersDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_data_pk");

            entity.ToTable("users_data");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ChatId).HasColumnName("chat_id");
            entity.Property(e => e.PlayerDescription)
                .HasMaxLength(4000)
                .HasColumnName("player_description");
            entity.Property(e => e.PlayerName)
                .HasMaxLength(25)
                .HasColumnName("player_name");
            entity.Property(e => e.PlayerPosition).HasColumnName("player_position");
            entity.Property(e => e.PlayerRating).HasColumnName("player_rating");
            entity.Property(e => e.PlayerTgNick)
                .HasColumnType("character varying")
                .HasColumnName("player_tg_nick");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
