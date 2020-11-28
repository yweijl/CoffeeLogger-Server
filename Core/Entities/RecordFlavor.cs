namespace Core.Entities
{
    public class RecordFlavor : EntityBase
    {
        public long RecordId { get; set; }
        public Record Record { get; set; }
        public long FlavorId { get; set; }
        public Flavor Flavor { get; set; }
    }
}
