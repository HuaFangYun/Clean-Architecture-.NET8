using System.ComponentModel.DataAnnotations;

namespace CA.Domain.Entities
{
    public class TrackableEntity
    {
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
