using BasicAuthAPI.Core.Entities;
using BasicAuthAPI.Core.Repository.Interfaces;
using BasicAuthAPI.Core.Service.Interfaces;

namespace BasicAuthAPI.Core.Service.Services;

public class NewsService : INewsService
{
    private readonly INewsRepository _newsRepository;

    public NewsService(INewsRepository newsRepository)
    {
        _newsRepository = newsRepository;
    }

    public Task<IEnumerable<Article>> GetAllArticles()
    {
        throw new NotImplementedException();
    }

    public Task<Article> GetArticleById(int articleId)
    {
        throw new NotImplementedException();
    }

    public Task CreateArticle(CreateArticleDTO createArticleDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteArticleById(int articleId)
    {
        throw new NotImplementedException();
    }

    public Task EditArticle(EditArticleDTO editArticleDto)
    {
        throw new NotImplementedException();
    }

    public Task CreateComment(CreateCommentDTO createCommentDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteComment(int commentId)
    {
        throw new NotImplementedException();
    }

    public Task EditComment(EditCommentDTO editCommentDto)
    {
        throw new NotImplementedException();
    }

    public Task RebuildDatabase()
    {
        
    }
}