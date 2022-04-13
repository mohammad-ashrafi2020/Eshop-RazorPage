using Eshop.RazorPage.Models;
using Eshop.RazorPage.Models.Orders;
using Eshop.RazorPage.Models.Orders.Command;

namespace Eshop.RazorPage.Services.Orders;

public interface IOrderService
{
    Task<ApiResult> AddOrderItem(AddOrderItemCommand command);
    Task<ApiResult> CheckoutOrder(CheckOutOrderCommand command);
    Task<ApiResult> IncreaseOrderItem(IncreaseOrderItemCountCommand command);
    Task<ApiResult> DecreaseOrderItem(DecreaseOrderItemCountCommand command);
    Task<ApiResult> DeleteOrderItem(DeleteOrderItemCommand command);


    Task<OrderDto?> GetOrderById(long orderId);
    Task<OrderDto?> GetCurrentOrder();
    Task<OrderFilterResult> GetOrders(OrderFilterParams filterParams);

}