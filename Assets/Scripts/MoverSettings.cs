using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu]
public class MoverSettings : ScriptableObject
{
    [Range(.1f, 10)]
    public float walkingSpeed;
    [Range(.1f, 10)]
    public float carrySpeed;
    [Range(.1f, 10)]
    public float scaredSpeed;
    [Range(.1f, 10)]
    public float timeToPickupItem;
}
