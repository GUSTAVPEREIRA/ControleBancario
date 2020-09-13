namespace ControleBancario.Model
{
    using System;      

    public abstract class BaseEntityModel
    {        
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
