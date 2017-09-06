using UnityEngine;

// Tower controller
[RequireComponent(typeof(CircleCollider2D))]
public class TowerControllerScript : DamagableControllerScript
{
    // Aim response time
    [Range(0.0f, 1000.0f)]
    [SerializeField]
    private float aimResponseTime = 1.0f;

    // Enemy proximity
    [Range(0.0f, 10000.0f)]
    [SerializeField]
    private float proximity = 20.0f;

    // Fire time
    [Range(0.0f, 10000.0f)]
    [SerializeField]
    private float fireTime = 1.0f;

    // Projectile quantity
    [Range(1, 100)]
    [SerializeField]
    private uint projectileQuantity = 1U;

    // Projectile speed
    [Range(0.0f, 10000.0f)]
    [SerializeField]
    private float projectileSpeed = 10.0f;

    // Projectile damage
    [Range(0.0f, 10000.0f)]
    [SerializeField]
    private float projectileDamage = 10.0f;

    // Projectile spawn distance
    [Range(0.0f, 1000.0f)]
    [SerializeField]
    private float projectileSpawnDistance = 1.0f;

    // Projectile life time
    [Range(0.0f, 1000.0f)]
    [SerializeField]
    private float projectileLifeTime = 10.0f;

    // Accuracy angle
    [Range(0.0f, 360.0f)]
    [SerializeField]
    private float accuracyAngle;

    // Projectile assets
    [SerializeField]
    private GameObject[] projectileAssets;

    private float elapsedFireTime = 0.0f;

    // Elapsed aim response time
    private float elapsedAimResponseTime = 0.0f;

    // Locked target
    private DamagableControllerScript lockedTarget = null;

    // Locked target
    public DamagableControllerScript LockedTarget
    {
        get
        {
            return lockedTarget;
        }
    }

    // Use this for initialization
    void Start()
    {
        //
    }

    // Shoot
    protected void Shoot()
    {
        if ((lockedTarget != null) && (projectileAssets != null))
        {
            for (uint i = 0U; i != projectileQuantity; i++)
            {
                GameObject asset = Utils.GetRandomElement(projectileAssets);
                if (asset != null)
                {
                    Vector3 direction = (lockedTarget.transform.position - transform.position).normalized;
                    Vector3 d = Utils.DirectionInaccuracy(direction, accuracyAngle);
                    GameObject go = Instantiate(asset, transform.position + (d * projectileSpawnDistance), Quaternion.identity);
                    if (go != null)
                    {
                        ProjectileControllerScript projectile_controller = go.GetComponent<ProjectileControllerScript>();
                        
                        if (projectile_controller == null)
                        {
                            Destroy(go);
                            go = null;
                        }
                        else
                        {
                            Rigidbody rb = go.GetComponent<Rigidbody>();
                            if (rb == null)
                            {
                                Rigidbody2D rb2d = go.GetComponent<Rigidbody2D>();
                                if (rb2d == null)
                                {
                                    Destroy(go);
                                    go = null;
                                }
                                else
                                    rb2d.velocity = d * projectileSpeed;
                            }
                            else
                                rb.velocity = d * projectileSpeed;
                        }
                        if (go != null)
                        {
                            projectile_controller.SetValues(this, projectileDamage);
                            Destroy(go, projectileLifeTime);
                        }
                    }
                }
            }
        }
    }

    // Update
    private void Update()
    {
        if (lockedTarget == null)
        {
            elapsedAimResponseTime += Time.deltaTime;
            while (elapsedAimResponseTime >= aimResponseTime)
            {
                elapsedAimResponseTime -= aimResponseTime;
                DamagableControllerScript[] enemies = FindObjectsOfType<DamagableControllerScript>();
                if (enemies != null)
                {
                    DamagableControllerScript nearest = null;
                    foreach (DamagableControllerScript enemy in enemies)
                    {
                        if (CanAttack(enemy) && ((enemy.transform.position - transform.position).sqrMagnitude <= (proximity * proximity)))
                        {
                            if (nearest == null)
                                nearest = enemy;
                            else if ((enemy.transform.position - transform.position).sqrMagnitude < (nearest.transform.position - transform.position).sqrMagnitude)
                                nearest = enemy;
                        }
                    }
                    lockedTarget = nearest;
                }
            }
        }
        else
        {
            if (lockedTarget.IsAlive)
            {
                elapsedFireTime += Time.deltaTime;
                while (elapsedFireTime >= fireTime)
                {
                    elapsedFireTime -= fireTime;
                    Shoot();
                }
            }
            else
            {
                lockedTarget = null;
                elapsedAimResponseTime = 0.0f;
                elapsedFireTime = 0.0f;
            }
        }
    }

    // On draw gizmos selected
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, proximity);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, projectileSpawnDistance);
    }
}
