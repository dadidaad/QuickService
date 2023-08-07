using System;
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

    public virtual DbSet<Asset> Assets { get; set; }

    public virtual DbSet<AssetAssignment> AssetAssignments { get; set; }

    public virtual DbSet<AssetHistory> AssetHistories { get; set; }

    public virtual DbSet<Attachment> Attachments { get; set; }

    public virtual DbSet<BusinessHour> BusinessHours { get; set; }

    public virtual DbSet<Change> Changes { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<CustomField> CustomFields { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Problem> Problems { get; set; }

    public virtual DbSet<RequestTicket> RequestTickets { get; set; }

    public virtual DbSet<RequestTicketExt> RequestTicketExts { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServiceCategory> ServiceCategories { get; set; }

    public virtual DbSet<ServiceDeskHour> ServiceDeskHours { get; set; }

    public virtual DbSet<ServiceItem> ServiceItems { get; set; }

    public virtual DbSet<ServiceItemCustomField> ServiceItemCustomFields { get; set; }

    public virtual DbSet<ServiceType> ServiceTypes { get; set; }

    public virtual DbSet<Sla> Slas { get; set; }

    public virtual DbSet<Slametric> Slametrics { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Workflow> Workflows { get; set; }

    public virtual DbSet<WorkflowAssignment> WorkflowAssignments { get; set; }

    public virtual DbSet<WorkflowStep> WorkflowSteps { get; set; }

    public virtual DbSet<YearlyHolidayList> YearlyHolidayLists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=tcp:quick-service.database.windows.net,1433; database=Quick Service;uid=quickservice;pwd=admin123!;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
            entity.Property(e => e.AssignerId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("AssignerID");
            entity.Property(e => e.AttachmentId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("AttachmentID");
            entity.Property(e => e.BackoutPlan).IsUnicode(false);
            entity.Property(e => e.ChangeType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            entity.Property(e => e.GroupId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("GroupID");
            entity.Property(e => e.Impact).HasMaxLength(20);
            entity.Property(e => e.IsApprovedByCab).HasColumnName("IsApprovedByCAB");
            entity.Property(e => e.PlannedEndDate).HasColumnType("datetime");
            entity.Property(e => e.PlannedStartDate).HasColumnType("datetime");
            entity.Property(e => e.Priority).HasMaxLength(20);
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
            entity.Property(e => e.Risk).HasMaxLength(20);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Title).HasMaxLength(250);

            entity.HasOne(d => d.Assigner).WithMany(p => p.ChangeAssigners)
                .HasForeignKey(d => d.AssignerId)
                .HasConstraintName("FK_Changes_Users");

            entity.HasOne(d => d.Attachment).WithMany(p => p.Changes)
                .HasForeignKey(d => d.AttachmentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Changes_Attachments");

            entity.HasOne(d => d.Group).WithMany(p => p.Changes)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Changes_Group");

            entity.HasOne(d => d.Problem).WithMany(p => p.Changes)
                .HasForeignKey(d => d.ProblemId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Changes_Problems");

            entity.HasOne(d => d.Requester).WithMany(p => p.ChangeRequesters)
                .HasForeignKey(d => d.RequesterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Changes_User");
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
            entity.Property(e => e.CommentBy)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CommentText).HasMaxLength(1000);
            entity.Property(e => e.CommentTime).HasColumnType("datetime");
            entity.Property(e => e.LastModified).HasColumnType("datetime");
            entity.Property(e => e.RequestTicketId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("RequestTicketID");

            entity.HasOne(d => d.Attachment).WithMany(p => p.Comments)
                .HasForeignKey(d => d.AttachmentId)
                .HasConstraintName("FK__Comments__Attach__19DFD96B");

            entity.HasOne(d => d.CommentByNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.CommentBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comments__Commen__17F790F9");

            entity.HasOne(d => d.RequestTicket).WithMany(p => p.Comments)
                .HasForeignKey(d => d.RequestTicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comments__Reques__18EBB532");
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
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Groups__Business__02084FDA");

            entity.HasOne(d => d.GroupLeaderNavigation).WithMany(p => p.Groups)
                .HasForeignKey(d => d.GroupLeader)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Groups__GroupLea__01142BA1");
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
            entity.Property(e => e.AssignerId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("AssignerID");
            entity.Property(e => e.AttachmentId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("AttachmentID");
            entity.Property(e => e.DueTime).HasColumnType("datetime");
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
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Title).HasMaxLength(250);

            entity.HasOne(d => d.Assigner).WithMany(p => p.ProblemAssigners)
                .HasForeignKey(d => d.AssignerId)
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
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Problems_User2");
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
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .IsUnicode(false);
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
                .HasMaxLength(10)
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
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.ServiceCategory).WithMany(p => p.ServiceItems)
                .HasForeignKey(d => d.ServiceCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ServiceIt__Servi__08B54D69");
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
            entity.Property(e => e.ServiceItemId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Slaname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("SLAName");

            entity.HasOne(d => d.ServiceItem).WithMany(p => p.Slas)
                .HasForeignKey(d => d.ServiceItemId)
                .HasConstraintName("FK_SLAs_ServiceItems");
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
            entity.Property(e => e.Piority)
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
            entity.HasKey(e => e.WorkflowId).HasName("PK__Workflow__5704A64AA4E82F20");

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
            entity.Property(e => e.LastUpdate).HasColumnType("datetime");
            entity.Property(e => e.ReferenceId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ReferenceID");
            entity.Property(e => e.Slaid)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SLAID");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.WorkflowName)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Workflows)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Workflows__Creat__1CBC4616");

            entity.HasOne(d => d.Reference).WithMany(p => p.Workflows)
                .HasForeignKey(d => d.ReferenceId)
                .HasConstraintName("FK_Workflows_ServiceItems");

            entity.HasOne(d => d.Sla).WithMany(p => p.Workflows)
                .HasForeignKey(d => d.Slaid)
                .HasConstraintName("FK_Workflows_SLAs");
        });

        modelBuilder.Entity<WorkflowAssignment>(entity =>
        {
            entity.HasKey(e => new { e.ReferenceId, e.WorkflowId, e.CurrentStepId });

            entity.ToTable("WorkflowAssignments", "QuickServices");

            entity.Property(e => e.ReferenceId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ReferenceID");
            entity.Property(e => e.WorkflowId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("WorkflowID");
            entity.Property(e => e.CurrentStepId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CurrentStepID");
            entity.Property(e => e.AttachmentId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("AttachmentID");
            entity.Property(e => e.CompletedTime).HasColumnType("datetime");
            entity.Property(e => e.DueDate).HasColumnType("datetime");
            entity.Property(e => e.RejectReason).HasMaxLength(255);

            entity.HasOne(d => d.Attachment).WithMany(p => p.WorkflowAssignments)
                .HasForeignKey(d => d.AttachmentId)
                .HasConstraintName("FK_WorkflowAssignments_Attachments");

            entity.HasOne(d => d.CurrentStep).WithMany(p => p.WorkflowAssignments)
                .HasForeignKey(d => d.CurrentStepId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__WorkflowA__Curre__1B9317B3");

            entity.HasOne(d => d.Reference).WithMany(p => p.WorkflowAssignments)
                .HasForeignKey(d => d.ReferenceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkflowAssignments_Changes");

            entity.HasOne(d => d.ReferenceNavigation).WithMany(p => p.WorkflowAssignments)
                .HasForeignKey(d => d.ReferenceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkflowAssignments_Problems");

            entity.HasOne(d => d.Reference1).WithMany(p => p.WorkflowAssignments)
                .HasForeignKey(d => d.ReferenceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__WorkflowA__Refer__18B6AB08");

            entity.HasOne(d => d.Workflow).WithMany(p => p.WorkflowAssignments)
                .HasForeignKey(d => d.WorkflowId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__WorkflowA__Workf__1A9EF37A");
        });

        modelBuilder.Entity<WorkflowStep>(entity =>
        {
            entity.HasKey(e => e.WorkflowStepId).HasName("PK__Workflow__36121401BF6110DD");

            entity.ToTable("WorkflowSteps", "QuickServices");

            entity.Property(e => e.WorkflowStepId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("WorkflowStepID");
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
            entity.Property(e => e.WorkflowStepName).HasMaxLength(255);

            entity.HasOne(d => d.Workflow).WithMany(p => p.WorkflowSteps)
                .HasForeignKey(d => d.WorkflowId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkflowSteps_Workflow");
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
