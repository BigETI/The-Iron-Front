using UnityEngine;

// Dialogue object
[CreateAssetMenu(fileName = "Dialogue", menuName = "TheIronFront/Dialogue")]
public class DialogueObjectScript : ScriptableObject
{

    // Sender name
    [SerializeField]
    private string senderName;

    // Message
    [TextArea]
    [SerializeField]
    private string message;

    // Life time
    [Range(0.0f, 1000.0f)]
    [SerializeField]
    private float lifeTime = 5.0f;

    // Char time
    [Range(0.0f, 1000.0f)]
    [SerializeField]
    private float charTime = 0.0625f;

    // Blah blah clip
    [SerializeField]
    private AudioClip blahBlahClip;

    // Asset
    [SerializeField]
    private GameObject asset;

    // Sender name
    public string SenderName
    {
        get
        {
            return senderName;
        }
    }

    // Message
    public string Message
    {
        get
        {
            return message;
        }
    }

    // Life time
    public float LifeTime
    {
        get
        {
            return lifeTime;
        }
    }

    // Char time
    public float CharTime
    {
        get
        {
            return charTime;
        }
    }

    // Blah blah clip
    public AudioClip BlahBlahClip
    {
        get
        {
            return blahBlahClip;
        }
    }

    // Asset
    public GameObject Asset
    {
        get
        {
            return asset;
        }
    }
}
