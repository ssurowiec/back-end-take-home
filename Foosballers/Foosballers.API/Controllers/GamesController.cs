using Foosballers.Services;
using Microsoft.AspNetCore.Mvc;

namespace Foosballers.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
    private readonly ICreateGameService _createGameService;
    private readonly IGetAllGamesService _getAllGamesService;
    private readonly IScoreService _scoreService;

    public GamesController(ICreateGameService createGameService, IGetAllGamesService getAllGamesService, IScoreService scoreService)
    {
        _createGameService = createGameService;
        _getAllGamesService = getAllGamesService;
        _scoreService = scoreService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGames([FromQuery] string player)
    {
        var result = await _getAllGamesService.GetAllGames(player);

        if (!result.Found)
        {
            return NotFound();
        }

        return Ok(result.Games);
    }
    // [HttpGet("active")]
            // public async Task<IActionResult> GetActiveGame([FromQuery] string player)
            // {
            //     object result = await Task.FromResult(new object());
            //
            //     if (!result.Found)
            //     {
            //         return NotFound();
            //     }
            //
            //     return Ok(result.Games);
            // }

    [HttpPost]
    public async Task<IActionResult> CreateGame([FromBody] CreateGame command)
    {
        var result = await _createGameService.CreateGameAsync(command);
        if (!result.Success)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Created(string.Empty,null);
    }

    [HttpPost("score")]
    public async Task<IActionResult> Score([FromBody] string player)
    {
        var result = await _scoreService.Score(player);
        if (!result.Success)
        {
            return BadRequest();
        }
        return NoContent();
    }

}