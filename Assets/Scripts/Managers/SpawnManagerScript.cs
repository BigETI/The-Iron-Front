using UnityEngine;

// Spawn manager script
public class SpawnManagerScript : MonoBehaviour
{
    // Assets
    [SerializeField]
    private GameObject[] assets;

    // Positions
    [SerializeField]
    private Vector2[] positions;

    // Spawn
    public void Spawn()
    {
        foreach (Vector2 position in positions)
        {
            GameObject asset = Utils.GetRandomElement(assets);
            if (asset != null)
            {
                GameObject go = Instantiate(asset);
                if (go != null)
                    go.transform.position = new Vector3(position.x, position.y, 0.0f);
            }
        }
    }

    // On draw gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (positions != null)
        {
            foreach (Vector2 position in positions)
                Gizmos.DrawWireSphere(new Vector3(position.x, position.y, 0.0f), 1.0f);
        }
    }
}
