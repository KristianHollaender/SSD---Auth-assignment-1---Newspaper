using BasicAuthAPI.Core.Entities;
using BasicAuthAPI.Core.Repository.Interfaces;
using BasicAuthAPI.Database;
using Microsoft.EntityFrameworkCore;

namespace BasicAuthAPI.Core.Repository.Repositories;

public class NewsRepository(DatabaseContext databaseContext) : INewsRepository
{
    public async Task<IEnumerable<Article>> GetAllArticles()
    {
        return await databaseContext.Articles.ToListAsync();
    }

    public async Task<Article> GetArticleById(int articleId)
    {
        return await databaseContext.Articles.FirstOrDefaultAsync(a => a.Id == articleId);
    }

    public async Task<Article> CreateArticle(Article article)
    {
        await databaseContext.Articles.AddAsync(article);
        await databaseContext.SaveChangesAsync();
        return article;
    }

    public async Task DeleteArticleById(int articleId)
    {
        var articleToDelete = await databaseContext.Articles.FirstOrDefaultAsync(a => a.Id == articleId);
        
        if (articleToDelete == null)
        {
            throw new Exception("article not found");
        }
        
        databaseContext.Articles.Remove(articleToDelete);
        await databaseContext.SaveChangesAsync();
    }

    public async Task<Article> EditArticle(Article article, int articleId)
    {
        var articleToEdit = await databaseContext.Articles.FirstOrDefaultAsync(a => a.Id == articleId);
        
        if (articleToEdit == null)
        {
            throw new Exception("article not found");
        }
        
        databaseContext.Articles.Update(articleToEdit);
        await databaseContext.SaveChangesAsync();
        return articleToEdit;
    }

    public async Task<Comment> CreateComment(Comment comment)
    {
        await databaseContext.Comments.AddAsync(comment);
        await databaseContext.SaveChangesAsync();
        return comment;
    }

    public async Task DeleteComment(int commentId)
    {
        var commentToDelete = await databaseContext.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
        
        if (commentToDelete == null)
        {
            throw new Exception("Comment not found");
        }
        
        databaseContext.Comments.Remove(commentToDelete);
        await databaseContext.SaveChangesAsync();
    }

    public async Task<Comment> EditComment(Comment comment, int commentId)
    {
        var commentToEdit = await databaseContext.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
        
        if (commentToEdit == null)
        {
            throw new Exception("Comment not found");
        }
        
        databaseContext.Comments.Update(commentToEdit);
        await databaseContext.SaveChangesAsync();
        return commentToEdit;
    }

    public async Task RebuildDatabase()
    {
        await databaseContext.Database.EnsureDeletedAsync();
        await databaseContext.Database.EnsureCreatedAsync();
    }
}