using ShopBridge.Web.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopBridge.Web
{
    /// <summary>
    /// Intventory View Model
    /// </summary>
    public class InventoryViewModel
    {       
        public InventoryViewModel()
        {
            this.InventoryTypes = new Dictionary<int, string>();
            InventoryTypes.Add((int)InventoryTypeLookup.RawMaterials, InventoryTypeLookup.RawMaterials.ToString());
            InventoryTypes.Add((int)InventoryTypeLookup.WorkInProgress, InventoryTypeLookup.WorkInProgress.ToString());
            InventoryTypes.Add((int)InventoryTypeLookup.FinishedGoods, InventoryTypeLookup.FinishedGoods.ToString());
            InventoryTypes.Add((int)InventoryTypeLookup.PackingMaterial, InventoryTypeLookup.PackingMaterial.ToString());
            InventoryTypes.Add((int)InventoryTypeLookup.Supplies, InventoryTypeLookup.Supplies.ToString());   
        }
        public int InventoryID { get; set; }     
        [Required]  
        public string Name { get; set; }       
        [Required]
        public string Description { get; set; }       
        [Required]
        public float Price { get; set; }
        public string ImagePath { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateIn { get; set; }
        public int InventoryTypeId { get; set; }
        public string InventoryTypeName {
            get {
                   var name = Enum.GetName(typeof(InventoryTypeLookup), this.InventoryTypeId);
                   var raw = (InventoryTypeLookup)Enum.Parse(typeof(InventoryTypeLookup), name);                  
                   return raw.GetDescription<InventoryTypeLookup>();
               }
        }

        public Dictionary<int, string> InventoryTypes { get; set; }
    }  
}
