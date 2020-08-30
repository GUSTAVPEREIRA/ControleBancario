namespace ControleBancario
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class ApplicationContext : IdentityDbContext
    {
        //public DbSet<Model> NomeTabela { get; set; }

        public ApplicationContext()
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
            //new UserMapping().Mapping(ref builder);

            base.OnModelCreating(builder);
        }
    }
}