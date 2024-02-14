using csharp_game.Data;
using csharp_game.Models;
using Microsoft.EntityFrameworkCore;

namespace csharp_game.Repository
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly GameContext _context;

        public CharacterRepository(GameContext context)
        {
            _context = context;
        }

        public async Task<Character> CreateCharacter(Character character)
        {
            var existingCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Name == character.Name);
            if(existingCharacter != null)
            {
                throw new Exception("A character with this name already exists.");
            }

            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            return character;
        }

        public async Task<Character> GetCharacter(int id)
        {
            var character = await _context.Characters.Include(c => c.Stats).FirstOrDefaultAsync(c => c.Id == id);
            if(character == null)
            {
                throw new Exception("Character not found.");
            }
            return character;
        }

        public async Task<Character> FightMonster(int characterId , int monsterId)
        {
            var character = await _context.Characters.Include(c => c.Stats).FirstOrDefaultAsync(c => c.Id == characterId);
            if(character == null)
            {
                throw new Exception("Character not found.");
            }

            var monster = await _context.Monsters.Include(m => m.Stats).FirstOrDefaultAsync(m => m.Id == monsterId);
            if(monster == null)
            {
                throw new Exception("Monster not found.");
            }

            //TODO: Maybe change logic this is just easy example 
            //character always wins and gains experience equal to the monster's health points
            character.Stats.ExperiencePoints += monster.Stats.HealthPoints;

            _context.Characters.Update(character);
            await _context.SaveChangesAsync();

            return character;
        }
    }
}
