using Eshop.RazorPage.Models;
using Eshop.RazorPage.Models.Orders;
using Eshop.RazorPage.Models.Orders.Command;

namespace Eshop.RazorPage.Services.Orders;

public class OrderService : IOrderService
{
    private readonly HttpClient _client;

    public OrderService(HttpClient client)
    {
        _client = client;
    }

    public async Task<ApiResult> AddOrderItem(AddOrderItemCommand command)
    {
        var result = await _client.PostAsJsonAsync("order", command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> CheckoutOrder(CheckOutOrderCommand command)
    {
        var result = await _client.PostAsJsonAsync("order", command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> IncreaseOrderItem(IncreaseOrderItemCountCommand command)
    {
        var result = await _client.PutAsJsonAsync("order/orderItem/IncreaseCount", command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> DecreaseOrderItem(DecreaseOrderItemCountCommand command)
    {
        var result = await _client.PutAsJsonAsync("order/orderItem/DecreaseCount", command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> DeleteOrderItem(DeleteOrderItemCommand command)
    {
        var result = await _client.DeleteAsync($"order/orderItem/{command.OrderItemId}");
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<OrderDto?> GetOrderById(long orderId)
    {
        var result = await _client.GetFromJsonAsync<ApiResult<OrderDto?>>($"order/{orderId}");
        return result?.Data;
    }

    public async Task<OrderDto?> GetCurrentOrder()
    {
        var result = await _client.GetFromJsonAsync<ApiResult<OrderDto?>>($"order/current");
        return result?.Data;
    }

    public async Task<OrderFilterResult> GetOrders(OrderFilterParams filterParams)
    {
        var result = await _client.GetFromJsonAsync<ApiResult<OrderFilterResult>>($"order");
        return result?.Data;
    }
}