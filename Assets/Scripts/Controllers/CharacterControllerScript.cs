using System.Collections.Generic;
using UnityEngine;

// Character controller
[RequireComponent(typeof(Rigidbody2D))]
public class CharacterControllerScript : DamagableControllerScript
{

    // Character name
    [SerializeField]
    private string characterName;

    // Is vulnerable
    [SerializeField]
    private bool isVulnerable = true;

    // Move feedback
    [Range(0.0f, 10.0f)]
    [SerializeField]
    private float moveFeedback = 0.75f;

    // Movement speed
    [SerializeField]
    private float movementSpeed = 5.0f;

    // Speed curve
    [SerializeField]
    private AnimationCurve speedCurve;

    // Speeding time
    [SerializeField]
    private float speedingTime = 1.0f;

    // Smooth rotation multiplier
    [SerializeField]
    private float smoothRotationMultiplier = 10.0f;

    // Jump force
    [SerializeField]
    private float jumpForce = 5.0f;

    // Steep degrees
    [SerializeField]
    private float maximumFloorAngle = 45.0f;

    // Death dialogues
    [SerializeField]
    private DialogueObjectScript[] deathDialogues;

    // Character dialogue asset
    [SerializeField]
    private GameObject characterDialogueAsset;

    // Character blah blah clip
    [SerializeField]
    private AudioClip characterBlahBlahClip;

    // Is running
    private bool isRunning = false;

    // Weapons
    public WeaponObjectScript[] weapons;

    // Elapsed speeding time
    private float elapsedSpeedingTime = 0.0f;

    // Floor colliders
    private HashSet<int> floor = new HashSet<int>();

    // Character rigidbody
    private Rigidbody2D characterRigidbody;

    // Sprite renderer
    private SpriteRenderer spriteRenderer;

    // Character animator
    private Animator characterAnimator;

    // Character name
    public string CharacterName
    {
        get
        {
            return characterName;
        }
    }

    // Is vulnerable
    public bool IsVulnerable
    {
        get
        {
            return isVulnerable;
        }
    }

    // Is on floor
    public bool IsOnFloor
    {
        get
        {
            return (floor.Count > 0);
        }
    }
    
    // Weapons
    public WeaponObjectScript[] Weapons
    {
        get
        {
            return weapons;
        }
    }

    // Character rigidbody
    public Rigidbody2D CharacterRigidbody
    {
        get
        {
            return characterRigidbody;
        }
    }

    // Character animator
    public Animator CharacterAnimator
    {
        get
        {
            return characterAnimator;
        }
    }

    // Character dialogue asset
    public GameObject CharacterDialogueAsset
    {
        get
        {
            return characterDialogueAsset;
        }
    }

    // Character blah blah clip
    [SerializeField]
    public AudioClip CharacterBlahBlahClip
    {
        get
        {
            return characterBlahBlahClip;
        }
    }

    // Character death
    public override bool Die()
    {
        bool ret = false;
        if (base.Die())
        {
            if (characterRigidbody != null)
            {
                characterRigidbody.velocity = Vector2.zero;
                characterRigidbody.angularVelocity = 0.0f;
            }
            if ((deathDialogues != null) && (DialogueManagerScript.Instance != null))
            {
                if (deathDialogues.Length > 0)
                {
                    DialogueObjectScript dialogue = Utils.GetRandomElement(deathDialogues);
                    DialogueManagerScript.Instance.ShowDialogue(dialogue, false);
                }
            }
            ret = true;
        }
        return ret;
    }

    // Move character
    protected void Move(Vector2 movement, float mobilityMultiplier = 1.0f)
    {
        if (IsAlive)
        {
            if (GameManagerScript.Instance != null)
            {
                switch (GameManagerScript.Instance.MovementType)
                {
                    case EMovementType.Sidescroller:
                        if (Mathf.Abs(movement.x) >= moveFeedback)
                        {
                            elapsedSpeedingTime = Mathf.Clamp(elapsedSpeedingTime + Time.fixedDeltaTime, 0.0f, speedingTime);
                            CharacterRigidbody.velocity = new Vector2(movement.x * movementSpeed * mobilityMultiplier * speedCurve.Evaluate(elapsedSpeedingTime / speedingTime), CharacterRigidbody.velocity.y);
                            spriteRenderer.flipX = (movement.x < 0.0f);
                            if (!isRunning)
                            {
                                if ((characterAnimator != null) && IsOnFloor)
                                    characterAnimator.Play("Run");
                                isRunning = true;
                            }
                        }
                        else
                        {
                            CharacterRigidbody.velocity = new Vector2(0.0f, CharacterRigidbody.velocity.y);
                            Stop();
                        }
                        break;
                    case EMovementType.TopDown:
                        float mag = movement.magnitude;
                        if (mag >= moveFeedback)
                        {
                            elapsedSpeedingTime = Mathf.Clamp(elapsedSpeedingTime + Time.fixedDeltaTime, 0.0f, speedingTime);
                            Vector2 delta = (movement.normalized * movementSpeed * speedCurve.Evaluate(elapsedSpeedingTime / speedingTime) * Time.fixedDeltaTime);
                            CharacterRigidbody.MovePosition(CharacterRigidbody.position + delta);
                            float angle = Vector2.Angle(Vector2.up, delta);
                            CharacterRigidbody.rotation = Mathf.LerpAngle(CharacterRigidbody.rotation, (delta.x < 0.0f) ? angle : -angle, smoothRotationMultiplier * Time.fixedDeltaTime);
                        }
                        else
                            elapsedSpeedingTime = 0.0f;
                        CharacterRigidbody.velocity = Vector3.zero;
                        break;
                }
            }
        }
    }

    // Stop character
    protected void Stop()
    {
        if (isRunning)
        {
            if ((characterAnimator != null) && IsOnFloor)
                characterAnimator.Play("Idle");
            isRunning = false;
        }
    }

    // Jump character
    protected void Jump()
    {
        if (IsAlive)
        {
            if (GameManagerScript.Instance != null)
            {
                if (GameManagerScript.Instance.MovementType == EMovementType.Sidescroller)
                {
                    if (IsOnFloor)
                    {
                        characterRigidbody.AddRelativeForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                        if (characterAnimator != null)
                            characterAnimator.Play("Jump");
                    }
                }
            }
        }
    }

    // On ground
    public void OnCollide(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            float angle = Vector2.Angle(Vector2.up, contact.normal);
            if (maximumFloorAngle >= angle)
            {
                int id = contact.collider.gameObject.GetInstanceID();
                if (!(floor.Contains(id)))
                {
                    floor.Add(id);
                    if (characterAnimator != null)
                        characterAnimator.Play(isRunning ? "Run" : "Idle");
                }
            }
        }
    }

    // Start
    protected virtual void Start()
    {
        characterRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        characterAnimator = GetComponent<Animator>();
    }

    // On collision enter
    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollide(collision);
    }

    // On collision stay
    private void OnCollisionStay2D(Collision2D collision)
    {
        OnCollide(collision);
    }

    // On collision exit
    private void OnCollisionExit2D(Collision2D collision)
    {
        int id = collision.gameObject.GetInstanceID();
        //uint count = 0U;
        if (floor.Contains(id))
        {
            /*count = floor[id];
            if (count == 0)
                floor.Remove(id);
            else
            {
                --count;
                if (count == 0)
                    floor.Remove(id);
            }*/
            floor.Remove(id);
        }
    }
}
