using System.Collections.Generic;
using UnityEngine;

// Inventory manager
public class InventoryManagerScript : MonoBehaviour
{

    // Closed backpack
    [SerializeField]
    private GameObject closedBackpack;

    // Opened backpack
    [SerializeField]
    private GameObject openedBackpack;

    // Inventory item slots
    [SerializeField]
    private InventoryItemSlotControllerScript[] inventoryItemSlots;

    // Is backpack open
    private bool isBackpackOpen = false;

    // Instance reference
    private static InventoryManagerScript instance = null;

    // Inventory
    private InventoryItemObjectScript[] inventoryItems = null;

    // Is backpack open
    public bool IsBackpackOpen
    {
        get
        {
            return isBackpackOpen;
        }
        set
        {
            isBackpackOpen = value;
            (isBackpackOpen ? closedBackpack : openedBackpack).SetActive(false);
            (isBackpackOpen ? openedBackpack : closedBackpack).SetActive(true);
        }
    }

    // Instance reference
    public static InventoryManagerScript Instance
    {
        get
        {
            return instance;
        }
    }

    // Weapons
    public WeaponObjectScript[] Weapons
    {
        get
        {
            return PlayerProgress.Instance.Weapons;
        }
    }

    // Attachments
    public AttachmentObjectScript[] Attachments
    {
        get
        {
            return PlayerProgress.Instance.Attachments;
        }
    }

    // Fortress items
    public FortressItemObjectScript[] FortressItems
    {
        get
        {
            return PlayerProgress.Instance.FortressItems;
        }
    }

    // Inventory items
    public InventoryItemObjectScript[] InventoryItems
    {
        get
        {
            if (inventoryItems == null)
                inventoryItems = PlayerProgress.Instance.AllInventoryItems;
            return inventoryItems;
        }
    }

    // Add item
    public bool AddItem(InventoryItemObjectScript inventoryItem)
    {
        bool ret = false;
        if (inventoryItem != null)
        {
            List<InventoryItemObjectScript> items = new List<InventoryItemObjectScript>(PlayerProgress.Instance.AllInventoryItems);
            bool success = true;
            foreach (InventoryItemObjectScript item in items)
            {
                if (item == inventoryItem)
                {
                    success = false;
                    break;
                }
            }
            if (success)
            {
                items.Add(inventoryItem);
                inventoryItems = items.ToArray();
                PlayerProgress.Instance.AllInventoryItems = inventoryItems;
                UpdateVisuals();
                ret = true;
            }
        }
        return ret;
    }

    // Drop item
    public void DropItem(InventoryItemSlotControllerScript inventoryItemSlot)
    {
        foreach (InventoryItemSlotControllerScript item_slot in inventoryItemSlots)
        {
            if (item_slot == inventoryItemSlot)
            {
                // Vector3 position = ((playerController == null) ? Vector3.zero : playerController.transform.position) + (((Random.Range(0, 2) == 0) ? Vector3.left : Vector3.right) * 20.0f);
                inventoryItemSlot.Item = null;
                PlayerControllerScript player_controller = FindObjectOfType<PlayerControllerScript>();
                if (player_controller != null)
                {
                    if (DialogueManagerScript.Instance != null)
                        DialogueManagerScript.Instance.ShowDialogue(player_controller.CharacterDialogueAsset, player_controller.CharacterName, "Actually, I don't need this item anymore.", blahBlahClip: player_controller.CharacterBlahBlahClip);
                }
                break;
            }
        }
    }

    // Update visuals
    private void UpdateVisuals()
    {
        uint c = 0U;
        foreach (InventoryItemSlotControllerScript item_slot in inventoryItemSlots)
            item_slot.Item = null;
        InventoryItemObjectScript[] items = InventoryItems;
        foreach (InventoryItemObjectScript item in items)
        {
            if (c >= inventoryItemSlots.Length)
                break;
            inventoryItemSlots[c].Item = item;
            ++c;
        }
    }

    // On enable
    private void OnEnable()
    {
        if (instance == null)
            instance = this;
    }

    // On disable
    private void OnDisable()
    {
        if (instance == this)
            instance = null;
    }

    // Start
    private void Start()
    {
        UpdateVisuals();
    }
}
