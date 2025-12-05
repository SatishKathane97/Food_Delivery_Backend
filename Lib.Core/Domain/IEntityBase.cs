namespace Lib.Core.Domain
{
    public interface IEntityBase
    {
        public long Id { get; set; }
        public long CounterId { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public long? DeletedBy { get; set; }
        public bool? Active { get; set; }
        public bool Deleted { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
