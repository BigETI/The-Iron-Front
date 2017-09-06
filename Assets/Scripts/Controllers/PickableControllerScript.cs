using UnityEngine;

// Pickable controller
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PickableControllerScript : MonoBehaviour
{
    // Pickable item
    [SerializeField]
    private InventoryItemObjectScript pickableItem;

    // Pickable item
    public InventoryItemObjectScript PickableItem
    {
        get
        {
            return pickableItem;
        }
    }
}

