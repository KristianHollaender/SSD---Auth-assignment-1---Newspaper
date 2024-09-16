using BasicAuthAPI.Core.Entities;

namespace BasicAuthAPI.Core.Repository.Interfaces;

public interface INewsRepository
{
    public Task<IEnumerable<Article>> GetAllArticles();
    public Task<Article> GetArticleById(int articleId);
    public Task<Article> CreateArticle(Article article);
    public Task DeleteArticleById(int articleId);
    public Task<Article> EditArticle(Article article, int articleId);

    public Task<Comment> CreateComment(Comment comment);
    public Task DeleteComment(int commentId);
    public Task<Comment> EditComment(Comment comment, int commentId);
    
    
    public Task RebuildDatabase();
}