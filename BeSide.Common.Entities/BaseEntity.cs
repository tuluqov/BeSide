using System.ComponentModel.DataAnnotations;

namespace BeSide.Common.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public virtual int Id { get; set; }
    }
}
