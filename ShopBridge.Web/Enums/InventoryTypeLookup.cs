using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ShopBridge.Web.Enums
{
    public enum InventoryTypeLookup
    {
        [Description("Raw Materials")]
        RawMaterials = 1,
        [Description("Work In Progress")]
        WorkInProgress = 2 ,
        [Description("Finished Goods")]
        FinishedGoods =3,
        [Description("Packing Material")]
        PackingMaterial =4,
        [Description("Supplies")]
        Supplies = 5
    }
}