using csharp_game.Data;
using csharp_game.Models;
using Microsoft.EntityFrameworkCore;

namespace csharp_game.Repository
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly GameContext _context;

        public InventoryRepository(GameContext context)
        {
            _context = context;
        }

        public async Task<Item> AddItemToInventory(int characterId , Item item)
        {
            var character = await _context.Characters.FindAsync(characterId);
            if(character == null)
            {
                throw new Exception("Character not found.");
            }

            character.Inventory.Item.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Item> GetItemFromInventory(int characterId , int itemId)
        {
            var character = await _context.Characters.Include(c => c.Inventory.Item).FirstOrDefaultAsync(c => c.Id == characterId);
            if(character == null)
            {
                throw new Exception("Character not found.");
            }

            var item = character.Inventory.Item.FirstOrDefault(i => i.Id == itemId);
            if(item == null)
            {
                throw new Exception("Item not found in inventory.");
            }

            return item;
        }

        public async Task<Item> UpdateItemInInventory(int characterId , Item updatedItem)
        {
            var character = await _context.Characters.Include(c => c.Inventory.Item).FirstOrDefaultAsync(c => c.Id == characterId);
            if(character == null)
            {
                throw new Exception("Character not found.");
            }

            var item = character.Inventory.Item.FirstOrDefaultAsync(i => i.Id == updatedItem.Id);
            if(item == null)
            {
                throw new Exception("Item not found in inventory.");
            }

            item.Name = updatedItem.ItemName;
            item.Stats = updatedItem.ItemDescription;

            _context.Items.Update(item);
            await _context.SaveChangesAsync();

            return item;
        }
        public async Task DeleteItemFromInventory(int characterId , int itemId)
        {
            var character = await _context.Characters.Include(c => c.Inventory.Item).FirstOrDefaultAsync(c => c.Id == characterId);
            if(character == null)
            {
                throw new Exception("Character not found.");
            }

            var item = character.Inventory.Item.FirstOrDefault(i => i.Id == itemId);
            if(item == null)
            {
                throw new Exception("Item not found in inventory.");
            }

            character.Inventory.Item.Remove(item);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}