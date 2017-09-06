using System;
using UnityEngine;

// Effect
[Serializable]
public class Effect
{
    // Effect type
    [SerializeField]
    private EEffectType effectType;

    // Effect multiplier
    [Range(1.0f, 1000.0f)]
    [SerializeField]
    private float multiplier = 1.0f;

    // Effect type
    public EEffectType EffectType
    {
        get
        {
            return effectType;
        }
    }

    // Effect multiplier
    public float Multiplier
    {
        get
        {
            return multiplier;
        }
    }

    // To string
    public override string ToString()
    {
        return effectType.ToString() + " x" + multiplier;
    }
}
