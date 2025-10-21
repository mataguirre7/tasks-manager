using API.Domain;
using API.Domain.Boards;
using API.Domain.Columns;
using API.Domain.Comments;
using API.Domain.Extended;
using API.Domain.Items;
using API.Domain.Workspaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.EntityFrameworkCore
{
    public class TasksDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Column> Columns { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Workspace> Workspaces { get; set; }

        public TasksDbContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Configure your own tables/entities inside here */

            /* Application User */

            builder.Entity<ApplicationUser>(a =>
            {
                a.ToTable(TasksConsts.DbTablePrefix + "Users", TasksConsts.DbSchema);
                a.HasKey(a => a.Id);
                a.Property(a => a.FullName).IsRequired();
                a.Property(a => a.BirthDate).IsRequired();
                a.Property(a => a.Email).IsRequired();
                a.Property(a => a.NormalizedFullName).IsRequired(false);
            });

            builder.Entity<Item>(t =>
            {
                t.ToTable(TasksConsts.DbTablePrefix + "Items", TasksConsts.DbSchema);
                t.HasKey(p => p.Id);
                t.Property(p => p.Title).IsRequired();
                t.Property(p => p.Description).IsRequired();
                t.Property(p => p.DueDate).IsRequired();
                t.Property(p => p.Status).IsRequired();

                t.HasOne(p => p.AssignedUser)
                    .WithMany()
                    .HasForeignKey(p => p.AssignedUserId)
                    .IsRequired(false);

                t.HasOne(p => p.Column)
                    .WithMany(pr => pr.Items)
                    .HasForeignKey(p => p.ColumnId)
                    .IsRequired();
            });

            builder.Entity<Board>(p =>
            {
                p.ToTable(TasksConsts.DbTablePrefix + "Boards", TasksConsts.DbSchema);
                p.HasKey(p => p.Id);
                p.Property(p => p.Name).IsRequired();
                p.Property(p => p.Description).IsRequired();

                p.HasMany(p => p.Columns)
                    .WithOne(p => p.Board)
                    .HasForeignKey(p => p.BoardId);

                p.HasOne(p => p.User)
                    .WithMany()
                    .HasForeignKey(p => p.UserId)
                    .IsRequired();
            });

            builder.Entity<Comment>(c =>
            {
                c.ToTable(TasksConsts.DbTablePrefix + "Comments", TasksConsts.DbSchema);
                c.HasKey(p => p.Id);
                c.Property(p => p.Content).IsRequired();

                c.HasOne(p => p.Item)
                    .WithMany(t => t.Comments)
                    .HasForeignKey(p => p.TaskId)
                    .IsRequired();

                c.HasOne(p => p.Author)
                    .WithMany()
                    .HasForeignKey(p => p.AuthorId)
                    .IsRequired();
            });

            builder.Entity<Column>(c =>
            {
                c.ToTable(TasksConsts.DbTablePrefix + "Columns", TasksConsts.DbSchema);
                c.HasKey(p => p.Id);
                c.Property(p => p.Status).IsRequired();
                c.HasMany(p => p.Items).WithOne(p => p.Column).HasForeignKey(p => p.ColumnId);
                c.HasOne(p => p.Board).WithMany(p => p.Columns).HasForeignKey(p => p.BoardId);
            });

            builder.Entity<Workspace>(w =>
            {
                w.ToTable(TasksConsts.DbTablePrefix + "Workspaces", TasksConsts.DbSchema);
                w.HasKey(p => p.Id);
                w.Property(p => p.Name).IsRequired();
                w.Property(p => p.Description).IsRequired(false);
                w.HasMany(p => p.WorkspaceMembers)
                   .WithMany()
                   .UsingEntity<WorkspaceUser>(
                       pu => pu.HasOne(pu => pu.User).WithMany(),
                       pu => pu.HasOne(pu => pu.Workspace).WithMany(),
                       pu =>
                       {
                           pu.ToTable(TasksConsts.DbTablePrefix + "Workspaces_Users", TasksConsts.DbSchema);
                           pu.HasKey(pu => new { pu.WorkspaceId, pu.UserId });
                       });
            });
        }
    }
}