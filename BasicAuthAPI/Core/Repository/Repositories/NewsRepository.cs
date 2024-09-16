using BasicAuthAPI.Core.Entities;
using BasicAuthAPI.Core.Repository.Interfaces;
using BasicAuthAPI.Database;
using BasicAuthAPI.DTOs.NewsDTOs;

namespace BasicAuthAPI.Core.Repository.Repositories;

public class NewsRepository : INewsRepository
{
    private readonly DatabaseContext _databaseContext;

    public NewsRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
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

    public async Task RebuildDatabase()
    {
        await _databaseContext.Database.EnsureDeletedAsync();
        await _databaseContext.Database.EnsureCreatedAsync();
    }
}