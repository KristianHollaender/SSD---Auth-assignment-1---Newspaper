using BasicAuthAPI.Core.Entities;

namespace BasicAuthAPI.DTOs.NewsDTOs;

public class ArticleDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public AuthorDto Author { get; set; }
    public DateTime CreatedAt { get; set; }

    public static ArticleDto FromEntity(Article entity)
    {
        return new ArticleDto
        {
            Id = entity.Id,
            Title = entity.Title,
            Content = entity.Content,
            Author = AuthorDto.FromEntity(entity.Author),
            CreatedAt = entity.CreatedAt
        };
    }
}