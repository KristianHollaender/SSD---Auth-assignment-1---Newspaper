using AutoMapper;
using BasicAuthAPI.Core.Entities;
using BasicAuthAPI.Core.Repository.Interfaces;
using BasicAuthAPI.Core.Service.Interfaces;
using BasicAuthAPI.DTOs.NewsDTOs;

namespace BasicAuthAPI.Core.Service.Services;

public class NewsService(INewsRepository newsRepository, IMapper mapper) : INewsService
{
    
    public async Task<IEnumerable<Article>> GetAllArticles()
    {
        return await newsRepository.GetAllArticles();
    }

    public async Task<Article> GetArticleById(int articleId)
    {
        return await newsRepository.GetArticleById(articleId);
    }

    public async Task<Article> CreateArticle(CreateArticleDTO article)
    {
        return await newsRepository.CreateArticle(mapper.Map<Article>(article));
    }

    public async Task DeleteArticleById(int articleId)
    {
        await newsRepository.DeleteArticleById(articleId);
    }

    public async Task<Article> EditArticle(EditArticleDTO article, int articleId)
    {
        return await newsRepository.EditArticle(mapper.Map<Article>(article), articleId);
    }

    public async Task<Comment> CreateComment(CreateCommentDTO comment)
    { 
        return await newsRepository.CreateComment(mapper.Map<Comment>(comment));
    }

    public async Task DeleteComment(int commentId)
    {
        await newsRepository.DeleteComment(commentId);
    }

    public async Task<Comment> EditComment(EditCommentDTO comment, int commentId)
    {
        return await newsRepository.EditComment(mapper.Map<Comment>(comment), commentId);
    }

    public async Task RebuildDatabase()
    {
        await newsRepository.RebuildDatabase();
    }
}