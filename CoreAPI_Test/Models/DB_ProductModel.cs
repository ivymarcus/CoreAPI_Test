namespace CoreAPI_Test.Models
{
    public class DB_ProductModel
    {
        public int ProductID {  get; set; } 
        public string? ProductName { get; set; }
        public int UnitPrice {  get; set; }
        public decimal Discount { get; set; }
        public int CategoryID { get; set; }
        public string? Explain {  get; set; }
    }
}
