using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridgeData.Repository
{ 
    public interface IInventoryRepository
    {
        IEnumerable<Inventory> GetAllInventories();
        Inventory GetInventoryById(int Id);
        void AddInventory(Inventory inventory);
        void UpdateInventory(Inventory inventory);
        void DeleteInvetory(int Id);

    }


    /// <summary>
    /// Inventory Repository
    /// </summary>
    public class InventoryRepository : BaseRepository<Inventory>, IInventoryRepository
    {
        private readonly BaseRepository<Inventory> inventoryRepository;
        public InventoryRepository()
        {
            inventoryRepository = new BaseRepository<Inventory>();
        }

        /// <summary>
        /// Add new inventory
        /// </summary>
        /// <param name="inventory">Inventory to add</param>
        public void AddInventory(Inventory inventory)
        {
            inventoryRepository.Add(inventory);
        }

        /// <summary>
        /// Delete inventory by Id
        /// </summary>
        /// <param name="Id">ID to delete</param>
        public void DeleteInvetory(int Id)
        {
           inventoryRepository.Delete(Id);
        }

        /// <summary>
        /// Get all inventories record
        /// </summary>
        /// <returns>Returns collection of inventories</returns>
       public IEnumerable<Inventory> GetAllInventories()
        {
           return inventoryRepository.GetAll();
        }

        /// <summary>
        /// Get inventory record by ID
        /// </summary>
        /// <param name="Id">Id to fetch record</param>
        /// <returns>Return matching record</returns>
        public Inventory GetInventoryById(int Id)
        {
         return  inventoryRepository.GetById(Id);
        }

        /// <summary>
        /// Update inventory
        /// </summary>
        /// <param name="inventory">Inventory record to update</param>
        public void UpdateInventory(Inventory inventory)
        {
            inventoryRepository.Update(inventory);
        }
    }
}
