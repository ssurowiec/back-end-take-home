using Foosballers.Core;
using Foosballers.Infrastructure;
using Foosballers.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IGameRepository, InMemoryGameRepository>();
builder.Services.AddScoped<ICreateGameService, CreateGameService>();
builder.Services.AddScoped<IScoreService, ScoreService>();
builder.Services.AddScoped<IGetAllGamesService, GetAllGamesService>();
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();

app.Run();