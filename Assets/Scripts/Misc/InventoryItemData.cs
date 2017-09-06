using System;
using UnityEngine;

// Inventory item data (serializable)
[Serializable]
public class InventoryItemData
{

    // Resource name
    [SerializeField]
    private string resourceName;

    // Item type
    [SerializeField]
    private EItemType itemType;

    // Inventory item object
    private InventoryItemObjectScript item = null;

    // Inventory item object
    public InventoryItemObjectScript Item
    {
        get
        {
            if (item == null)
            {
                Type type = null;
                switch (itemType)
                {
                    case EItemType.Weapon:
                        type = typeof(WeaponObjectScript);
                        break;
                    case EItemType.Attachment:
                        type = typeof(AttachmentObjectScript);
                        break;
                    case EItemType.FortressItem:
                        type = typeof(FortressItemObjectScript);
                        break;
                }
                if (type != null)
                    item = Resources.Load(itemType.ToString() + "s/" + resourceName, type) as InventoryItemObjectScript;
            }
            return item;
        }
    }

    // Default constructor
    public InventoryItemData()
    {
        //
    }

    // Constructor
    public InventoryItemData(InventoryItemObjectScript item)
    {
        if (item != null)
        {
            if (item is WeaponObjectScript)
                itemType = EItemType.Weapon;
            else if (item is AttachmentObjectScript)
                itemType = EItemType.Attachment;
            else if (item is FortressItemObjectScript)
                itemType = EItemType.FortressItem;
            resourceName = item.name;
            this.item = item;
        }
    }
}
