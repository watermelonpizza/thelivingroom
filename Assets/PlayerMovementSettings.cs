using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerMovementSettings : ScriptableObject
{
    [Range(1, 10)]
    public int speed;
}
