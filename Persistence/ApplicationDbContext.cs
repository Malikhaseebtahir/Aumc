using Aumc.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Aumc.Persistence
{
    public class ApplicationDbContext: IdentityDbContext<User, Role, int, 
        IdentityUserClaim<int>, UserRole, 
        IdentityUserLogin<int>, IdentityRoleClaim<int>, 
        IdentityUserToken<int>>
    {
        public DbSet<Like> Likes { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<BlockUser> BlockUsers { get; set; }
        public DbSet<GroupType> GroupTypes { get; set; }
        public DbSet<Following> Followings { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<UserReport> UserReports { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<StudentGroup> StudentGroups { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<GroupNewsLetter> GroupNewsLetters { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<StudentGroupPost> StudentGroupPosts { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<UniversityNews> UniversityNews { get; set; }
        public DbSet<StudentGroupComment> StudentGroupComments { get; set; }
        public DbSet<StudentGroupAttendance> StudentGroupAttendances { get; set; }
        public DbSet<StudentGroupNewsLetter> StudentGroupNewsLetters { get; set; }
        public DbSet<StudentGroupPostComment> StudentGroupPostComments { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(userRole => {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<Photo>()
                .Property(p => p.Url)
                .IsRequired();
                
            // Own entity type
            modelBuilder.Entity<User>()
                .OwnsOne(u => u.Address);
            
            // User Table
            modelBuilder.Entity<User>()
                .Property(u => u.UserName)
                .HasMaxLength(128)
                .IsRequired();

            // ReportUser Table
            modelBuilder.Entity<UserReport>()
                .HasOne(ru => ru.Reportee)
                .WithMany(u => u.Reporters)
                .HasForeignKey(ru => ru.ReporteeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserReport>()
                .HasOne(ru => ru.Reporter)
                .WithMany(u => u.Reportees)
                .HasForeignKey(ru => ru.ReporterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserReport>()
                .Property(m => m.Message)
                .IsRequired();

            modelBuilder.Entity<UserReport>()
                .Property(m => m.ReporteeId)
                .IsRequired();

            modelBuilder.Entity<UserReport>()
                .Property(m => m.ReporterId)
                .IsRequired();

            // Interest Table
            modelBuilder.Entity<Interest>()
                .Property(i => i.Id)
                .ValueGeneratedOnAdd();

            // Adrress Table
            modelBuilder.Entity<Address>()
                .Property(a => a.City)
                .HasColumnName("City")
                .HasMaxLength(128)
                .IsRequired();

            modelBuilder.Entity<Address>()
                .Property(a => a.Province)
                .HasColumnName("Province")
                .HasMaxLength(128)
                .IsRequired();

            modelBuilder.Entity<Address>()
                .Property(a => a.Country)
                .HasColumnName("Country")
                .HasMaxLength(128)
                .IsRequired();

            // Like Table
            modelBuilder.Entity<Like>()
                .HasKey(l => new { l.LikerId, l.LikeeId });

            modelBuilder.Entity<Like>()
                .HasOne(u => u.Likee)
                .WithMany(u => u.Liker)
                .HasForeignKey(u => u.LikerId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Like>()
                .HasOne(u => u.Liker)
                .WithMany(u => u.Likee)
                .HasForeignKey(u => u.LikeeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Messaged Table
            modelBuilder.Entity<Message>()
                .HasOne(u => u.Sender)
                .WithMany(m => m.MessageSent)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(u => u.Recipient)
                .WithMany(m => m.MessageReceived)
                .OnDelete(DeleteBehavior.Restrict);

            // Group Table
            modelBuilder.Entity<Group>()
                .Property(g => g.ClassName)
                .HasMaxLength(128)
                .IsRequired();

            modelBuilder.Entity<Group>()
                .Property(g => g.Section)
                .HasMaxLength(128)
                .IsRequired();

            modelBuilder.Entity<Group>()
                .Property(g => g.Subject)
                .HasMaxLength(128)
                .IsRequired();

            modelBuilder.Entity<Group>()
                .Property(g => g.Description)
                .IsRequired();

            modelBuilder.Entity<Group>()
                .Property(g => g.Room)
                .IsRequired();

            modelBuilder.Entity<Group>()
                .Property(g => g.GroupTypeId)
                .IsRequired();

            modelBuilder.Entity<Group>()
                .Property(g => g.UserId)
                .IsRequired();

            // Group Type Table
            modelBuilder.Entity<GroupType>()
                .Property(g => g.Name)
                .HasMaxLength(128);

            // Post Table
            modelBuilder.Entity<Post>()
                .Property(p => p.Title)
                .HasMaxLength(128)
                .IsRequired();

            modelBuilder.Entity<Post>()
                .Property(p => p.PostCategoryId)
                .IsRequired();
            
            modelBuilder.Entity<Post>()
                .Property(p => p.Description)
                .IsRequired();
            
            // Category Table
            modelBuilder.Entity<PostCategory>()
                .Property(pc => pc.Name)
                .HasMaxLength(128)
                .IsRequired();

            // Attendance
            modelBuilder.Entity<Attendance>()
                .HasKey(a => new { a.UserId, a.GroupId });
            
            modelBuilder.Entity<Attendance>()
                .HasOne(g => g.Group)
                .WithMany(g => g.Attendances)
                .OnDelete(DeleteBehavior.Restrict);

            // Following
            modelBuilder.Entity<Following>()
                .HasKey(f => new { f.FolloweeId, f.FollowerId });

            modelBuilder.Entity<User>()
                .HasMany(u => u.Followers)
                .WithOne(u => u.Followee)
                .HasForeignKey(u => u.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<User>() 
                .HasMany(u => u.Followees)
                .WithOne(u => u.Follower)
                .HasForeignKey(u => u.FolloweeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Block user Table
            modelBuilder.Entity<BlockUser>()
                .Property(bu => bu.ClassName)
                .IsRequired();

            modelBuilder.Entity<BlockUser>()
                .Property(bu => bu.UserId)
                .IsRequired();

            modelBuilder.Entity<BlockUser>()
                .Property(bu => bu.Message)
                .IsRequired();

            modelBuilder.Entity<BlockUser>()
                .Property(bu => bu.TeacherOrStudent)
                .IsRequired();

            // Comments
            modelBuilder.Entity<Comment>()
                .Property(c => c.Message)
                .IsRequired();
            
            modelBuilder.Entity<Comment>()
                .Property(c => c.UserId)
                .IsRequired();
            
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Group)
                .WithMany(g => g.Comments);

            // UserNotification Table 
            modelBuilder.Entity<UserNotification>()
                .HasKey(un => new { un.UserId, un.NotificationId});
            
            modelBuilder.Entity<UserNotification>()
                .HasOne(n => n.User)
                .WithMany(u => u.UserNotifications)
                .OnDelete(DeleteBehavior.Restrict);

            // StudentGroup
            modelBuilder.Entity<StudentGroup>()
                .Property(g => g.Subject)
                .HasMaxLength(128)
                .IsRequired();

            modelBuilder.Entity<StudentGroup>()
                .Property(g => g.Description)
                .HasMaxLength(256)
                .IsRequired();

            modelBuilder.Entity<StudentGroup>()
                .Property(g => g.UserId)
                .IsRequired();

            // StudentGroupNewsLetter
            modelBuilder.Entity<StudentGroupNewsLetter>()
                .Property(n => n.Title)
                .HasMaxLength(128)
                .IsRequired();

            modelBuilder.Entity<StudentGroupNewsLetter>()
                .Property(n => n.Description)
                .IsRequired();

            modelBuilder.Entity<StudentGroupNewsLetter>()
                .Property(n => n.GroupId)
                .IsRequired();

            // StudentGroupPost
            modelBuilder.Entity<StudentGroupPost>()
                .Property(p => p.Title)
                .HasMaxLength(128)
                .IsRequired();

            modelBuilder.Entity<StudentGroupPost>()
                .Property(p => p.Description)
                .IsRequired();

            modelBuilder.Entity<StudentGroupPost>()
                .Property(p => p.UserId)
                .IsRequired();

            modelBuilder.Entity<StudentGroupPost>()
                .Property(p => p.GroupId)
                .IsRequired();

            modelBuilder.Entity<StudentGroupPost>()
                .HasOne(p => p.Group)
                .WithMany(p => p.StudentGroupPosts)
                .OnDelete(DeleteBehavior.Restrict);

            // StudentGroupAttendace
            modelBuilder.Entity<StudentGroupAttendance>()
                .HasKey(a => new { a.UserId, a.GroupId });
            
            modelBuilder.Entity<StudentGroupAttendance>()
                .HasOne(g => g.Group)
                .WithMany(g => g.StudentGroupAttendances)
                .OnDelete(DeleteBehavior.Restrict);

            // GroupNewsLetter
            modelBuilder.Entity<GroupNewsLetter>()
                .Property(n => n.Title)
                .HasMaxLength(128)
                .IsRequired();

            modelBuilder.Entity<StudentGroupNewsLetter>()
                .Property(n => n.Description)
                .IsRequired();

            modelBuilder.Entity<StudentGroupNewsLetter>()
                .Property(n => n.GroupId)
                .IsRequired();

            // StudentGroupComments
            modelBuilder.Entity<StudentGroupComment>()
                .Property(c => c.GroupId)
                .IsRequired();

            modelBuilder.Entity<StudentGroupComment>()
                .Property(c => c.UserId)
                .IsRequired();
                
            modelBuilder.Entity<StudentGroupComment>()
                .Property(c => c.Message)
                .IsRequired();

            modelBuilder.Entity<StudentGroupComment>()
                .HasOne(c => c.Group)
                .WithMany(g => g.StudentGroupComments)
                .OnDelete(DeleteBehavior.Restrict);

            // Post Comments
            modelBuilder.Entity<PostComment>()
                .Property(pc => pc.Message)
                .IsRequired();

            modelBuilder.Entity<PostComment>()
                .Property(pc => pc.UserId)
                .IsRequired();

            modelBuilder.Entity<PostComment>()
                .Property(pc => pc.PostId)
                .IsRequired();
            
            modelBuilder.Entity<PostComment>()
                .HasOne(pc => pc.Post)
                .WithMany(p => p.PostCommets)
                .OnDelete(DeleteBehavior.Restrict);

            // StudentGroupPostComment
            modelBuilder.Entity<StudentGroupPostComment>()
                .Property(pc => pc.Message)
                .IsRequired();

            modelBuilder.Entity<StudentGroupPostComment>()
                .Property(pc => pc.UserId)
                .IsRequired();
            modelBuilder.Entity<StudentGroupPostComment>()
                .Property(pc => pc.PostId)
                .IsRequired();

            modelBuilder.Entity<StudentGroupPostComment>()
                .HasOne(pc => pc.Post)
                .WithMany(p => p.StudentGroupPostComments)
                .OnDelete(DeleteBehavior.Restrict);

            // University News
            modelBuilder.Entity<UniversityNews>()
                .Property(n => n.Description)
                .IsRequired();

            modelBuilder.Entity<UniversityNews>()
                .Property(n => n.Title)
                .IsRequired();

            modelBuilder.Entity<UniversityNews>()
                .Property(n => n.UserId)
                .IsRequired();


            // Global query filter
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDisabled);
            modelBuilder.Entity<Post>().HasQueryFilter(p => p.IsApproved);
            modelBuilder.Entity<Group>().HasQueryFilter(g => g.IsApproved);
            modelBuilder.Entity<Photo>().HasQueryFilter(p => p.IsApproved);
            modelBuilder.Entity<Interest>().HasQueryFilter(i => i.IsApproved);
            modelBuilder.Entity<UserReport>().HasQueryFilter(ur => ur.IsApproved);
        }
    }
}