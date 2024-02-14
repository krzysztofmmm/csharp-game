using csharp_game.Models;

namespace csharp_game.Repository
{
    public interface ICharacterRepository
    {
        Task<Character> CreateCharacter(Character character);
        Task<Character> GetCharacter(int id);
        Task<Character> FightMonster(int characterId , int monsterId);
    }
}
