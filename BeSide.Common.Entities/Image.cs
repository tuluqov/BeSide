using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeSide.Common.Entities
{
    public class Image : BaseEntity
    {
        [StringLength(255)]
        public string FileName { get; set; }

        [StringLength(100)]
        public string ContentType { get; set; }

        public byte[] Content { get; set; }

        public int? OrderId { get; set; }
        public virtual Order Order { get; set; }

        public FileType FileType { get; set; }
    }
}
