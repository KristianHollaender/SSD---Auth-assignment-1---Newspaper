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
    
    
    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteArticle()
    {
        try
        {
            return Ok(await _newsService.DeleteArticle());
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
    
    


}