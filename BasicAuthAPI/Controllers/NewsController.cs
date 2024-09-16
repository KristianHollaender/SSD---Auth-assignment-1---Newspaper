using BasicAuthAPI.Core.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasicAuthAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NewsController : ControllerBase
{
    private readonly INewsService _newsService;

    public NewsController(INewsService newsService)
    {
        _newsService = newsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllArticles()
    {
        try
        {
            return Ok(await _newsService.GetAllArticles());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("/{articleId}")]
    public async Task<IActionResult> GetArticleById([FromRoute] int articleId)
    {
        try
        {
            return Ok(await _newsService.GetArticleById(articleId));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Authorize]
    [Route("/{articleId}")]
    public async Task<IActionResult> DeleteArticleById([FromRoute] int articleId)
    {
        try
        {
            return Ok(await _newsService.DeleteArticleById(articleId));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> EditArticle()
    {
        try
        {
            return Ok(await _newsService.EditArticle());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateArticle()
    {
        try
        {
            return Ok(await _newsService.CreateArticle());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpGet]
    [Route("/{articleId}")]
    public async Task<IActionResult> GetCommentsOnArticle([FromRoute] int articleId)
    {
        try
        {
            return Ok(await _newsService.GetCommentsOnArticle(articleId));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateComment()
    {
        try
        {
            return Ok(await _newsService.CreateComment());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete]
    [Authorize]
    [Route("/{commentId}")]
    public async Task<IActionResult> DeleteComment([FromRoute] int commentId)
    {
        try
        {
            return Ok(await _newsService.CreateComment(commentId));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    
    [HttpGet]
    [Route("/rebuildDB")]
    public async Task<IActionResult> RebuildDatabase()
    {
        try
        {
            await _newsService.RebuildDatabase();
            return StatusCode(200, "Database recreated");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}