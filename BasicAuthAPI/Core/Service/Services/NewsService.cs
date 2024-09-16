using BasicAuthAPI.Core.Entities;
using BasicAuthAPI.Core.Repository.Interfaces;
using BasicAuthAPI.Core.Service.Interfaces;
using BasicAuthAPI.DTOs.NewsDTOs;

namespace BasicAuthAPI.Core.Service.Services;

public class NewsService : INewsService
{
    private readonly INewsRepository _newsRepository;

    public NewsService(INewsRepository newsRepository)
    {
        _newsRepository = newsRepository;
    }

    public async Task<IEnumerable<Article>> GetAllArticles()
    {
        return await _newsRepository.GetAllArticles();
    }

    public async Task<Article> GetArticleById(int articleId)
    {
        return await _newsRepository.GetArticleById(articleId);
    }

    public async Task CreateArticle(CreateArticleDTO createArticleDto)
    {
        await _newsRepository.CreateArticle(createArticleDto);
    }

    public async Task DeleteArticleById(int articleId)
    {
        await _newsRepository.DeleteArticleById(articleId);
    }

    public async Task EditArticle(EditArticleDTO editArticleDto)
    {
        await _newsRepository.EditArticle(editArticleDto);
    }

    public async Task CreateComment(CreateCommentDTO createCommentDto)
    {
        await _newsRepository.CreateComment(createCommentDto);
    }

    public async Task DeleteComment(int commentId)
    {
        await _newsRepository.DeleteComment(commentId);
    }

    public async Task EditComment(EditCommentDTO editCommentDto)
    {
        await _newsRepository.EditComment(editCommentDto);
    }

    public async Task RebuildDatabase()
    {
        await _newsRepository.RebuildDatabase();
    }
}