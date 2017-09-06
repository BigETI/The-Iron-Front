using UnityEngine;

// Inventory item object
public abstract class InventoryItemObjectScript : ScriptableObject
{
    // Item name
    [SerializeField]
    private string itemName;

    // Display icon
    [SerializeField]
    private Sprite icon;

    // Top down sprite
    [SerializeField]
    private Sprite topDownSprite;

    // Sidescroller sprite
    [SerializeField]
    private Sprite sidescrollerSprite;

    // Item name
    public string ItemName
    {
        get
        {
            return itemName;
        }
    }

    // Display icon
    public Sprite Icon
    {
        get
        {
            return icon;
        }
    }

    // Top down sprite
    public Sprite TopDownSprite
    {
        get
        {
            return topDownSprite;
        }
    }

    // Sidescroller sprite
    public Sprite SidescrollerSprite
    {
        get
        {
            return sidescrollerSprite;
        }
    }

    // Movement type sprite
    public Sprite MovementTypeSprite
    {
        get
        {
            return (GameManagerScript.Instance == null) ? null : ((GameManagerScript.Instance.MovementType == EMovementType.TopDown) ? topDownSprite : sidescrollerSprite);
        }
    }
}
