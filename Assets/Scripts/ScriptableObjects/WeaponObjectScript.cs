using System.Collections.Generic;
using UnityEngine;

// Weapon object
[CreateAssetMenu(fileName = "Weapon", menuName = "TheIronFront/Weapon")]
public class WeaponObjectScript : InventoryItemObjectScript
{

    // Fire rate per second
    [Range(0.0f, 10000.0f)]
    [SerializeField]
    private float fireRate;

    // Projectile quantity
    [Range(1, 100)]
    [SerializeField]
    private uint projectileQuantity;

    // Damage per hit
    [Range(0.0f, 10000.0f)]
    [SerializeField]
    private float damage;

    // Projectile speed
    [Range(0.0f, 10000.0f)]
    [SerializeField]
    private float projectileSpeed;

    // Mobility
    [Range(0.0f, 10000.0f)]
    [SerializeField]
    private float mobility;

    // Accuracy angle
    [Range(0.0f, 360.0f)]
    [SerializeField]
    private float accuracyAngle;

    // Projectile life time
    [Range(0.0f, 1000.0f)]
    [SerializeField]
    private float projectileLifeTime;

    // Compatible attachments
    [SerializeField]
    private AttachmentObjectScript[] compatibleAttachments;

    // Projectile assets
    [SerializeField]
    private GameObject[] projectileAssets;

    // Fire rate per second
    public float FireRate
    {
        get
        {
            return fireRate;
        }
    }

    // Projectile quantity
    public uint ProjectileQuantity
    {
        get
        {
            return projectileQuantity;
        }
    }

    // Damage per hit
    public float Damage
    {
        get
        {
            return damage;
        }
    }

    // Projectile speed
    public float ProjectileSpeed
    {
        get
        {
            return projectileSpeed;
        }
    }

    // Mobility
    public float Mobility
    {
        get
        {
            return mobility;
        }
    }

    // Accuracy angle
    public float AccuracyAngle
    {
        get
        {
            return accuracyAngle;
        }
    }

    // Projectile life time
    public float ProjectileLifeTime
    {
        get
        {
            return projectileLifeTime;
        }
    }

    // Compatible upgrades
    public IEnumerable<AttachmentObjectScript> CompatibleAttachments
    {
        get
        {
            return compatibleAttachments;
        }
    }

    // Projectile assets
    public IEnumerable<GameObject> ProjectileAssets
    {
        get
        {
            return projectileAssets;
        }
    }

    // Random projectile asset
    public GameObject RandomProjectileAsset
    {
        get
        {
            return Utils.GetRandomElement(projectileAssets);
        }
    }
}
