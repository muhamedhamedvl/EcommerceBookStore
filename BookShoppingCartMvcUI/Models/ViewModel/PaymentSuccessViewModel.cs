namespace BookShoppingCartMvcUI.Models.ViewModel
{
    public class PaymentSuccessViewModel
    {
        public string PaymentId { get; set; }
        public decimal AmountPaid { get; set; }
        public string Currency { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
    }
}
