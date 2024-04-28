
namespace CA.Domain.Entities
{
    public class BaseEntity<TKey> : TrackableEntity, IHasKey<TKey>
    {
        public TKey Id { get; set; }
    }
}
