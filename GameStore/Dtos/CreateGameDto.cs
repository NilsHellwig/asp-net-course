namespace GameStore.Dtos;

public record class CreateGameDto(int Id, string Name, string Genre, decimal Price, DateOnly ReleaseDate);
