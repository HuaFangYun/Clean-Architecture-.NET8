namespace CA.Domain.Entities
{
    public interface IHasKey<TKey>
    {
        TKey Id { get; set; }
    }
}
