namespace ControleBancario.Mapping
{
    using System;
    using ControleBancario.Model;
    using Microsoft.EntityFrameworkCore;

    public class UserMapping : IMapping
    {
        public void Mapping(ref ModelBuilder builder)
        {
            builder.Entity<User>().HasKey(k => k.ID);
            builder.Entity<User>().Property(p => p.ID).UseSerialColumn();
            builder.Entity<User>().Property(p => p.UserName).HasMaxLength(30).IsRequired();
            builder.Entity<User>().Property(p => p.Password).HasMaxLength(30).IsRequired();
            builder.Entity<User>().Property(p => p.DeletedAt).HasDefaultValue(new Nullable<DateTime>());
            builder.Entity<User>().Property(p => p.CreatedAt).HasDefaultValue(DateTime.UtcNow);
            builder.Entity<User>().Property(p => p.UpdatedAt).HasDefaultValue(DateTime.UtcNow);
            builder.Entity<User>().Property(p => p.FName).HasDefaultValue("").HasMaxLength(60);
            builder.Entity<User>().Property(p => p.LName).HasDefaultValue("").HasMaxLength(60);
            builder.Entity<User>().Property(p => p.Email).HasDefaultValue(null).HasMaxLength(100);
            builder.Entity<User>().Property(p => p.UpdatedAt).HasDefaultValue(DateTime.UtcNow);
            builder.Entity<User>().Property(p => p.UpdatedAt).HasDefaultValue(DateTime.UtcNow);
        }
    }
}