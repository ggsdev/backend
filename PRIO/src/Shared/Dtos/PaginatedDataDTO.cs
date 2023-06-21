namespace PRIO.src.Shared.Dtos
{
    public class PaginatedDataDTO<T>
    {
        public string? PreviousPage { get; set; }
        public string? NextPage { get; set; }
        public int? Count { get; set; }
        public List<T>? Data { get; set; }
    }
}
