namespace Domain.Models
{
    public class PredictedOrderModel
    {
        public int Custid { get; set; }
        public string CustomerName { get; set; } = null!;
        public DateTime LastOrderDate { get; set; }
        public DateTime? NextPredictedOrder { get; set; }
    }
}
