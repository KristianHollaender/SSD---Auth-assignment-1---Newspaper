using BasicAuthAPI.Core.Service.Interfaces;
using BasicAuthAPI.DTOs.NewsDTOs;
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
    [Route("/Articles/GetAllArticles")]
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
    [Route("/Articles/{articleId}")]
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
    [Route("/Articles/{deleteArticle}")]
    public async Task<IActionResult> DeleteArticleById([FromRoute] int articleId)
    {
        try
        {
            await _newsService.DeleteArticleById(articleId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Authorize]
    [Route("/Articles/{articleId}")]
    public async Task<IActionResult> EditArticle([FromBody] EditArticleDTO dto, [FromRoute] int articleId)
    {
        try
        {
            await _newsService.EditArticle(dto, articleId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Authorize]
    [Route("/Articles/CreateArticle")]
    public async Task<IActionResult> CreateArticle([FromBody] CreateArticleDTO dto)
    {
        try
        {
            await _newsService.CreateArticle(dto);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Authorize]
    [Route("/Comments/CreateComment")]
    public async Task<IActionResult> CreateComment([FromBody] CreateCommentDTO dto)
    {
        try
        {
            await _newsService.CreateComment(dto);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Authorize]
    [Route("/Comments/{commentId}")]
    public async Task<IActionResult> DeleteComment([FromRoute] int commentId)
    {
        try
        {
            await _newsService.DeleteComment(commentId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Authorize]
    [Route("/Comments/{commentId}")]
    public async Task<IActionResult> EditComment([FromBody] EditCommentDTO dto, [FromRoute] int commentId)
    {
        try
        {
            await _newsService.EditComment(dto, commentId);
            return Ok();
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