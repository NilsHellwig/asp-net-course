namespace GameStore.Dtos;

// Records: Standardmäßig sind die Eigenschaften eines record unveränderlich. Das bedeutet, dass sie nach der Erstellung des Objekts nicht mehr geändert werden können (du kannst dies jedoch überschreiben, wenn du es möchtest).
public record class GameDto(int Id, string Name, string Genre, decimal Price, DateOnly ReleaseDate);