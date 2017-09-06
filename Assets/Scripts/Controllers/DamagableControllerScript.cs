using UnityEngine;
using UnityEngine.Events;

public class DamagableControllerScript : MonoBehaviour
{
    // Health
    [SerializeField]
    public float health = 100.0f;

    // Team
    [SerializeField]
    private ETeam team = ETeam.Neutral;

    // Character dialogue asset
    [SerializeField]
    private UnityEvent onDie;

    // Is alive
    private bool isAlive = true;

    // Health
    public float Health
    {
        get
        {
            return health;
        }
    }

    // Team
    public ETeam Team
    {
        get
        {
            return team;
        }
    }

    // Is alive
    public bool IsAlive
    {
        get
        {
            return isAlive;
        }
    }

    // Can attack
    public bool CanAttack(DamagableControllerScript damagableController)
    {
        bool ret = false;
        if (damagableController != null)
            ret = ((this == damagableController) ? false : ((damagableController.Team == ETeam.Neutral) ? true : Team != damagableController.Team));
        return ret;
    }

    // Damage character
    public void Damage(float amount)
    {
        if ((amount > 0.0f) && (health > 0.0f))
        {
            health -= amount;
            if (health <= 0.0f)
                Die();
        }
    }

    // Die
    public virtual bool Die()
    {
        bool ret = false;
        if (isAlive)
        {
            isAlive = false;
            ret = true;
            onDie.Invoke();
        }
        return ret;
    }
}
