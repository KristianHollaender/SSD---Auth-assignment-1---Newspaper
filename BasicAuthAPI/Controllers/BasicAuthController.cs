using BasicAuthAPI.Core.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasicAuthAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BasicAuthController : ControllerBase
{
    private readonly INewsService _newsService;

    public BasicAuthController(INewsService newsService)
    {
        _newsService = newsService;
    }
    
    [HttpGet]
    


}