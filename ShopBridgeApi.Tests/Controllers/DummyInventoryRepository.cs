using ShopBridgeData.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopBridgeData;

namespace ShopBridgeApi.Tests.Controllers
{
    public class DummyInventoryRepository : IInventoryRepository
    {
        // Master list of books that will mimic the persitent database storage
        List<Inventory> m_inventories = null;

        public DummyInventoryRepository(List<Inventory> inventories)
        {
            m_inventories = inventories;
        }

        public void AddInventory(Inventory inventory)
        {
            m_inventories.Add(inventory);
        }

        public void DeleteInvetory(int Id)
        {
           var inventory1 =  m_inventories.SingleOrDefault(inventory => inventory.InventoryID == Id);
            m_inventories.Remove(inventory1);
        }

        public IEnumerable<Inventory> GetAllInventories()
        {
            return m_inventories;
        }

        public Inventory GetInventoryById(int Id)
        {
            return m_inventories.SingleOrDefault(inventory => inventory.InventoryID == Id);
        }

        public void UpdateInventory(Inventory inventory)
        {
            int id = inventory.InventoryID;
            Inventory inventoryToUpdate = m_inventories.SingleOrDefault(b => b.InventoryID == id);
            DeleteInvetory(inventoryToUpdate.InventoryID);
            m_inventories.Add(inventory);
        }
    }
}
