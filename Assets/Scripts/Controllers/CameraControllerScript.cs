using UnityEngine;

// Camera controller
[RequireComponent(typeof(Camera))]
public class CameraControllerScript : MonoBehaviour
{

    // Smooth movement multiplier
    [SerializeField]
    private float smoothMovementMultiplier = 5.0f;

    // Minimum scroll size
    [SerializeField]
    private float minimumScrollSize = 2.0f;

    // Maximum scroll size
    [SerializeField]
    private float maximumScrollSize = 10.0f;

    // Scroll size
    [SerializeField]
    private float scrollSize = 5.0f;

    // Scroll speed
    [SerializeField]
    private float scrollSpeed = 1.0f;

    // Smooth scroll multiplier
    [SerializeField]
    private float smoothScrollMultiplier = 5.0f;

    // Camera offset
    [SerializeField]
    private Vector3 cameraOffset;

    // This camera
    private Camera thisCamera;

    // Player controller
    private PlayerControllerScript playerController = null;

    // Start
    private void Start()
    {
        thisCamera = GetComponent<Camera>();
        playerController = FindObjectOfType<PlayerControllerScript>();
    }

    // Update
    protected virtual void Update()
    {
        float scroll = -Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.0f)
            scrollSize = Mathf.Clamp(scrollSize + (scroll * scrollSpeed), minimumScrollSize, maximumScrollSize);
    }

    // FixedUpdate
    protected virtual void FixedUpdate()
    {
        if (thisCamera != null)
        {
            if (thisCamera.orthographic)
            {
                transform.position = Vector3.Lerp(transform.position + cameraOffset, playerController.transform.position + cameraOffset, smoothMovementMultiplier * Time.fixedDeltaTime);
                thisCamera.orthographicSize = Mathf.Lerp(thisCamera.orthographicSize, scrollSize, smoothScrollMultiplier * Time.fixedDeltaTime);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position + cameraOffset, playerController.transform.position + cameraOffset + (Vector3.back * scrollSize), smoothMovementMultiplier * Time.fixedDeltaTime);
            }
        }
    }
}
