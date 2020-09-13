namespace ControleBancario.Mapping
{
    using System;
    using ControleBancario.Model;
    using Microsoft.EntityFrameworkCore;

    public class SettingsMapping : IMapping
    {
        public void Mapping(ref ModelBuilder builder)
        {
            builder.Entity<Settings>().HasKey(p => p.ID);
            builder.Entity<Settings>().Property(p => p.Name).HasMaxLength(100).IsRequired(true);
            builder.Entity<Settings>().Property(p => p.IsAdmin).HasDefaultValue(false).IsRequired(true);
            builder.Entity<Settings>().Property(p => p.IsCreateUser).HasDefaultValue(false).IsRequired(true);
            builder.Entity<Settings>().Property(p => p.IsManager).HasDefaultValue(false).IsRequired(true);
            builder.Entity<User>().Property(p => p.DeletedAt).HasDefaultValue(new Nullable<DateTime>());
            builder.Entity<User>().Property(p => p.CreatedAt).HasDefaultValue(DateTime.UtcNow);
            builder.Entity<User>().Property(p => p.UpdatedAt).HasDefaultValue(DateTime.UtcNow);
        }
    }
}