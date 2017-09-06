using UnityEngine;

// Game manager
public class GameManagerScript : MonoBehaviour
{

    // Instance reference
    private static GameManagerScript instance = null;

    // Movement type
    [SerializeField]
    private EMovementType movementType;

    // Instance reference
    public static GameManagerScript Instance
    {
        get
        {
            return instance;
        }
    }

    // Movement type
    public EMovementType MovementType
    {
        get
        {
            return movementType;
        }
    }

    // On enable
    private void OnEnable()
    {
        if (instance == null)
            instance = this;
    }

    // On disable
    private void OnDisable()
    {
        if (instance == this)
            instance = null;
    }
}
