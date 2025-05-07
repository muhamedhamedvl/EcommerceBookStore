namespace BookShoppingCartMvcUI.Repositories
{
    public interface IExtendedUserOrderRepository : IUserOrderRepository
    {
        Task TogglePaymentStatus(int orderId);
        Task<Order?> GetOrderById(int orderId);
        Task ChangeOrderStatus(UpdateOrderStatusModel data);
    }
}
