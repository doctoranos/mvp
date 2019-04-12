using api.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Context
{
    public class DoctoranosDbContext : DbContext
    {
        public DbSet<Form> Forms { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<CompletedForm> CompletedForms { get; set; }

        public DoctoranosDbContext(DbContextOptions<DoctoranosDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Form>()
                .ToTable("form")
                .HasMany(x => x.Questions)
                .WithOne(x => x.Form)
                .HasForeignKey(x => x.FormId)
                .HasPrincipalKey(x => x.Id);
            
            modelBuilder.Entity<Question>().ToTable("question");
            
            modelBuilder.Entity<CompletedForm>()
                .ToTable("completed_form")
                .HasOne(x => x.Form)
                .WithMany(x => x.CompletedForms)
                .HasForeignKey(x => x.FormId)
                .HasPrincipalKey(x => x.Id);
        }
    }
}