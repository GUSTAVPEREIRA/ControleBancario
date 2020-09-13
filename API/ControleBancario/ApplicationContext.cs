namespace ControleBancario
{
    using ControleBancario.Model;
    using ControleBancario.Mapping;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class ApplicationContext : IdentityDbContext
    {
        public DbSet<User> TbUsers { get; set; }
        public DbSet<Settings> TbSettings { get; set; }

        public ApplicationContext()
        {

        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("User ID=postgres;Password=postgres;Server=localhost;Port=5432; Database=ControleBancarioTestes; Integrated Security=true;Pooling=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            new UserMapping().Mapping(ref builder);
            new SettingsMapping().Mapping(ref builder);

            base.OnModelCreating(builder);
        }
    }
}