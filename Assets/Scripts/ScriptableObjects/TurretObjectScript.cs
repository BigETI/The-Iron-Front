using UnityEngine;

// Turret object
[CreateAssetMenu(fileName = "Turret", menuName = "TheIronFront/Turret")]
public class TurretObjectScript : FortressItemObjectScript
{
    // Fire rate per second
    [Range(0.0f, 10000.0f)]
    [SerializeField]
    private float fireRate;

    // Projectile asset
    [SerializeField]
    private GameObject projectileAsset;

    // Fire rate per second
    public float FireRate
    {
        get
        {
            return fireRate;
        }
    }

    // Projectile asset
    public GameObject ProjectileAsset
    {
        get
        {
            return projectileAsset;
        }
    }
}
