using BasicAuthAPI.Core.Entities;

namespace BasicAuthAPI.DTOs.NewsDTOs;

public class CreateArticleDTO
{
    public string Title { get; set; }
    public string Content { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}