using UnityEngine;

// Attachment object
[CreateAssetMenu(fileName = "Attachment", menuName = "TheIronFront/Attachment")]
public class AttachmentObjectScript : InventoryItemObjectScript
{
    // Effects type
    [SerializeField]
    private Effect[] effects;

    // Effect type
    public Effect[] Effects
    {
        get
        {
            return effects;
        }
    }
}
