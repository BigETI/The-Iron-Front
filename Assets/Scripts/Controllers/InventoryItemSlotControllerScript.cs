using UnityEngine;
using UnityEngine.UI;

// Inventory item slot
public class InventoryItemSlotControllerScript : MonoBehaviour
{

    // Remove button
    [SerializeField]
    private GameObject removeButton;

    // Icon image
    [SerializeField]
    private Image iconImage = null;

    // Item
    private InventoryItemObjectScript item = null;
    
    // Item
    public InventoryItemObjectScript Item
    {
        get
        {
            return item;
        }
        set
        {
            item = value;
            UpdateVisuals();
        }
    }

    // Drop item
    public void DropItem()
    {
        Debug.Log("DropItem 0");
        if (item != null)
        {
            Debug.Log("DropItem 1");
            if (InventoryManagerScript.Instance != null)
                InventoryManagerScript.Instance.DropItem(this);
            else
                Debug.Log("DropItem 2");
        }
    }

    // Update visuals
    private void UpdateVisuals()
    {
        if (removeButton != null)
            removeButton.SetActive(item != null);
        if (iconImage != null)
        {
            iconImage.color = (item == null) ? (new Color(0.0f, 0.0f, 0.0f, 0.0f)) : Color.white;
            iconImage.sprite = (item == null) ? null : item.Icon;
        }
    }
}
