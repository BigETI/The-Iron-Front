using UnityEngine;

// Player controller script
public class PlayerControllerScript : CharacterControllerScript
{

    // Control sensitivity
    [Range(0.0f, 10.0f)]
    [SerializeField]
    private float controlSensitivity = 1.25f;

    // Use mouse aim
    [SerializeField]
    private bool useMouseAim = true;

    // Weapon sprite renderer
    [SerializeField]
    private SpriteRenderer weaponSpriteRenderer;

    // Projectile fire time
    [Range(0.0f, 10000.0f)]
    [SerializeField]
    private float projectileFireTime = 1.0f;

    // Projectile spawn distance
    [Range(0.0f, 1000.0f)]
    [SerializeField]
    private float projectileSpawnDistance = 1.0f;

    // Movement
    private Vector2 movement = Vector2.zero;

    // Jumping
    private bool jumping = false;

    // Holding weapon index
    private uint holdingWeaponIndex = 0U;

    // Elapsed projectile fire time
    private float elapsedProjectileFireTime = 0.0f;

    // Holding weapon
    private WeaponObjectScript holdingWeapon = null;

    // Projectile damage multiplier
    private float projectileDamageMultiplier = 1.0f;

    // Weapon fire rate multiplier
    private float weaponFireRateMultiplier = 1.0f;

    // Mobility multiplier
    private float mobilityMultiplier = 1.0f;

    // Projectile precision multiplier
    private float projectilePrecisionMultiplier = 1.0f;

    // Weapon reload speed multiplier
    private float weaponReloadSpeedMultiplier = 1.0f;

    // Player camera
    private Camera playerCamera = null;

    public WeaponObjectScript HoldingWeapon
    {
        get
        {
            return holdingWeapon;
        }
    }

    // Die
    public override bool Die()
    {
        bool ret = base.Die();
        //Time.timeScale = 0.0f;
        return ret;
    }

    // Start
    protected override void Start()
    {
        base.Start();
        if (weapons.Length > 0)
            holdingWeapon = weapons[0];
        playerCamera = FindObjectOfType<Camera>();
    }

    // Author: IsaiahKelly
    // URL: http://answers.unity3d.com/questions/798707/2d-look-at-mouse-position-z-rotation-c.html 
    /*void Turning()
    {
        // Distance from camera to object.  We need this to get the proper calculation.
        float camDis = Camera.main.transform.position.y - transform.position.y;

        // Get the mouse position in world space. Using camDis for the Z axis.
        Vector3 mouse = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camDis));

        float angleRad = Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x);
        float angleDeg = (180 / Mathf.PI) * angleRad;

        CharacterRigidbody.rotation = angleDeg - 90f;

    }*/

