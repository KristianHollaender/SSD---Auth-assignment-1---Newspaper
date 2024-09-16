using BasicAuthAPI.Core.Entities;
using BasicAuthAPI.Core.Repository.Interfaces;

namespace BasicAuthAPI.Core.Service.Interfaces;

public interface INewsService
{
    public Task<IEnumerable<Article>> GetAllArticles();
    public Task<Article> GetArticleById(int articleId);
    public Task CreateArticle(CreateArticleDTO createArticleDto);
    public Task DeleteArticleById(int articleId);
    public Task EditArticle(EditArticleDTO editArticleDto);

    public Task CreateComment(CreateCommentDTO createCommentDto);
    public Task DeleteComment(int commentId);
    public Task EditComment(EditCommentDTO editCommentDto);
    
    
    public Task RebuildDatabase();
}