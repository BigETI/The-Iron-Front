using UnityEngine;
using UnityEngine.UI;

// Weapon text controller
public class WeaponTextControllerScript : MonoBehaviour
{
    // Weapon text
    private Text weaponText = null;

    // Player controller
    private PlayerControllerScript playerController = null;

    // Last holding weapon
    private WeaponObjectScript lastHoldingWeapon = null;

    // Update visuals
    private void UpdateVisuals()
    {
        if (weaponText != null)
            weaponText.text = (lastHoldingWeapon == null) ? "Press \"Q\" to select a weapon." : ("Holding: " + lastHoldingWeapon.ItemName + "\nPress \"Q\" to change");
    }

    // Use this for initialization
    void Start()
    {
        weaponText = GetComponent<Text>();
        playerController = FindObjectOfType<PlayerControllerScript>();
        if (playerController != null)
            lastHoldingWeapon = playerController.HoldingWeapon;
        UpdateVisuals();
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerController != null)
        {
            if (playerController.HoldingWeapon != lastHoldingWeapon)
            {
                lastHoldingWeapon = playerController.HoldingWeapon;
                UpdateVisuals();
            }
        }
    }
}
