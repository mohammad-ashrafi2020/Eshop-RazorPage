namespace Eshop.RazorPage.Models.Comments;

public class AddCommentCommand
{
    public string Text { get; set; }
    public long UserId { get; set; }
    public long ProductId { get; set; }
}
public class EditCommentCommand
{
    public long CommentId { get; set; }
    public string Text { get; set; }
    public long UserId { get; set; }
}