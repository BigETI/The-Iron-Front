using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Spawn manager
public class WaveSpawnManagerScript : MonoBehaviour
{
    // Waves
    [SerializeField]
    private WaveObjectScript[] waves;

    // Distance to spawn at
    [SerializeField]
    private float distance;

    // On waves end
    [SerializeField]
    private UnityEvent onWavesEnd;

    // Current wave index
    private uint currentWaveIndex = 0U;

    // Currently spawned wave
    private List<DamagableControllerScript> damagables = new List<DamagableControllerScript>();

    // Waves active
    private bool wavesActive = true;

    // Instance reference
    private static WaveSpawnManagerScript instance = null;

    // Instance reference
    public WaveSpawnManagerScript Instance
    {
        get
        {
            return instance;
        }
    }

    // Clear wave
    private void ClearWave()
    {
        foreach (DamagableControllerScript damagable in damagables)
        {
            if (damagable != null)
                Destroy(damagable.gameObject);
        }
        damagables.Clear();
    }

    // Spawn next wave
    private void SpawnNextWave()
    {
        if (waves != null)
        {
            if (currentWaveIndex < waves.Length)
            {
                ClearWave();
                WaveObjectScript wave = waves[currentWaveIndex];
                for (uint i = 0U; i != wave.Quantity; i++)
                {
                    GameObject asset = wave.RandomAsset;
                    if (asset != null)
                    {
                        Vector2 position = new Vector2(transform.position.x, transform.position.y) + ((new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f))).normalized * distance);
                        GameObject go = Instantiate(asset, new Vector3(position.x, position.y, 0.0f), Quaternion.identity);
                        if (go != null)
                        {
                            DamagableControllerScript damagable_controller = go.GetComponent<DamagableControllerScript>();
                            if (damagable_controller != null)
                                damagables.Add(damagable_controller);
                        }
                    }
                }
                ++currentWaveIndex;
            }
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

    // Update
    private void Update()
    {
        if (waves != null)
        {
            if (currentWaveIndex < waves.Length)
            {
                bool wave_over = true;
                foreach (DamagableControllerScript damagable in damagables)
                {
                    if (damagable.IsAlive)
                    {
                        wave_over = false;
                        break;
                    }
                }
                if (wave_over)
                    SpawnNextWave();
            }
            else if (wavesActive)
            {
                bool wave_over = true;
                foreach (DamagableControllerScript damagable in damagables)
                {
                    if (damagable.IsAlive)
                    {
                        wave_over = false;
                        break;
                    }
                }
                if (wave_over)
                {
                    wavesActive = false;
                    onWavesEnd.Invoke();
                }
            }
        }
    }

    // On draw gizmos selected
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 1.0f);
    }
}
