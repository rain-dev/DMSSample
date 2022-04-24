namespace Pinewood.DMSSample.Data.Models
{
    public abstract class ModelBase<TId>
        where TId : struct
    {
        public TId Id { get; set; }
    }
}
