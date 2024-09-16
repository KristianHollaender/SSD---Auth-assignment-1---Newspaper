using BasicAuthAPI.Core.Entities;

namespace BasicAuthAPI.DTOs.NewsDTOs;

public class CreateCommentDTO
{
    public string Content { get; set; }
    public int ArticleId { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
}