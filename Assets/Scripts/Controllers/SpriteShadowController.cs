using UnityEngine;

// Sprite shadow controller
public class SpriteShadowController : MonoBehaviour
{

    // Shadow sprite renderer
    [SerializeField]
    private SpriteRenderer shadowSpriteRenderer;

    // Max shadow distance
    [Range(0.0f, 10000.0f)]
    [SerializeField]
    private float maxShadowDistance = 100.0f;

    // Fixed update
    private void LateUpdate()
    {
        if (shadowSpriteRenderer != null)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(new Vector2(transform.position.x, transform.position.y), Vector3.down, maxShadowDistance);
            float nearest_distance = 0.0f;
            int nearest_index = -1;
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                if (hit.collider.tag != "Entity")
                {
                    if (nearest_index < 0)
                    {
                        nearest_index = i;
                        nearest_distance = hit.distance;
                    }
                    else if (hit.distance < nearest_distance)
                    {
                        nearest_index = i;
                        nearest_distance = hit.distance;
                    }
                }
            }
            if (nearest_index >= 0)
            {
                RaycastHit2D hit = hits[nearest_index];
                shadowSpriteRenderer.transform.position = new Vector3(hit.point.x, hit.point.y + 0.03125f, transform.position.z);
            }
        }
    }
}
