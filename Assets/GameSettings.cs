using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu]
public class GameSettings : ScriptableObject
{
    public Wave[] waves;
    public float timeBetweenWaves;
    public int startingFeels;

    [System.Serializable]
    public class Wave
    {
        public int numberOfEnemies = 1;
        public float secondsBetweenEachSpawn;
        public float spawnJitter;

        public Wave Clone()
        {
            return (Wave)MemberwiseClone();
        }
    }
}
