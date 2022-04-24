namespace Pinewood.DMSSample.Data.Models
{
    public class PartInvoice : ModelBase<int>
    {
        public PartInvoice(string stockCode, int quantity, int customerID)
            : this(-1, stockCode, quantity, customerID)
        {
        }
        public PartInvoice(int id, string stockCode, int quantity, int customerID)
        {
            Id = id;
            StockCode = stockCode;
            Quantity = quantity;
            CustomerID = customerID;
        }
        public string StockCode { get; set; }
        public int Quantity { get; set; }
        public int CustomerID { get; set; }
    }
}
