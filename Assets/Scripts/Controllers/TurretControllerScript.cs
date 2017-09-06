using UnityEngine;

// Turret controller
public class TurretControllerScript : TowerControllerScript
{
    // Turn smooth time
    [Range(0.0f, 1000.0f)]
    [SerializeField]
    private float turnSmoothTime = 1.0f;

    // Turret transform
    [SerializeField]
    private Transform turretTransform;

    // Fixed update
    private void FixedUpdate()
    {
        if (LockedTarget != null)
        {
            Transform t = (turretTransform == null) ? transform : turretTransform;
            t.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Lerp(t.rotation.eulerAngles.z, Vector3.Angle(Vector3.up, LockedTarget.transform.position - t.position), turnSmoothTime * Time.fixedDeltaTime));
        }
    }
}
