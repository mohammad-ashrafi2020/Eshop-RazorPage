using CookieManager;
using Eshop.RazorPage.Models;
using Eshop.RazorPage.Models.Orders;
using Eshop.RazorPage.Services.Products;
using Eshop.RazorPage.Services.Sellers;

namespace Eshop.RazorPage.Infrastructure.CookieUtils;

public class ShopCartCookieManager
{
    private readonly ICookieManager _cookieManager;
    private const string CookieShopCartName = "shop-cart";
    private readonly ISellerService _sellerService;
    private readonly IProductService _productService;
    public ShopCartCookieManager(ICookieManager cookieManager, ISellerService sellerService, IProductService productService)
    {
        _cookieManager = cookieManager;
        _sellerService = sellerService;
        _productService = productService;
    }

    public OrderDto? GetShopCart()
    {
        return _cookieManager.Get<OrderDto>(CookieShopCartName);
    }

    public async Task<ApiResult> AddItem(long inventoryId, int count)
    {
        var shopCart = GetShopCart();
        var inventory = await _sellerService.GetInventoryById(inventoryId);
        if (inventory == null)
            return ApiResult.Error();

        var product = await _productService.GetProductById(inventory.ProductId);
        if (shopCart == null)
        {
            var order = new OrderDto()
            {
                Address = null,
                CreationDate = DateTime.Now,
                Discount = null,
                Id = 1,
                UserId = 1,
                UserFullName = "",
                Status = OrderStatus.Finally,
                Items = new List<OrderItemDto>()
                {
                    new OrderItemDto()
                    {
                        Price = inventory.Price,
                        Count = count,
                        ProductImageName = inventory.ProductImage,
                        ShopName = inventory.ShopName,
                        CreationDate = DateTime.Now,
                        ProductTitle = inventory.ProductTitle,
                        InventoryId = inventoryId,
                        OrderId = 1,
                        Id = GenerateId(),
                        ProductSlug =product!.Slug
                    }
                }
            };
            SetCookie(order);
            return ApiResult.Success();
        }
        else
        {
            if (shopCart.Items.Any(f => f.InventoryId == inventoryId))
            {
                var item = shopCart.Items.First(f => f.InventoryId == inventoryId);
                if (inventory.Count >= item.Count + count)
                    item.Count += count;
                else
                {
                    return ApiResult.Error("تعداد موجودی فروشنده کمتر از تعداد درخواستی است");
                }
            }
            else
            {
                var newItem = new OrderItemDto()
                {
                    Price = inventory.Price,
                    Count = count,
                    ProductImageName = inventory.ProductImage,
                    ShopName = inventory.ShopName,
                    CreationDate = DateTime.Now,
                    ProductTitle = inventory.ProductTitle,
                    InventoryId = inventoryId,
                    OrderId = 1,
                    Id = GenerateId(),
                    ProductSlug = product!.Slug
                };
                shopCart.Items.Add(newItem);
            }
            SetCookie(shopCart);
            return ApiResult.Success();
        }
    }

    public void Increase(long itemId)
    {
        var shopCart = GetShopCart();
        if (shopCart == null)
            return;

        var item = shopCart.Items.FirstOrDefault(f => f.Id == itemId);
        if (item == null)
            return;

        item.Count += 1;
        SetCookie(shopCart);
    }
    public void Decrease(long itemId)
    {
        var shopCart = GetShopCart();
        if (shopCart == null)
            return;
        var item = shopCart.Items.FirstOrDefault(f => f.Id == itemId);

        if (item == null || item.Count==1)
            return;
        item.Count -= 1;
        SetCookie(shopCart);

    }
    public void DeleteOrderItem(long itemId)
    {
        var shopCart = GetShopCart();
        if (shopCart == null)
            return;

        var item = shopCart.Items.FirstOrDefault(f => f.Id == itemId);
        if (item == null)
            return;

        shopCart.Items.Remove(item);
        SetCookie(shopCart);
    }
    private void SetCookie(OrderDto order)
    {
        _cookieManager.Set(CookieShopCartName, order, new CookieOptions()
        {
            HttpOnly = true,
            Expires = DateTimeOffset.Now.AddDays(7),
            Secure = true
        });
    }

    private long GenerateId()
    {
        var random = new Random();
        var number = random.Next(0, 10000) * 6 ^ 2 + random.Next(6, 1000000);
        return number;
    }
}