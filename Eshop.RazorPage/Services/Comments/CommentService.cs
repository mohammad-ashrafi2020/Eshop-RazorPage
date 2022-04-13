using Eshop.RazorPage.Models;
using Eshop.RazorPage.Models.Categories;
using Eshop.RazorPage.Models.Comments;
using Eshop.RazorPage.Services.Banners;

namespace Eshop.RazorPage.Services.Comments;

public class CommentService : ICommentService
{
    private readonly HttpClient _client;

    public CommentService(HttpClient client)
    {
        _client = client;
    }

    public async Task<ApiResult> AddComment(AddCommentCommand command)
    {
        var result = await _client.PostAsJsonAsync("comment", command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> EditComment(EditCommentCommand command)
    {
        var result = await _client.PutAsJsonAsync("comment", command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> ChangeStatus(long commentId, CommentStatus status)
    {
        var result = await _client.PutAsJsonAsync("comment/changeStatus", new { id = commentId, status });
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<CommentFilterResult> GetCommentsByFilter(CommentFilterParams filterParams)
    {
        var result = await _client.GetFromJsonAsync<ApiResult<CommentFilterResult>>($"comment");
        return result?.Data;
    }

    public async Task<CommentDto?> GetCommentById(long id)
    {
        var result = await _client.GetFromJsonAsync<ApiResult<CommentDto>>($"comment/{id}");
        return result?.Data;
    }
}