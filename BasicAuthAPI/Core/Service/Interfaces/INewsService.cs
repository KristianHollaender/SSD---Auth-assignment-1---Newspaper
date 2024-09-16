using BasicAuthAPI.Core.Entities;
using BasicAuthAPI.DTOs.NewsDTOs;

namespace BasicAuthAPI.Core.Service.Interfaces;

public interface INewsService
{
    public Task<IEnumerable<Article>> GetAllArticles();
    public Task<Article> GetArticleById(int articleId);
    public Task<Article> CreateArticle(CreateArticleDTO article);
    public Task DeleteArticleById(int articleId);
    public Task<Article> EditArticle(EditArticleDTO article, int articleId);

    public Task<Comment> CreateComment(CreateCommentDTO comment);
    public Task DeleteComment(int commentId);
    public Task<Comment> EditComment(EditCommentDTO comment, int commentId);
    
    
    public Task RebuildDatabase();
}