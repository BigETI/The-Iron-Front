using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Player progress class
[Serializable]
public class PlayerProgress
{

    // Scrap metal
    [SerializeField]
    private uint scrapMetal = 0U;

    // Inventory item data
    [SerializeField]
    private InventoryItemData[] inventoryItemData = new InventoryItemData[0];

    // Instance reference
    private static PlayerProgress instance = null;

    // Scrap metal
    public uint ScrapMetal
    {
        get
        {
            return scrapMetal;
        }
    }

    // Recommended file name
    public static string RecommendedFileName
    {
        get
        {
            return Application.persistentDataPath + "/player-progress.json";
        }
    }

    // Instance reference
    public static PlayerProgress Instance
    {
        get
        {
            if (instance == null)
                instance = Load(RecommendedFileName);
            return instance;
        }
    }

    // All inventory items
    public InventoryItemObjectScript[] AllInventoryItems
    {
        get
        {
            List<InventoryItemObjectScript> items = new List<InventoryItemObjectScript>();
            foreach (InventoryItemData item_data in inventoryItemData)
            {
                if (item_data != null)
                {
                    InventoryItemObjectScript item = item_data.Item;
                    if (item != null)
                        items.Add(item);
                }
            }
            return items.ToArray();
        }
        set
        {
            if (value == null)
                inventoryItemData = new InventoryItemData[0];
            else
            {
                inventoryItemData = new InventoryItemData[value.Length];
                for (int i = 0; i < value.Length; i++)
                    inventoryItemData[i] = new InventoryItemData(value[i]);
            }
        }
    }

    // Weapons
    public WeaponObjectScript[] Weapons
    {
        get
        {
            return GetInventoryItemsByType<WeaponObjectScript>();
        }
    }

    // Attachments
    public AttachmentObjectScript[] Attachments
    {
        get
        {
            return GetInventoryItemsByType<AttachmentObjectScript>();
        }
    }

    // Fortress items
    public FortressItemObjectScript[] FortressItems
    {
        get
        {
            return GetInventoryItemsByType<FortressItemObjectScript>();
        }
    }

    // Get inventory item by type
    public T[] GetInventoryItemsByType<T>() where T : InventoryItemObjectScript
    {
        List<T> items = new List<T>();
        foreach (InventoryItemData item_data in inventoryItemData)
        {
            if (item_data != null)
            {
                InventoryItemObjectScript item = item_data.Item;
                if (item is T)
                    items.Add((T)item);
            }
        }
        return items.ToArray();
    }
    
    // Load from file
    private static PlayerProgress Load(string fileName)
    {
        PlayerProgress ret = new PlayerProgress();
        PlayerProgress pp = null;
        if (File.Exists(fileName))
        {
            try
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    pp = JsonUtility.FromJson<PlayerProgress>(reader.ReadToEnd());
                    if (pp != null)
                        ret = pp;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
        return ret;
    }

    // Save player progress to file
    public void Save(string fileName)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.Write(JsonUtility.ToJson(this));
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    // Save player progress
    public static void Save()
    {
        Instance.Save(RecommendedFileName);
    }
}
