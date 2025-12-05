using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib.Core.Domain
{
    public class EntityBase : IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; } //AUTO Increament

        public long? CreatedBy { get; set; } //B
        public long? ModifiedBy { get; set; } //B
        public long? DeletedBy { get; set; } //B
        public bool? Active { get; set; }
        public bool Deleted { get; set; }
        public long CounterId { get; set; }
        public DateTime? CreatedAt { get; set; } 
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}
