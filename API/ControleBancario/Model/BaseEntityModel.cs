namespace ControleBancario.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;    

    public abstract class BaseEntityModel
    {
        [Key]        
        public int ID { get; set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; protected set; }

        public void SetLogicDeleted()
        {
            this.DeletedAt = DateTime.UtcNow;
            this.Update();
        }

        public void UnsetLogicDeleted()
        {
            this.DeletedAt = new Nullable<DateTime>();
            this.Update();
        }

        public void Update()
        {
            this.UpdatedAt = DateTime.UtcNow;
        }
    }
}
