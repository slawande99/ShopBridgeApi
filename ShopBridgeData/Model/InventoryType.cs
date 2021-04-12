using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridgeData.Model
{
    public class InventoryType
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }        
    }
}
