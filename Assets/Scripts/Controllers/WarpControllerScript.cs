using UnityEngine;
using UnityEngine.SceneManagement;

// Warp controller script
public class WarpControllerScript : MonoBehaviour
{
    // Trigger distance
    [SerializeField]
    private float triggerDistance = 2.0f;

    // Warp type
    [SerializeField]
    private EWarpType warpType;

    // Is two sided
    [SerializeField]
    private bool isTwoSided;

    // Location
    [SerializeField]
    private Vector3 location;

    // Scene name
    [SerializeField]
    private string sceneName;

    // Warp indicator asset
    [SerializeField]
    private GameObject warpIndicatorAsset;

    // Player controller
    private PlayerControllerScript playerController;

    // Is at warp
    private bool isAtWarp = false;

    // Is at other warp
    private bool isAtOtherWarp = false;

    // To location
    public Vector3 ToLocation
    {
        get
        {
            Vector3 ret = Vector3.zero;
            switch (warpType)
            {
                case EWarpType.Absolute:
                    ret = location;
                    break;
                case EWarpType.Relative:
                    ret = transform.position + location;
                    break;
                case EWarpType.Move:
                    if (playerController == null)
                        ret = transform.position + location;
                    else
                        ret = playerController.transform.position + location;
                    break;
            }
            return ret;
        }
    }

    // Warp
    private void Warp()
    {
        if (warpType == EWarpType.Scene)
            SceneManager.LoadScene(sceneName);
        else if (playerController != null)
            playerController.transform.position = ToLocation;
    }

    // Warp back
    private void WarpBack()
    {
        if ((warpType != EWarpType.Scene) && (warpType != EWarpType.Move))
        {
            if (playerController != null)
                playerController.transform.position = transform.position;
        }
    }

    // Start
    private void Start()
    {
        playerController = FindObjectOfType<PlayerControllerScript>();
        if (playerController == null)
            Debug.LogError("playerController: null");
        if (warpIndicatorAsset != null)
        {
            Instantiate(warpIndicatorAsset, transform.position, Quaternion.identity);
            if ((warpType != EWarpType.Scene) && (warpType != EWarpType.Move))
                Instantiate(warpIndicatorAsset, ToLocation, Quaternion.identity);
        }
    }

    // Update
    private void Update()
    {
        if (playerController != null)
        {
            if (isTwoSided)
                isAtOtherWarp = ((playerController.transform.position - ToLocation).sqrMagnitude <= (triggerDistance * triggerDistance));
            isAtWarp = ((playerController.transform.position - transform.position).sqrMagnitude <= (triggerDistance * triggerDistance));
            if (Input.GetButtonDown("Interact"))
            {
                if (isAtWarp)
                    Warp();
                else if (isAtOtherWarp)
                    WarpBack();
            }
        }
    }

    // On draw gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, triggerDistance);
        if (warpType != EWarpType.Scene)
        {
            Gizmos.color = Color.blue;
            Vector3 to = ToLocation;
            Gizmos.DrawWireSphere(to, triggerDistance);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, to);
        }
    }
}
