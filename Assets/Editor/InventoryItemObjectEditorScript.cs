using UnityEditor;
using UnityEngine;

// Inventory item object editor
[CustomEditor(typeof(WeaponObjectScript))]
[CanEditMultipleObjects]
public class InventoryItemObjectEditorScript : Editor
{
    // On inspector GUI
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (InventoryManagerScript.Instance != null)
        {
            if (GUILayout.Button("Add to Inventory"))
                InventoryManagerScript.Instance.AddItem((WeaponObjectScript)target);
        }
        else
            GUILayout.Label("Create an instance to add en item.");
    }
}
