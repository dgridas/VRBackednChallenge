namespace HdrBoxReader.BO.Entities
{
    public class HdrBox : BaseEntity
    {
        public string SupplierIdentifier { get; set; }
        public string Identifier { get; set; }

        public ICollection<HdrBoxContent> Contents { get; set; }

    }
}