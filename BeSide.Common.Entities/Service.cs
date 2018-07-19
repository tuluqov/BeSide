namespace BeSide.Common.Entities
{
    public class Service : BaseEntity
    {
        public string Name { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
