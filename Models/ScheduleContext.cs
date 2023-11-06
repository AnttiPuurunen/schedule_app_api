using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ScheduleAppApi.Models;

public partial class ScheduleContext : DbContext
{
    public ScheduleContext()
    {
    }

    public ScheduleContext(DbContextOptions<ScheduleContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<Tasktype> Tasktypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("name=ConnectionStrings:ScheduleDb", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.21-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.Taskid).HasName("PRIMARY");

            entity.ToTable("task");

            entity.HasIndex(e => e.Tasktypeid, "tasktypeid");

            entity.Property(e => e.Taskid)
                .ValueGeneratedNever()
                .HasColumnName("taskid");
            entity.Property(e => e.Duedate)
                .HasColumnType("datetime")
                .HasColumnName("duedate");
            entity.Property(e => e.Iscompleted).HasColumnName("iscompleted");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Tasktypeid).HasColumnName("tasktypeid");

            entity.HasOne(d => d.Tasktype).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.Tasktypeid)
                .HasConstraintName("task_ibfk_1");
        });

        modelBuilder.Entity<Tasktype>(entity =>
        {
            entity.HasKey(e => e.TaskTypeId).HasName("PRIMARY");

            entity.ToTable("tasktype");

            entity.Property(e => e.TaskTypeId).HasColumnName("taskTypeId");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
