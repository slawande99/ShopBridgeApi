using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ShopBridgeData.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopBridgeData
{
    /// <summary>
    /// Inventory model
    /// </summary>
    public class Inventory
    {
        [Key]
        public int InventoryID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public string ImagePath { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public DateTime DateIn { get; set; }

        [ForeignKey("InventoryType")]
        public int InventoryTypeId { get; set; }

        public virtual InventoryType InventoryType { get; set; }

     }
}
