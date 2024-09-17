using BasicAuthAPI.Core.Entities;
using BasicAuthAPI.Database;
using BasicAuthAPI.DTOs.NewsDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BasicAuthAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NewsController : ControllerBase
{
    private readonly DatabaseContext _databaseContext;
    
    public NewsController(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    [HttpGet]
    [Route("/Articles/GetAllArticles")]
    public IEnumerable<ArticleDto> GetAllArticles()
    {
        return _databaseContext.Articles.Include(x => x.Author).Select(ArticleDto.FromEntity);
    }

    [HttpGet]
    [Route("/Articles/{articleId}")]
    public ArticleDto? GetArticleById([FromRoute] int articleId)
    {
        return _databaseContext
            .Articles.Include(x => x.Author)
            .Where(x => x.Id == articleId)
            .Select(ArticleDto.FromEntity)
            .SingleOrDefault();
    }

    // [HttpDelete]
    // [Route("/Articles/{deleteArticle}")]
    // public async Task<IActionResult> DeleteArticleById([FromRoute] int articleId)
    // {
    //     try
    //     {
    //         await _newsService.DeleteArticleById(articleId);
    //         return Ok();
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e.Message);
    //     }
    // }

    [HttpPut]
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
    
    /**
     * -------------- Below is for comments -------------
     * (This should really be split into 2 controllers.
     */
    
    [HttpGet]
    [Route("/Comments/GetCommentsOnArticle")]
    public IEnumerable<CommentDto> Get([FromQuery] int? articleId)
    {
        var query = _databaseContext.Comments.Include(x => x.Author).AsQueryable();
        if (articleId.HasValue)
            query = query.Where(c => c.ArticleId == articleId);
        return query.Select(CommentDto.FromEntity);
    }
    
    [HttpGet]
    [Route("/Comments/{commentId}")]
    public CommentDto? GetById(int commentId)
    {
        return _databaseContext
            .Comments.Include(x => x.Author)
            .Select(CommentDto.FromEntity)
            .SingleOrDefault(x => x.Id == commentId);
    }


    [HttpPost]
    [Route("/Comments/CreateComment")]
    public CommentDto Post([FromBody] CommentFormDto dto)
    {
        var userName = HttpContext.User.Identity?.Name;
        var author = _databaseContext.Users.Single(x => x.UserName == userName);
        var article = _databaseContext.Articles.Single(x => x.Id == dto.ArticleId);
        var entity = new Comment
        {
            Content = dto.Content,
            Article = article,
            Author = author,
        };
        var created = _databaseContext.Comments.Add(entity).Entity;
        _databaseContext.SaveChanges();
        return CommentDto.FromEntity(created);
    }

    // [HttpDelete]
    // [Route("/Comments/{commentId}")]
    // public async Task<IActionResult> DeleteComment([FromRoute] int commentId)
    // {
    //     try
    //     {
    //         await _newsService.DeleteComment(commentId);
    //         return Ok();
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e.Message);
    //     }
    // }

    [HttpPut]
    [Route("/Comments/{commentId}")]
    public CommentDto Put(int id, [FromBody] CommentFormDto dto)
    {
        var userName = HttpContext.User.Identity?.Name;
        var entity = _databaseContext
            .Comments.Include(x => x.Author)
            .Where(x => x.Author.UserName == userName)
            .Single(x => x.Id == id);
        entity.Content = dto.Content;
        var updated = _databaseContext.Comments.Update(entity).Entity;
        _databaseContext.SaveChanges();
        return CommentDto.FromEntity(updated);
    }
}