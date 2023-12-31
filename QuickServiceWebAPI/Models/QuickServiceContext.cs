﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QuickServiceWebAPI.Models;

public partial class QuickServiceContext : DbContext
{
    public QuickServiceContext()
    {
    }

    public QuickServiceContext(DbContextOptions<QuickServiceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AggregatedCounter> AggregatedCounters { get; set; }

    public virtual DbSet<Asset> Assets { get; set; }

    public virtual DbSet<AssetAssignment> AssetAssignments { get; set; }

    public virtual DbSet<AssetHistory> AssetHistories { get; set; }

    public virtual DbSet<Attachment> Attachments { get; set; }

    public virtual DbSet<BusinessHour> BusinessHours { get; set; }

    public virtual DbSet<Change> Changes { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Counter> Counters { get; set; }

    public virtual DbSet<CustomField> CustomFields { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Hash> Hashes { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<JobParameter> JobParameters { get; set; }

    public virtual DbSet<JobQueue> JobQueues { get; set; }

    public virtual DbSet<List> Lists { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Problem> Problems { get; set; }

    public virtual DbSet<Query> Queries { get; set; }

    public virtual DbSet<RequestTicket> RequestTickets { get; set; }

    public virtual DbSet<RequestTicketExt> RequestTicketExts { get; set; }

    public virtual DbSet<RequestTicketHistory> RequestTicketHistories { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Schema> Schemas { get; set; }

    public virtual DbSet<Server> Servers { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServiceCategory> ServiceCategories { get; set; }

    public virtual DbSet<ServiceDeskHour> ServiceDeskHours { get; set; }

    public virtual DbSet<ServiceItem> ServiceItems { get; set; }

    public virtual DbSet<ServiceItemCustomField> ServiceItemCustomFields { get; set; }

    public virtual DbSet<ServiceType> ServiceTypes { get; set; }

    public virtual DbSet<Set> Sets { get; set; }

    public virtual DbSet<Sla> Slas { get; set; }

    public virtual DbSet<Slametric> Slametrics { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Workflow> Workflows { get; set; }

    public virtual DbSet<WorkflowAssignment> WorkflowAssignments { get; set; }

    public virtual DbSet<WorkflowTask> WorkflowTasks { get; set; }

    public virtual DbSet<WorkflowTransition> WorkflowTransitions { get; set; }

    public virtual DbSet<YearlyHolidayList> YearlyHolidayLists { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AggregatedCounter>(entity =>
        {
            entity.HasKey(e => e.Key).HasName("PK_HangFire_CounterAggregated");

            entity.ToTable("AggregatedCounter", "HangFire");

            entity.HasIndex(e => e.ExpireAt, "IX_HangFire_AggregatedCounter_ExpireAt").HasFilter("([ExpireAt] IS NOT NULL)");

            entity.Property(e => e.Key).HasMaxLength(100);
            entity.Property(e => e.ExpireAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Asset>(entity =>
        {
            entity.HasKey(e => e.AssetId).HasName("PK__Assets__43492372FD1E1DD6");

            entity.ToTable("Assets", "QuickServices");

            entity.Property(e => e.AssetId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("AssetID");
            entity.Property(e => e.AssetName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.AssetType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ExpiryDate).HasColumnType("date");
            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Manufacturer)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PurchaseDate).HasColumnType("date");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AssetAssignment>(entity =>
        {
            entity.HasKey(e => e.AssetAssignmentId).HasName("PK__AssetAss__9C8C06915CB3B210");

            entity.ToTable("AssetAssignments", "QuickServices");

            entity.Property(e => e.AssetAssignmentId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("AssetAssignmentID");
            entity.Property(e => e.AssetId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.AssignedDate).HasColumnType("datetime");
            entity.Property(e => e.AssignedTo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ReturnDate).HasColumnType("datetime");

            entity.HasOne(d => d.Asset).WithMany(p => p.AssetAssignments)
                .HasForeignKey(d => d.AssetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AssetAssi__Asset__2739D489");

            entity.HasOne(d => d.AssignedToNavigation).WithMany(p => p.AssetAssignments)
                .HasForeignKey(d => d.AssignedTo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AssetAssi__Assig__282DF8C2");
        });

        modelBuilder.Entity<AssetHistory>(entity =>
        {
            entity.HasKey(e => e.AssetHistoryId).HasName("PK__AssetHis__5681DDED2A133A4F");

            entity.ToTable("AssetHistories", "QuickServices");

            entity.Property(e => e.AssetHistoryId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("AssetHistoryID");
            entity.Property(e => e.Action)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ActionTime).HasColumnType("datetime");
            entity.Property(e => e.AssetId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.Asset).WithMany(p => p.AssetHistories)
                .HasForeignKey(d => d.AssetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AssetHist__Asset__30C33EC3");
        });

        modelBuilder.Entity<Attachment>(entity =>
        {
            entity.HasKey(e => e.AttachmentId).HasName("PK__Attachme__442C64DE88F82C5D");

            entity.ToTable("Attachments", "QuickServices");

            entity.Property(e => e.AttachmentId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("AttachmentID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.FilePath)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Filename)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<BusinessHour>(entity =>
        {
            entity.HasKey(e => e.BusinessHourId).HasName("PK__Business__D34018BCF1C7015B");

            entity.ToTable("BusinessHours", "QuickServices");

            entity.Property(e => e.BusinessHourId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("BusinessHourID");
            entity.Property(e => e.BusinessHourName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TimeZone)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<Change>(entity =>
        {
            entity.ToTable("Changes", "QuickServices");

            entity.Property(e => e.ChangeId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ChangeID");
            entity.Property(e => e.AssigneeId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("AssigneeID");
            entity.Property(e => e.AttachmentId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("AttachmentID");
            entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            entity.Property(e => e.GroupId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("GroupID");
            entity.Property(e => e.Impact).HasMaxLength(20);
            entity.Property(e => e.Priority).HasMaxLength(20);
            entity.Property(e => e.RequesterId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("RequesterID");
            entity.Property(e => e.Slaid)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SLAID");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Title).HasMaxLength(250);

            entity.HasOne(d => d.Assignee).WithMany(p => p.ChangeAssignees)
                .HasForeignKey(d => d.AssigneeId)
                .HasConstraintName("FK_Changes_Users");

            entity.HasOne(d => d.Attachment).WithMany(p => p.Changes)
                .HasForeignKey(d => d.AttachmentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Changes_Attachments");

            entity.HasOne(d => d.Group).WithMany(p => p.Changes)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Changes_Group");

            entity.HasOne(d => d.Requester).WithMany(p => p.ChangeRequesters)
                .HasForeignKey(d => d.RequesterId)
                .HasConstraintName("FK__Changes__Request__60083D91");

            entity.HasOne(d => d.Sla).WithMany(p => p.Changes)
                .HasForeignKey(d => d.Slaid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Changes__SLAID__27C3E46E");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comments__C3B4DFCA8AF1F18E");

            entity.ToTable("Comments", "QuickServices");

            entity.Property(e => e.CommentId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.AttachmentId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("AttachmentID");
            entity.Property(e => e.ChangeId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ChangeID");
            entity.Property(e => e.CommentBy)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CommentText).HasMaxLength(1000);
            entity.Property(e => e.CommentTime).HasColumnType("datetime");
            entity.Property(e => e.LastModified).HasColumnType("datetime");
            entity.Property(e => e.ProblemId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ProblemID");
            entity.Property(e => e.RequestTicketId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("RequestTicketID");

            entity.HasOne(d => d.Attachment).WithMany(p => p.Comments)
                .HasForeignKey(d => d.AttachmentId)
                .HasConstraintName("FK__Comments__Attach__19DFD96B");

            entity.HasOne(d => d.Change).WithMany(p => p.Comments)
                .HasForeignKey(d => d.ChangeId)
                .HasConstraintName("FK__Comments__Change__60FC61CA");

            entity.HasOne(d => d.CommentByNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.CommentBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comments__Commen__17F790F9");

            entity.HasOne(d => d.Problem).WithMany(p => p.Comments)
                .HasForeignKey(d => d.ProblemId)
                .HasConstraintName("FK__Comments__Proble__62E4AA3C");

            entity.HasOne(d => d.RequestTicket).WithMany(p => p.Comments)
                .HasForeignKey(d => d.RequestTicketId)
                .HasConstraintName("FK__Comments__Reques__18EBB532");
        });

        modelBuilder.Entity<Counter>(entity =>
        {
            entity.HasKey(e => new { e.Key, e.Id }).HasName("PK_HangFire_Counter");

            entity.ToTable("Counter", "HangFire");

            entity.Property(e => e.Key).HasMaxLength(100);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ExpireAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<CustomField>(entity =>
        {
            entity.HasKey(e => e.CustomFieldId).HasName("PK__CustomFi__403326D4BB7BA42B");

            entity.ToTable("CustomFields", "QuickServices");

            entity.Property(e => e.CustomFieldId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CustomFieldID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DefaultValue).HasMaxLength(500);
            entity.Property(e => e.FieldCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FieldDescription).HasMaxLength(300);
            entity.Property(e => e.FieldName).HasMaxLength(100);
            entity.Property(e => e.FieldType)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ListOfValue).HasMaxLength(500);
            entity.Property(e => e.ListOfValueDisplay).HasMaxLength(2500);
            entity.Property(e => e.ValType)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PK__Groups__149AF30AAA93ADA9");

            entity.ToTable("Groups", "QuickServices");

            entity.HasIndex(e => e.GroupName, "UQ__Groups__6EFCD43454568209").IsUnique();

            entity.Property(e => e.GroupId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("GroupID");
            entity.Property(e => e.BusinessHourId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("BusinessHourID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.GroupLeader)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.GroupName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.BusinessHour).WithMany(p => p.Groups)
                .HasForeignKey(d => d.BusinessHourId)
                .HasConstraintName("FK__Groups__Business__74444068");

            entity.HasOne(d => d.GroupLeaderNavigation).WithMany(p => p.Groups)
                .HasForeignKey(d => d.GroupLeader)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Groups__GroupLea__01142BA1");
        });

        modelBuilder.Entity<Hash>(entity =>
        {
            entity.HasKey(e => new { e.Key, e.Field }).HasName("PK_HangFire_Hash");

            entity.ToTable("Hash", "HangFire");

            entity.HasIndex(e => e.ExpireAt, "IX_HangFire_Hash_ExpireAt").HasFilter("([ExpireAt] IS NOT NULL)");

            entity.Property(e => e.Key).HasMaxLength(100);
            entity.Property(e => e.Field).HasMaxLength(100);
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_HangFire_Job");

            entity.ToTable("Job", "HangFire");

            entity.HasIndex(e => e.ExpireAt, "IX_HangFire_Job_ExpireAt").HasFilter("([ExpireAt] IS NOT NULL)");

            entity.HasIndex(e => e.StateName, "IX_HangFire_Job_StateName").HasFilter("([StateName] IS NOT NULL)");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            entity.Property(e => e.StateName).HasMaxLength(20);
        });

        modelBuilder.Entity<JobParameter>(entity =>
        {
            entity.HasKey(e => new { e.JobId, e.Name }).HasName("PK_HangFire_JobParameter");

            entity.ToTable("JobParameter", "HangFire");

            entity.Property(e => e.Name).HasMaxLength(40);

            entity.HasOne(d => d.Job).WithMany(p => p.JobParameters)
                .HasForeignKey(d => d.JobId)
                .HasConstraintName("FK_HangFire_JobParameter_Job");
        });

        modelBuilder.Entity<JobQueue>(entity =>
        {
            entity.HasKey(e => new { e.Queue, e.Id }).HasName("PK_HangFire_JobQueue");

            entity.ToTable("JobQueue", "HangFire");

            entity.Property(e => e.Queue).HasMaxLength(50);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.FetchedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<List>(entity =>
        {
            entity.HasKey(e => new { e.Key, e.Id }).HasName("PK_HangFire_List");

            entity.ToTable("List", "HangFire");

            entity.HasIndex(e => e.ExpireAt, "IX_HangFire_List_ExpireAt").HasFilter("([ExpireAt] IS NOT NULL)");

            entity.Property(e => e.Key).HasMaxLength(100);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ExpireAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.ToTable("Notifications", "QuickServices");

            entity.Property(e => e.NotificationId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("NotificationID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.FromUserId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("FromUserID");
            entity.Property(e => e.NotificationBody).HasMaxLength(250);
            entity.Property(e => e.NotificationHeader).HasMaxLength(50);
            entity.Property(e => e.NotificationType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.RelateId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("RelateID");
            entity.Property(e => e.TargetUrl)
                .HasMaxLength(50)
                .HasColumnName("TargetURL");
            entity.Property(e => e.ToGroupId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ToGroupID");
            entity.Property(e => e.ToUserId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ToUserID");

            entity.HasOne(d => d.FromUser).WithMany(p => p.NotificationFromUsers)
                .HasForeignKey(d => d.FromUserId)
                .HasConstraintName("FK_Notification_UserFrom");

            entity.HasOne(d => d.Relate).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.RelateId)
                .HasConstraintName("FK_Notification_RelateTo");

            entity.HasOne(d => d.ToGroup).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.ToGroupId)
                .HasConstraintName("FK_Notification_GroupTo");

            entity.HasOne(d => d.ToUser).WithMany(p => p.NotificationToUsers)
                .HasForeignKey(d => d.ToUserId)
                .HasConstraintName("FK_Notification_UserTo");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("PK__Permissi__EFA6FB0FDE165077");

            entity.ToTable("Permissions", "QuickServices");

            entity.Property(e => e.PermissionId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PermissionID");
            entity.Property(e => e.PermissionName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Problem>(entity =>
        {
            entity.ToTable("Problems", "QuickServices");

            entity.Property(e => e.ProblemId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ProblemID");
            entity.Property(e => e.AssigneeId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("AssigneeID");
            entity.Property(e => e.AttachmentId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("AttachmentID");
            entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            entity.Property(e => e.GroupId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("GroupID");
            entity.Property(e => e.Impact)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Priority)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.RequesterId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("RequesterID");
            entity.Property(e => e.Slaid)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SLAID");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Title).HasMaxLength(250);

            entity.HasOne(d => d.Assignee).WithMany(p => p.ProblemAssignees)
                .HasForeignKey(d => d.AssigneeId)
                .HasConstraintName("FK_Problems_User");

            entity.HasOne(d => d.Attachment).WithMany(p => p.Problems)
                .HasForeignKey(d => d.AttachmentId)
                .HasConstraintName("FK_Problems_Attachments");

            entity.HasOne(d => d.Group).WithMany(p => p.Problems)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Problems_Group");

            entity.HasOne(d => d.Requester).WithMany(p => p.ProblemRequesters)
                .HasForeignKey(d => d.RequesterId)
                .HasConstraintName("FK__Problems__Reques__6A85CC04");

            entity.HasOne(d => d.Sla).WithMany(p => p.Problems)
                .HasForeignKey(d => d.Slaid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Problems__SLAID__314D4EA8");
        });

        modelBuilder.Entity<Query>(entity =>
        {
            entity.HasKey(e => e.QueryId).HasName("PK__Queries__5967F7FB6A725E8C");

            entity.ToTable("Queries", "QuickServices");

            entity.Property(e => e.QueryId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("QueryID");
            entity.Property(e => e.QueryName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.QueryStatement)
                .HasMaxLength(2000)
                .IsUnicode(false);
            entity.Property(e => e.QueryType)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Queries)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Queries__UserID__40C49C62");
        });

        modelBuilder.Entity<RequestTicket>(entity =>
        {
            entity.HasKey(e => e.RequestTicketId).HasName("PK__RequestT__99AAD2D7A5B8B93C");

            entity.ToTable("RequestTickets", "QuickServices");

            entity.Property(e => e.RequestTicketId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("RequestTicketID");
            entity.Property(e => e.AssignedTo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.AssignedToGroup)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.AttachmentId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("AttachmentID");
            entity.Property(e => e.ChangeId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ChangeID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.DueDate).HasColumnType("datetime");
            entity.Property(e => e.Impact)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.LastUpdateAt).HasColumnType("datetime");
            entity.Property(e => e.Priority)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ProblemId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ProblemID");
            entity.Property(e => e.RequesterId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("RequesterID");
            entity.Property(e => e.ResolvedTime).HasColumnType("datetime");
            entity.Property(e => e.ServiceItemId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ServiceItemID");
            entity.Property(e => e.Slaid)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SLAID");
            entity.Property(e => e.State)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Tags)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Title).HasMaxLength(500);
            entity.Property(e => e.Urgency)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.WorkflowId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("WorkflowID");

            entity.HasOne(d => d.AssignedToNavigation).WithMany(p => p.RequestTicketAssignedToNavigations)
                .HasForeignKey(d => d.AssignedTo)
                .HasConstraintName("FK__RequestTi__Assig__0E6E26BF");

            entity.HasOne(d => d.AssignedToGroupNavigation).WithMany(p => p.RequestTickets)
                .HasForeignKey(d => d.AssignedToGroup)
                .HasConstraintName("FK__RequestTi__Assig__0F624AF8");

            entity.HasOne(d => d.Attachment).WithMany(p => p.RequestTickets)
                .HasForeignKey(d => d.AttachmentId)
                .HasConstraintName("FK__RequestTi__Attac__114A936A");

            entity.HasOne(d => d.Change).WithMany(p => p.RequestTickets)
                .HasForeignKey(d => d.ChangeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_RequestTickets_Changes");

            entity.HasOne(d => d.Problem).WithMany(p => p.RequestTickets)
                .HasForeignKey(d => d.ProblemId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_RequestTickets_Problems");

            entity.HasOne(d => d.Requester).WithMany(p => p.RequestTicketRequesters)
                .HasForeignKey(d => d.RequesterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RequestTi__Reque__0C85DE4D");

            entity.HasOne(d => d.ServiceItem).WithMany(p => p.RequestTickets)
                .HasForeignKey(d => d.ServiceItemId)
                .HasConstraintName("FK__RequestTi__Servi__65370702");

            entity.HasOne(d => d.Sla).WithMany(p => p.RequestTickets)
                .HasForeignKey(d => d.Slaid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RequestTi__SLAID__10566F31");

            entity.HasOne(d => d.Workflow).WithMany(p => p.RequestTickets)
                .HasForeignKey(d => d.WorkflowId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_RequestTickets_Workflows");
        });

        modelBuilder.Entity<RequestTicketExt>(entity =>
        {
            entity.HasKey(e => e.RequestTicketExId).HasName("PK__RequestT__83D6874DD45B3BDF");

            entity.ToTable("RequestTicketExt", "QuickServices");

            entity.Property(e => e.RequestTicketExId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("RequestTicketExID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.FieldId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.FieldValue).HasMaxLength(2000);
            entity.Property(e => e.TicketId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Field).WithMany(p => p.RequestTicketExts)
                .HasForeignKey(d => d.FieldId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RequestTi__Field__56E8E7AB");

            entity.HasOne(d => d.Ticket).WithMany(p => p.RequestTicketExts)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RequestTi__Ticke__55F4C372");
        });

        modelBuilder.Entity<RequestTicketHistory>(entity =>
        {
            entity.HasKey(e => e.RequestTicketHistoryId).HasName("PK__RequestT__58D8082550D4C07F");

            entity.ToTable("RequestTicketHistories", "QuickServices");

            entity.Property(e => e.RequestTicketHistoryId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("RequestTicketHistoryID");
            entity.Property(e => e.ChangeId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ChangeID");
            entity.Property(e => e.Content)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LastUpdate).HasColumnType("datetime");
            entity.Property(e => e.ProblemId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ProblemID");
            entity.Property(e => e.RequestTicketId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("RequestTicketID");
            entity.Property(e => e.UserId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("UserID");

            entity.HasOne(d => d.Change).WithMany(p => p.RequestTicketHistories)
                .HasForeignKey(d => d.ChangeId)
                .HasConstraintName("FK__RequestTi__Chang__52AE4273");

            entity.HasOne(d => d.Problem).WithMany(p => p.RequestTicketHistories)
                .HasForeignKey(d => d.ProblemId)
                .HasConstraintName("FK__RequestTi__Probl__53A266AC");

            entity.HasOne(d => d.RequestTicket).WithMany(p => p.RequestTicketHistories)
                .HasForeignKey(d => d.RequestTicketId)
                .HasConstraintName("FK__RequestTi__Reque__3A179ED3");

            entity.HasOne(d => d.User).WithMany(p => p.RequestTicketHistories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RequestTi__UserI__3B0BC30C");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3A0C8CECFE");

            entity.ToTable("Roles", "QuickServices");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B61601C86A472").IsUnique();

            entity.Property(e => e.RoleId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("RoleID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RoleType)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasMany(d => d.Permissions).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "RolePermission",
                    r => r.HasOne<Permission>().WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__RolePermi__Permi__6383C8BA"),
                    l => l.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__RolePermi__RoleI__628FA481"),
                    j =>
                    {
                        j.HasKey("RoleId", "PermissionId").HasName("PK__RolePerm__6400A18A8DF39A79");
                        j.ToTable("RolePermissions", "QuickServices");
                        j.IndexerProperty<string>("RoleId")
                            .HasMaxLength(10)
                            .IsUnicode(false)
                            .IsFixedLength()
                            .HasColumnName("RoleID");
                        j.IndexerProperty<string>("PermissionId")
                            .HasMaxLength(10)
                            .IsUnicode(false)
                            .IsFixedLength()
                            .HasColumnName("PermissionID");
                    });
        });

        modelBuilder.Entity<Schema>(entity =>
        {
            entity.HasKey(e => e.Version).HasName("PK_HangFire_Schema");

            entity.ToTable("Schema", "HangFire");

            entity.Property(e => e.Version).ValueGeneratedNever();
        });

        modelBuilder.Entity<Server>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_HangFire_Server");

            entity.ToTable("Server", "HangFire");

            entity.HasIndex(e => e.LastHeartbeat, "IX_HangFire_Server_LastHeartbeat");

            entity.Property(e => e.Id).HasMaxLength(200);
            entity.Property(e => e.LastHeartbeat).HasColumnType("datetime");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Services__C51BB0EA8118D865");

            entity.ToTable("Services", "QuickServices");

            entity.Property(e => e.ServiceId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ServiceID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.HealthStatus)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Impact)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ManagedBy)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ManagedByGroup)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ServiceName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ServiceTypeId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ServiceTypeID");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ServiceCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Services__Create__2B0A656D");

            entity.HasOne(d => d.ManagedByNavigation).WithMany(p => p.ServiceManagedByNavigations)
                .HasForeignKey(d => d.ManagedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Services__Manage__2CF2ADDF");

            entity.HasOne(d => d.ManagedByGroupNavigation).WithMany(p => p.Services)
                .HasForeignKey(d => d.ManagedByGroup)
                .HasConstraintName("FK__Services__Manage__2DE6D218");

            entity.HasOne(d => d.ServiceType).WithMany(p => p.Services)
                .HasForeignKey(d => d.ServiceTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Services__Servic__2BFE89A6");
        });

        modelBuilder.Entity<ServiceCategory>(entity =>
        {
            entity.HasKey(e => e.ServiceCategoryId).HasName("PK__ServiceC__E4CC7E8AB5603753");

            entity.ToTable("ServiceCategories", "QuickServices");

            entity.Property(e => e.ServiceCategoryId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ServiceCategoryID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ServiceCategoryName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ServiceDeskHour>(entity =>
        {
            entity.HasKey(e => e.ServiceDeskHourId).HasName("PK__ServiceD__ED3A85AF1325A40B");

            entity.ToTable("ServiceDeskHours", "QuickServices");

            entity.Property(e => e.ServiceDeskHourId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ServiceDeskHourID");
            entity.Property(e => e.BusinessHourId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("BusinessHourID");
            entity.Property(e => e.DayOfWeek)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TimeEnd).HasColumnType("date");
            entity.Property(e => e.TimeStart).HasColumnType("date");

            entity.HasOne(d => d.BusinessHour).WithMany(p => p.ServiceDeskHours)
                .HasForeignKey(d => d.BusinessHourId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ServiceDe__Busin__6A30C649");
        });

        modelBuilder.Entity<ServiceItem>(entity =>
        {
            entity.HasKey(e => e.ServiceItemId).HasName("PK__ServiceI__CC153FD87A2190F8");

            entity.ToTable("ServiceItems", "QuickServices");

            entity.Property(e => e.ServiceItemId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ServiceItemID");
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.IconDisplay)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ServiceCategoryId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ServiceCategoryID");
            entity.Property(e => e.ServiceItemName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ShortDescription)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Slaid)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SLAID");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.WorkflowId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("WorkflowID");

            entity.HasOne(d => d.ServiceCategory).WithMany(p => p.ServiceItems)
                .HasForeignKey(d => d.ServiceCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ServiceIt__Servi__08B54D69");

            entity.HasOne(d => d.Sla).WithMany(p => p.ServiceItems)
                .HasForeignKey(d => d.Slaid)
                .HasConstraintName("FK__ServiceIt__SLAID__61316BF4");

            entity.HasOne(d => d.Workflow).WithMany(p => p.ServiceItems)
                .HasForeignKey(d => d.WorkflowId)
                .HasConstraintName("FK_ServiceItems_Workflows");
        });

        modelBuilder.Entity<ServiceItemCustomField>(entity =>
        {
            entity.HasKey(e => new { e.ServiceItemId, e.CustomFieldId }).HasName("PK__ServiceI__98160DB56ED16F07");

            entity.ToTable("ServiceItemCustomFields", "QuickServices");

            entity.Property(e => e.ServiceItemId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ServiceItemID");
            entity.Property(e => e.CustomFieldId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CustomFieldID");
            entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            entity.Property(e => e.Mandatory).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.CustomField).WithMany(p => p.ServiceItemCustomFields)
                .HasForeignKey(d => d.CustomFieldId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ServiceIt__Custo__6442E2C9");

            entity.HasOne(d => d.ServiceItem).WithMany(p => p.ServiceItemCustomFields)
                .HasForeignKey(d => d.ServiceItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ServiceIt__Servi__634EBE90");
        });

        modelBuilder.Entity<ServiceType>(entity =>
        {
            entity.HasKey(e => e.ServiceTypeId).HasName("PK__ServiceT__8ADFAA0C80E089A7");

            entity.ToTable("ServiceTypes", "QuickServices");

            entity.Property(e => e.ServiceTypeId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ServiceTypeID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ServiceTypeName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Set>(entity =>
        {
            entity.HasKey(e => new { e.Key, e.Value }).HasName("PK_HangFire_Set");

            entity.ToTable("Set", "HangFire");

            entity.HasIndex(e => e.ExpireAt, "IX_HangFire_Set_ExpireAt").HasFilter("([ExpireAt] IS NOT NULL)");

            entity.HasIndex(e => new { e.Key, e.Score }, "IX_HangFire_Set_Score");

            entity.Property(e => e.Key).HasMaxLength(100);
            entity.Property(e => e.Value).HasMaxLength(256);
            entity.Property(e => e.ExpireAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Sla>(entity =>
        {
            entity.HasKey(e => e.Slaid).HasName("PK__SLAs__2848A22937332E34");

            entity.ToTable("SLAs", "QuickServices");

            entity.Property(e => e.Slaid)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SLAID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.IsDefault).HasColumnName("isDefault");
            entity.Property(e => e.Slaname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("SLAName");
        });

        modelBuilder.Entity<Slametric>(entity =>
        {
            entity.HasKey(e => e.SlametricId).HasName("PK__tmp_ms_x__D08D303D95EFD6C7");

            entity.ToTable("SLAMetrics", "QuickServices");

            entity.Property(e => e.SlametricId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SLAMetricID");
            entity.Property(e => e.BusinessHourId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("BusinessHourID");
            entity.Property(e => e.EscalationPolicy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NotificationRules)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Priority)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Slaid)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SLAID");

            entity.HasOne(d => d.BusinessHour).WithMany(p => p.Slametrics)
                .HasForeignKey(d => d.BusinessHourId)
                .HasConstraintName("FK__SLAMetric__Busin__6DCC4D03");

            entity.HasOne(d => d.Sla).WithMany(p => p.Slametrics)
                .HasForeignKey(d => d.Slaid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SLAMetric__SLAID__6CD828CA");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => new { e.JobId, e.Id }).HasName("PK_HangFire_State");

            entity.ToTable("State", "HangFire");

            entity.HasIndex(e => e.CreatedAt, "IX_HangFire_State_CreatedAt");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.Reason).HasMaxLength(100);

            entity.HasOne(d => d.Job).WithMany(p => p.States)
                .HasForeignKey(d => d.JobId)
                .HasConstraintName("FK_HangFire_State_Job");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC0BE873DE");

            entity.ToTable("Users", "QuickServices");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534C14DDDDE").IsUnique();

            entity.Property(e => e.UserId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("UserID");
            entity.Property(e => e.Avatar)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.BirthDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            entity.Property(e => e.Department)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.JobTitle)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MiddleName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PersonalEmail)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.RoleId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("RoleID");
            entity.Property(e => e.WallPaper)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__RoleID__40F9A68C");

            entity.HasMany(d => d.GroupsNavigation).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserGroup",
                    r => r.HasOne<Group>().WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserGroup__Group__05D8E0BE"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserGroup__UserI__04E4BC85"),
                    j =>
                    {
                        j.HasKey("UserId", "GroupId").HasName("PK__UserGrou__A6C1639C797C87F4");
                        j.ToTable("UserGroups", "QuickServices");
                        j.IndexerProperty<string>("UserId")
                            .HasMaxLength(10)
                            .IsUnicode(false)
                            .IsFixedLength()
                            .HasColumnName("UserID");
                        j.IndexerProperty<string>("GroupId")
                            .HasMaxLength(10)
                            .IsUnicode(false)
                            .IsFixedLength()
                            .HasColumnName("GroupID");
                    });
        });

        modelBuilder.Entity<Workflow>(entity =>
        {
            entity.HasKey(e => e.WorkflowId).HasName("PK__tmp_ms_x__5704A64A7A77662F");

            entity.ToTable("Workflows", "QuickServices");

            entity.Property(e => e.WorkflowId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("WorkflowID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.LastUpdate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.WorkflowName).HasMaxLength(255);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Workflows)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Workflows__Creat__68D28DBC");
        });

        modelBuilder.Entity<WorkflowAssignment>(entity =>
        {
            entity.ToTable("WorkflowAssignments", "QuickServices");

            entity.Property(e => e.WorkflowAssignmentId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("WorkflowAssignmentID");
            entity.Property(e => e.AssigneeId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("AssigneeID");
            entity.Property(e => e.AttachmentId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("AttachmentID");
            entity.Property(e => e.CompletedTime).HasColumnType("datetime");
            entity.Property(e => e.CurrentTaskId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CurrentTaskID");
            entity.Property(e => e.DueDate).HasColumnType("datetime");
            entity.Property(e => e.HandleTime).HasColumnType("datetime");
            entity.Property(e => e.Message).HasMaxLength(255);
            entity.Property(e => e.ReferenceId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ReferenceID");

            entity.HasOne(d => d.Assignee).WithMany(p => p.WorkflowAssignments)
                .HasForeignKey(d => d.AssigneeId)
                .HasConstraintName("FK_WorkflowAssignments_User");

            entity.HasOne(d => d.Attachment).WithMany(p => p.WorkflowAssignments)
                .HasForeignKey(d => d.AttachmentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_WorkflowAssignments_Attachments");

            entity.HasOne(d => d.CurrentTask).WithMany(p => p.WorkflowAssignments)
                .HasForeignKey(d => d.CurrentTaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__WorkflowA__Curre__7073AF84");

            entity.HasOne(d => d.Reference).WithMany(p => p.WorkflowAssignments)
                .HasForeignKey(d => d.ReferenceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__WorkflowA__Refer__7167D3BD");
        });

        modelBuilder.Entity<WorkflowTask>(entity =>
        {
            entity.HasKey(e => e.WorkflowTaskId).HasName("PK__Workflow__36121401BF6110DD");

            entity.ToTable("WorkflowTasks", "QuickServices");

            entity.Property(e => e.WorkflowTaskId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("WorkflowTaskID");
            entity.Property(e => e.AssignerId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("AssignerID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.GroupId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("GroupID");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.WorkflowId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("WorkflowID");
            entity.Property(e => e.WorkflowTaskName).HasMaxLength(255);

            entity.HasOne(d => d.Assigner).WithMany(p => p.WorkflowTasks)
                .HasForeignKey(d => d.AssignerId)
                .HasConstraintName("FK_WorkflowTasks_Users");

            entity.HasOne(d => d.Group).WithMany(p => p.WorkflowTasks)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK_WorkflowTasks_Groups");

            entity.HasOne(d => d.Workflow).WithMany(p => p.WorkflowTasks)
                .HasForeignKey(d => d.WorkflowId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkflowSteps_Workflow");
        });

        modelBuilder.Entity<WorkflowTransition>(entity =>
        {
            entity.HasKey(e => new { e.FromWorkflowTask, e.ToWorkflowTask });

            entity.ToTable("WorkflowTransitions", "QuickServices");

            entity.Property(e => e.FromWorkflowTask)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ToWorkflowTask)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.WorkflowTransitionName).HasMaxLength(50);

            entity.HasOne(d => d.FromWorkflowTaskNavigation).WithMany(p => p.WorkflowTransitionFromWorkflowTaskNavigations)
                .HasForeignKey(d => d.FromWorkflowTask)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkflowTransitions_WorkflowTasksFrom");

            entity.HasOne(d => d.ToWorkflowTaskNavigation).WithMany(p => p.WorkflowTransitionToWorkflowTaskNavigations)
                .HasForeignKey(d => d.ToWorkflowTask)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkflowTransitions_WorkflowTasksTo");
        });

        modelBuilder.Entity<YearlyHolidayList>(entity =>
        {
            entity.HasKey(e => new { e.Holiday, e.BusinessHourId }).HasName("PK__YearlyHo__026314D352E863BC");

            entity.ToTable("YearlyHolidayLists", "QuickServices");

            entity.HasIndex(e => e.HolidayName, "UQ__YearlyHo__38021A2B09ACF220").IsUnique();

            entity.Property(e => e.Holiday).HasColumnType("date");
            entity.Property(e => e.BusinessHourId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("BusinessHourID");
            entity.Property(e => e.HolidayName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.BusinessHour).WithMany(p => p.YearlyHolidayLists)
                .HasForeignKey(d => d.BusinessHourId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__YearlyHol__Busin__3493CFA7");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
