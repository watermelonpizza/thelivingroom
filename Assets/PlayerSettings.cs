using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerSettings : ScriptableObject
{
    [Range(1, 10)]
    public int speed;

    [Range(.1f, 10)]
    public float range;

    [Range(.1f, 10)]
    public float cooldown;
}
