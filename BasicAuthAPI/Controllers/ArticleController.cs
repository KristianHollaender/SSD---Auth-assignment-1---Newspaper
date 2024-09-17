using BasicAuthAPI.Core.Entities;
using BasicAuthAPI.Database;
using BasicAuthAPI.DTOs.NewsDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BasicAuthAPI.Controllers;

public class ArticleController : ControllerBase
{
    private readonly DatabaseContext _databaseContext;
    
    public ArticleController(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    // No Auth because all roles are allowed to see articles
    [HttpGet]
    [Route("/Articles/GetAllArticles")]
    public IEnumerable<ArticleDto> GetAllArticles()
    {
        return _databaseContext.Articles.Include(x => x.Author).Select(ArticleDto.FromEntity);
    }

    [HttpGet]
    [Authorize]
    [Route("/Articles/{articleId}")]
    public ArticleDto? GetArticleById([FromRoute] int articleId)
    {
        return _databaseContext
            .Articles.Include(x => x.Author)
            .Where(x => x.Id == articleId)
            .Select(ArticleDto.FromEntity)
            .SingleOrDefault();
    }

    [HttpDelete]
    [Authorize]
    [Route("/Articles/{articleId}")]
    public IActionResult DeleteArticleById([FromRoute] int articleId)
    {
        // Fetch the article from the database
        var article = _databaseContext.Articles
            .Include(x => x.Author)
            .SingleOrDefault(x => x.Id == articleId);

        // Check if the article exists
        if (article == null)
        {
            return NotFound("Article not found");
        }

        // Remove the article
        _databaseContext.Articles.Remove(article);
        _databaseContext.SaveChanges();

        // Return a success response
        return Ok(ArticleDto.FromEntity(article));
    }


    [HttpPut]
    [Authorize]
    [Route("/Articles/{articleId}")]
    public ArticleDto EditArticle([FromBody] ArticleFormDto dto, [FromRoute] int articleId)
    {
        var userName = HttpContext.User.Identity?.Name;
        var entity = _databaseContext
            .Articles
            .Include(x => x.Author)
            .Single(x => x.Id == articleId);
        entity.Title = dto.Title;
        entity.Content = dto.Content;
        var updated = _databaseContext.Articles.Update(entity).Entity;
        _databaseContext.SaveChanges();
        return ArticleDto.FromEntity(updated);
    }

    [HttpPost]
    [Authorize]
    [Route("/Articles/CreateArticle")]
    public ArticleDto CreateArticle([FromBody] ArticleFormDto formDto)
    {
        var userName = HttpContext.User.Identity?.Name;
        var author = _databaseContext.Users.Single(x => x.UserName == userName);
        var entity = new Article
        {
            Title = formDto.Title,
            Content = formDto.Content,
            Author = author,
            CreatedAt = DateTime.Now
        };
        var created = _databaseContext.Articles.Add(entity).Entity;
        _databaseContext.SaveChanges();
        return ArticleDto.FromEntity(created);
    }
    
}