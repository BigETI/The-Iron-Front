// Day time
using System;

[Flags]
public enum EDayTime
{
    // Morning
    Morning = 0x1,

    // Noon
    Noon = 0x2,

    // Evening
    Evening = 0x4,

    // Night
    Night = 0x8
}