    // Mouse turn
    private void MouseTurn()
    {
        if (playerCamera != null)
        {
            Vector3 mouse = playerCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, (playerCamera.transform.position.y - transform.position.y)));
            CharacterRigidbody.rotation = ((180.0f * Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x)) / Mathf.PI) - 90.0f;
        }
    }

    // Change weapon
    private void ChangeWeapon(bool next)
    {
        if (InventoryManagerScript.Instance != null)
        {
            WeaponObjectScript[] weapons = InventoryManagerScript.Instance.Weapons;
            if (weapons.Length > 0)
            {
                if (next)
                {
                    ++holdingWeaponIndex;
                    if (holdingWeaponIndex >= weapons.Length)
                        holdingWeaponIndex = 0U;
                }
                else
                {
                    if (holdingWeaponIndex == 0U)
                        holdingWeaponIndex = (uint)(weapons.Length - 1);
                    else
                        --holdingWeaponIndex;
                }
                holdingWeapon = weapons[holdingWeaponIndex];
            }
        }
        UpdateStats();
        UpdateVisuals();
    }

    // Shoot
    public void Shoot()
    {
        if (IsAlive && (holdingWeapon != null))
        {
            if (holdingWeapon.RandomProjectileAsset != null)
            {
                for (uint i = 0U; i != holdingWeapon.ProjectileQuantity; i++)
                {
                    GameObject asset = holdingWeapon.RandomProjectileAsset;
                    if (asset != null)
                    {
                        Vector3 direction = Utils.DirectionInaccuracy(transform.up, holdingWeapon.AccuracyAngle);
                        GameObject go = Instantiate(asset, transform.position + (direction * projectileSpawnDistance), Quaternion.identity);
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
                                        rb2d.velocity = direction * holdingWeapon.ProjectileSpeed;
                                }
                                else
                                {
                                    
                                    rb.velocity = direction * holdingWeapon.ProjectileSpeed;
                                }
                            }
                            if (go != null)
                            {
                                projectile_controller.SetValues(this, holdingWeapon.Damage * projectileDamageMultiplier);
                                Destroy(go, holdingWeapon.ProjectileLifeTime);
                            }
                        }
                    }
                }
            }
        }
    }

    // Update stats
    private void UpdateStats()
    {
        float damage_multiplier = 1.0f;
        float fire_rate_multiplier = 1.0f;
        float mobility_multiplier = 1.0f;
        float precision_multiplier = 1.0f;
        float reload_speed_multiplier = 1.0f;
        if ((InventoryManagerScript.Instance != null) && (holdingWeapon != null))
        {
            foreach (AttachmentObjectScript attachment in InventoryManagerScript.Instance.Attachments)
            {
                if (attachment != null)
                {
                    foreach (AttachmentObjectScript compatible in holdingWeapon.CompatibleAttachments)
                    {
                        if (compatible != null)
                        {
                            if (attachment.name == compatible.name)
                            {
                                foreach (Effect effect in attachment.Effects)
                                {
                                    switch (effect.EffectType)
                                    {
                                        case EEffectType.Damage:
                                            damage_multiplier *= effect.Multiplier;
                                            break;
                                        case EEffectType.FireRate:
                                            fire_rate_multiplier *= effect.Multiplier;
                                            break;
                                        case EEffectType.Mobility:
                                            mobility_multiplier *= effect.Multiplier;
                                            break;
                                        case EEffectType.Precision:
                                            precision_multiplier *= effect.Multiplier;
                                            break;
                                        case EEffectType.ReloadSpeed:
                                            reload_speed_multiplier *= effect.Multiplier;
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            damage_multiplier *= holdingWeapon.Damage;
            fire_rate_multiplier *= holdingWeapon.FireRate;
            mobility_multiplier *= holdingWeapon.Mobility;
        }
        projectileDamageMultiplier = damage_multiplier;
        weaponFireRateMultiplier = fire_rate_multiplier;
        mobilityMultiplier = mobility_multiplier;
        projectilePrecisionMultiplier = precision_multiplier;
        weaponReloadSpeedMultiplier = reload_speed_multiplier;
    }

    // Update visuals
    private void UpdateVisuals()
    {
        if (weaponSpriteRenderer != null)
            weaponSpriteRenderer.sprite = (holdingWeapon == null) ? null : holdingWeapon.MovementTypeSprite;
    }

    // Update
    private void Update()
    {
        if (IsAlive)
        {
            elapsedProjectileFireTime += Time.deltaTime;
            movement = new Vector2(Mathf.Clamp(Input.GetAxisRaw("Horizontal") * controlSensitivity, -1.0f, 1.0f), Mathf.Clamp(Input.GetAxisRaw("Vertical") * controlSensitivity, -1.0f, 1.0f));
            jumping = Input.GetButton("Jump");
            if (Input.GetButtonDown("Inventory"))
            {
                if (InventoryManagerScript.Instance != null)
                    InventoryManagerScript.Instance.IsBackpackOpen = !InventoryManagerScript.Instance.IsBackpackOpen;
            }
            if (Input.GetButton("Fire1"))
            {
                if (elapsedProjectileFireTime >= (projectileFireTime / weaponFireRateMultiplier))
                {
                    elapsedProjectileFireTime = 0.0f;
                    Shoot();
                }
            }
            if (Input.GetButtonDown("ChangeWeapon"))
                ChangeWeapon(true);
        }
    }

    // Fixed update
    private void FixedUpdate()
    {
        if (IsAlive)
        {
            Move(movement, mobilityMultiplier);
            if (useMouseAim && (GameManagerScript.Instance != null))
            {
                if (GameManagerScript.Instance.MovementType == EMovementType.TopDown)
                    MouseTurn();
            }
            if (jumping)
                Jump();
        }
        else
            Stop();
    }

    // On trigger enter 2D
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PickableControllerScript pickable_controller = collision.GetComponent<PickableControllerScript>();
        if (pickable_controller != null)
        {
            InventoryItemObjectScript item = pickable_controller.PickableItem;
            if (item != null)
            {
                if (InventoryManagerScript.Instance != null)
                {
                    if (InventoryManagerScript.Instance.AddItem(item))
                    {
                        if (DialogueManagerScript.Instance != null)
                            DialogueManagerScript.Instance.ShowDialogue(CharacterDialogueAsset, CharacterName, "Looks like I've found a " + item.ItemName.ToLower() + ".", blahBlahClip: CharacterBlahBlahClip);
                    }
                    else
                    {
                        if (DialogueManagerScript.Instance != null)
                            DialogueManagerScript.Instance.ShowDialogue(CharacterDialogueAsset, CharacterName, "I've already got a " + item.ItemName.ToLower() + "...\nI'll leave that for now.", blahBlahClip: CharacterBlahBlahClip);
                    }
                }
            }
            Destroy(collision.gameObject);
        }
    }

    // On draw gizmos selected
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, projectileSpawnDistance);
    }
}
