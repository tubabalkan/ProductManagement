namespace ProductManagement.Domain.Entities
{
    public class LowStockLog
    {
        public int Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Stock { get; set; }
        public DateTime LoggedAt { get; set; }
    }
}
