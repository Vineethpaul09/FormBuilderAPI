namespace FormBuilderAPI.Data
{
    using FormBuilderAPI.Data.Entities;
    using Microsoft.EntityFrameworkCore;

    public class AppDbContext : DbContext
    {
        public DbSet<Form> Forms { get; set; }
        public DbSet<FormSection> FormSections { get; set; }
        public DbSet<FormField> FormFields { get; set; }
        public DbSet<FormSubmission> FormSubmissions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tenant> Tenants { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tenant>().HasData(
                new Tenant
                {
                    Id = 1,
                    Name = "Vineeth",
                    Description = "Tenant for Vineeth",
                    CreatedAt = new DateTime() // Dynamic value causing the issue
                }
            );

            // Configure Form -> FormSection relationship
            modelBuilder.Entity<Form>()
                .HasMany(f => f.Fields)
                .WithOne(fs => fs.Form)
                .HasForeignKey(fs => fs.FormId);

            // Configure FormSection -> FormField relationship
            modelBuilder.Entity<FormSection>()
                .HasMany(fs => fs.Columns)
                .WithOne(ff => ff.FormSection)
                .HasForeignKey(ff => ff.FormSectionId);

            modelBuilder.Entity<Form>()
                .HasOne(f => f.Tenant)
                .WithMany()
                .HasForeignKey(f => f.TenantId);

            // Configure primary keys
            modelBuilder.Entity<FormField>()
                .HasKey(ff => ff.Id);

            modelBuilder.Entity<FormSection>()
                .HasKey(fs => fs.Id);

            modelBuilder.Entity<Form>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<Tenant>()
                .HasKey(t => t.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
