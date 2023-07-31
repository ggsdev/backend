namespace PRIO.src.Modules.Hierarchy.Fields.Dtos
{
    public class FRFieldDTO
    {
        public decimal? FRGas { get; set; }
        public decimal? FROil { get; set; }
        public decimal? FRWater { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public FieldWithoutWellDTO Field { get; set; }
    }
}
