using csharp_game.Models;

namespace csharp_game.Repository
{
    public interface IInventoryRepository
    {
        Task<Item> AddItemToInventory(int characterId , Item item);
        Task<Item> GetItemFromInventory(int characterId , int itemId);
        Task<Item> UpdateItemInInventory(int characterId , Item item);
        Task DeleteItemFromInventory(int characterId , int itemId);
    }
}
