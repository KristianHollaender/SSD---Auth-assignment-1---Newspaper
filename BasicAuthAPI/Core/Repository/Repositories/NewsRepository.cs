using BasicAuthAPI.Core.Entities;
using BasicAuthAPI.Core.Repository.Interfaces;
using BasicAuthAPI.Database;
using BasicAuthAPI.DTOs.NewsDTOs;
using Microsoft.EntityFrameworkCore;

namespace BasicAuthAPI.Core.Repository.Repositories;

public class NewsRepository : INewsRepository
{
    private readonly DatabaseContext _databaseContext;

    public NewsRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<IEnumerable<Article>> GetAllArticles()
    {
        return await _databaseContext.Articles.ToListAsync();
    }

    public async Task<Article> GetArticleById(int articleId)
    {
        return await _databaseContext.Articles.FirstOrDefaultAsync(a => a.Id == articleId);
    }

    public async Task CreateArticle(CreateArticleDTO createArticleDto)
    {
        await _databaseContext.Articles.AddAsync(createArticleDto);
        await _databaseContext.SaveChangesAsync();
    }

    public async Task DeleteArticleById(int articleId)
    {
        var articleToDelete = await _databaseContext.Articles.FirstOrDefaultAsync(a => a.Id == articleId);
        _databaseContext.Articles.Remove(articleToDelete);
        await _databaseContext.SaveChangesAsync();
    }

    public async Task EditArticle(EditArticleDTO editArticleDto, int articleId)
    {
        var articleToEdit = await _databaseContext.Articles.FirstOrDefaultAsync(a => a.Id == articleId);
        _databaseContext.Articles.Update(articleToEdit);
        await _databaseContext.SaveChangesAsync();
    }

    public async Task CreateComment(CreateCommentDTO createCommentDto)
    {
        await _databaseContext.Comments.AddAsync(createCommentDto);
        await _databaseContext.SaveChangesAsync();
    }

    public async Task DeleteComment(int commentId)
    {
        var commentToDelete = await _databaseContext.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
        _databaseContext.Comments.Remove(commentToDelete);
        await _databaseContext.SaveChangesAsync();
    }

    public async Task EditComment(EditCommentDTO editCommentDto, int commentId)
    {
        var commentToEdit = await _databaseContext.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
        _databaseContext.Comments.Update(commentToEdit);
        await _databaseContext.SaveChangesAsync();
    }

    public async Task RebuildDatabase()
    {
        await _databaseContext.Database.EnsureDeletedAsync();
        await _databaseContext.Database.EnsureCreatedAsync();
    }
}