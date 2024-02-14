using csharp_game.Repository;

namespace csharp_game.Endpoints
{
    public static class CharacterEndpoints
    {
        public static void ConfigureCharacterEndpoints(this WebApplication app)
        {
            var characterGroup = app.MapGroup("api/characters");

            characterGroup.MapPost("/" , async (CharacterDto characterDto , ICharacterRepository repository) =>
            {
                var character = new Character { Name = characterDto.Name , Class = characterDto.Class };
                var createdCharacter = await repository.CreateCharacter(character);
                return Results.Created($"/api/characters/{createdCharacter.Id}" , createdCharacter);
            });

            characterGroup.MapGet("/{id}" , async (int id , ICharacterRepository repository) =>
            {
                var character = await repository.GetCharacter(id);
                if(character == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(character);
            });

            characterGroup.MapPost("/{id}/fight" , async (int id , MonsterDto monsterDto , ICharacterRepository repository) =>
            {
                var character = await repository.FightMonster(id , monsterDto.Id);
                if(character == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(character);
            });
        }
    }
}
