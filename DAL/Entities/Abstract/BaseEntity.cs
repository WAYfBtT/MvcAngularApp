namespace DAL.Entities.Abstract
{
    public class BaseEntity<TKey> where TKey : struct
    {
        public TKey Id { get; set; } = default;
    }

    public class BaseEntity : BaseEntity<int>
    {
    }
}
