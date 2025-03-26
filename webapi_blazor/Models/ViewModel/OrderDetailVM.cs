public class OrderItemVM{
    public int Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public List<ItemOderVM> lstOrderDetail { get; set; } = new List<ItemOderVM>();
}

public class ItemOderVM{
    public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public bool Deleted { get; set; }
}