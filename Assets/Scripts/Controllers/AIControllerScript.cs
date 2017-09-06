using UnityEngine;

// AI controller
public class AIControllerScript : CharacterControllerScript
{
    // Player controller
    private PlayerControllerScript playerController = null;

    // Dialogue object
    [SerializeField]
    private DialogueObjectScript dialogue;

    // Trigger dialogue distance
    [SerializeField]
    private float triggerDialogueDistance = 10.0f;

    // Touch damage
    [Range(0.0f, 10000.0f)]
    [SerializeField]
    private float touchDamage = 10.0f;

    // Damage buff time
    [Range(0.0f, 10000.0f)]
    [SerializeField]
    private float damageBuffTime = 1.0f;

    // Elapsed damage buff time
    private float elapsedDamageBuffTime = 0.0f;

    // Trigger dialogue
    private bool triggerDialog = true;

    // Die
    public override bool Die()
    {
        bool ret = base.Die();
        Destroy(gameObject);
        return ret;
    }

    // Start
    protected override void Start()
    {
        base.Start();
        playerController = FindObjectOfType<PlayerControllerScript>();
    }

    // Fixed update
    protected void FixedUpdate()
    {
        elapsedDamageBuffTime += Time.fixedDeltaTime;
        if (playerController != null)
        {
            if (triggerDialog && (dialogue != null))
            {
                if ((playerController.transform.position - transform.position).sqrMagnitude <= (triggerDialogueDistance * triggerDialogueDistance))
                {
                    if (DialogueManagerScript.Instance != null)
                        DialogueManagerScript.Instance.ShowDialogue(CharacterDialogueAsset, CharacterName, dialogue.Message, blahBlahClip: CharacterBlahBlahClip);
                    triggerDialog = false;
                }
            }
            Vector2 movement = (playerController.CharacterRigidbody.position - CharacterRigidbody.position).normalized;
            Move(movement);
        }
    }

    // On collision enter
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (damageBuffTime < elapsedDamageBuffTime)
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                DamagableControllerScript damagable_controller = contact.collider.GetComponent<DamagableControllerScript>();
                if (CanAttack(damagable_controller))
                {
                    damagable_controller.Damage(touchDamage);
                    elapsedDamageBuffTime = 0.0f;
                }
            }
        }
    }
}
