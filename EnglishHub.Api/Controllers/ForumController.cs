using EnglishHub.Domain.UseCases.GetForumUseCase;
using EnglishHub.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnglishHub.Controllers;


[ApiController]
[Route("[controller]")]
public class ForumController : ControllerBase
{
    private readonly IForumUseCase _useCase;
    
    public ForumController(IForumUseCase useCase)
    {
        _useCase = useCase;
    }
    
    [HttpGet]
    [Route("All")]
    public async Task<IActionResult> Get()
    {
        return Ok((await _useCase.GetForums()).Select(a =>new Forum(){Id = a.Id,Title = a.Title}));
    }
}