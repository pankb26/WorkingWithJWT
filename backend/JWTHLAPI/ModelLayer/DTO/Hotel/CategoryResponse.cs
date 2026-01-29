namespace JWTHLAPI.ModelLayer.DTO.Hotel
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public int CounterId { get; set; }
        public string CounterName { get; set; }
        public string CategoryName { get; set; }
        public int? DisplayOrder { get; set; }
    }
}