using UnityEngine;

// Wave object
[CreateAssetMenu(fileName = "Wave", menuName = "TheIronFront/Wave")]
public class WaveObjectScript : ScriptableObject
{

    // Wave name
    [SerializeField]
    private string waveName;

    // Quantity
    [SerializeField]
    private uint quantity;

    // Assets
    [SerializeField]
    private GameObject[] assets;

    // Wave name
    public string WaveName
    {
        get
        {
            return WaveName;
        }
    }

    // Quantity
    public uint Quantity
    {
        get
        {
            return quantity;
        }
    }

    // Assets
    public GameObject[] Assets
    {
        get
        {
            return assets;
        }
    }

    // Random asset
    public GameObject RandomAsset
    {
        get
        {
            return Utils.GetRandomElement(assets);
        }
    }
}