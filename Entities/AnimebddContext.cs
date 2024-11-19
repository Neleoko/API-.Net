using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API_.Net.Entities;

public partial class AnimebddContext : DbContext
{
    public AnimebddContext()
    {
    }

    public AnimebddContext(DbContextOptions<AnimebddContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Anime> Animes { get; set; }

    public virtual DbSet<Type> Types { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=;database=animebdd");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Anime>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("anime");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Acronyme)
                .HasMaxLength(255)
                .HasColumnName("acronyme");
            entity.Property(e => e.Nom)
                .HasMaxLength(255)
                .HasColumnName("nom");
        });

        modelBuilder.Entity<Type>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("type");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nom)
                .HasMaxLength(255)
                .HasColumnName("nom");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
