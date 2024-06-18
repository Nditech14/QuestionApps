using Microsoft.EntityFrameworkCore;
using QuestionApp.Entity;

namespace QuestionApp.Data
{
    public class QuestionAppDbContext : DbContext
    {

        public DbSet<Question> Questions { get; set; }
        public DbSet<Response> Responses { get; set; }
        public QuestionAppDbContext(DbContextOptions options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Response>().HasKey(r => r.Id);
            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id);
            });
        }

    }
}
