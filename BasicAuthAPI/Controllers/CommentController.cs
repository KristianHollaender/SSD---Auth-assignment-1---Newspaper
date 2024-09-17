using BasicAuthAPI.Core.Entities;
using BasicAuthAPI.Database;
using BasicAuthAPI.DTOs.NewsDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BasicAuthAPI.Controllers;

public class CommentController: ControllerBase
{
    private readonly DatabaseContext _databaseContext;
    
    public CommentController(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    // No Auth because all roles are allowed to see comments
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
    [Authorize]
    [Route("/Comments/{commentId}")]
    public CommentDto? GetById(int commentId)
    {
        return _databaseContext
            .Comments.Include(x => x.Author)
            .Select(CommentDto.FromEntity)
            .SingleOrDefault(x => x.Id == commentId);
    }


    [HttpPost]
    [Authorize]
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

    [HttpDelete]
    [Authorize]
    [Route("/Comments/{commentId}")]
    public IActionResult DeleteComment([FromRoute] int commentId)
    {
        // Fetch the comment from the database
        var comment = _databaseContext.Comments
            .Include(x => x.Author)
            .SingleOrDefault(x => x.Id == commentId);

        // Check if the comment exists
        if (comment == null)
        {
            return NotFound("Comment not found");
        }

        // Remove the comment
        _databaseContext.Comments.Remove(comment);
        _databaseContext.SaveChanges();

        // Return a success response
        return Ok(CommentDto.FromEntity(comment));
    }

    [HttpPut]
    [Authorize]
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