using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopBridgeApi.Controllers;
using ShopBridgeApi.Tests.Controllers;
using ShopBridgeData;
using ShopBridgeData.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace ShopBridgeApi.Tests
{
    [TestClass]
    public class InventoryControllerTest
    {
        private readonly IInventoryRepository _inventoryRepository;
        [TestMethod]
        public void Get()
        {
            var testInventories = GetTestInventories();
            var controller = new DummyInventoryRepository(testInventories);

            var result = controller.GetAllInventories() as List<Inventory>;
            Assert.AreEqual(testInventories.Count, result.Count);
        }

        [TestMethod]
        public void GetInventory_ShouldReturnCorrectInventory()
        {
            var testInventories = GetTestInventories();
            var controller = new DummyInventoryRepository(testInventories);

            var result = controller.GetInventoryById(1) as Inventory;
            Assert.IsNotNull(result);
            Assert.AreEqual(testInventories[0].Name, result.Name);
        }

        [TestMethod]
        public void GetInventory_ShouldNotFindInventory()
        {
            var controller = new DummyInventoryRepository(GetTestInventories());

            var result = controller.GetInventoryById(999) as Inventory; ;
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Post()
        {
            //// Arrange
            //var controller = new InventoryController(_inventoryRepository);
            //// Act
            var inventory = new Inventory(){
                InventoryID = 2,
                Name = "Lenova laptop",
                Description = "New Lenova laptop",
                Price = 25000,
                ImagePath = "~/Images/HeadPhone.jfif",
                IsActive = true,
                DateIn = DateTime.Now,
                InventoryTypeId = 2
            } ;
            //controller.Post(inventory);
            //// Assert
            var controller = new DummyInventoryRepository(GetTestInventories());

            controller.AddInventory(inventory);
            
        }

        [TestMethod]
        public void Put()
        {
            // Arrange
           // var controller = new InventoryController(_inventoryRepository);
            // Act
            var inventory = new Inventory() {
                InventoryID = 1,
                Name = "Lenova laptop",
                Description = "New Lenova laptop",
                Price = 25000,
                ImagePath = "~/Images/HeadPhone.jfif",
                IsActive = true,
                DateIn = DateTime.Now,
                InventoryTypeId = 2
            };

           //  controller.Put(2, inventory);

            // Assert
            var controller = new DummyInventoryRepository(GetTestInventories());

            controller.UpdateInventory(inventory);
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            var controller = new DummyInventoryRepository(GetTestInventories());

            // Act
            controller.DeleteInvetory(2);

            // Assert
        }

        private List<Inventory> GetTestInventories()
        {
            List<Inventory> inventories;
            inventories = new List<Inventory>() {
            new Inventory(){InventoryID =1, Name="Lenova laptop", Description="New Lenova laptop", Price=25000,ImagePath="~/Images/HeadPhone.jfif",
                            IsActive = true,DateIn = DateTime.Now,InventoryTypeId = 2},
                     };
            return inventories;
        }

    }
}
