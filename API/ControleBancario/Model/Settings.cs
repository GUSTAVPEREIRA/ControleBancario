namespace ControleBancario.Model
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;
    using System.ComponentModel.DataAnnotations;

    public class Settings : BaseEntityModel
    {
        public Settings()
        {

        }

        public Settings(string name, bool isAdmin, bool isManager, bool isCreateUser)
        {
            Name = name;
            IsAdmin = isAdmin;
            IsManager = isManager;
            IsCreateUser = isCreateUser;
            UpdatedAt = DateTime.UtcNow;
            CreatedAt = DateTime.UtcNow;
            DeletedAt = new Nullable<DateTime>();
        }

        public Settings(string name)
        {
            UpdatedAt = DateTime.UtcNow;
            CreatedAt = DateTime.UtcNow;
            DeletedAt = new Nullable<DateTime>();
        }

        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsManager { get; set; }
        public bool IsCreateUser { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public List<User> Users { get; set; }       
    }
}