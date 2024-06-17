using GameStore.Dtos;

namespace GameStor.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameDto> games = [
        new (1, "Street Fighter II", "Fighting", 19.99m, new DateOnly(1992, 7, 15)),
    new (2, "Final Fantasy XIV", "Roleplaying", 59.99m, new DateOnly(2010, 9, 30)),
    new (3, "FIFA 23", "Sports", 49.99m, new DateOnly(2022, 9, 27)),
    new (4, "The Witcher 3: Wild Hunt", "Action RPG", 29.99m, new DateOnly(2015, 5, 19)),
    new (5, "Minecraft", "Sandbox", 26.95m, new DateOnly(2011, 11, 18))
    ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games");
        // GET /games
        group.MapGet("/", () => games);

        // GET /games/{id}
        group.MapGet("/{id}", (int id) =>
        {
            // kann null oder GameDto sein
            GameDto? game = games.Find(game => game.Id == id);

            return game == null ? Results.NotFound() : Results.Ok(game);
        })
        .WithName(GetGameEndpointName); // Route wird so benannt

        // POST /games
        group.MapPost("/", (CreateGameDto newGame) =>
        {

            GameDto game = new(games.Count + 1, newGame.Name, newGame.Genre, newGame.Price, newGame.ReleaseDate);

            games.Add(game);

            // id spezifiziert hier die id, die /games/{id} übergeben wird
            return Results.CreatedAtRoute("GetGame", new { id = game.Id }, game); // neues anonymes objekt
        });

        // PUT /games
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = games.FindIndex(game => game.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }

            games[index] = new GameDto(id, updatedGame.Name, updatedGame.Genre, updatedGame.Price, updatedGame.ReleaseDate);

            return Results.NoContent();
        });

        // DELETE /games

        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);
            return Results.NoContent();
        });

        return group;

    }
}

