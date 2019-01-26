using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu]
public class GameSettings : ScriptableObject
{
    public GameState gameState;

    public Wave[] waves;
    public int currentWave;

    public enum GameState
    {
        PreStart,
        Menu,
        Running,
        GameOver,
    }

    [System.Serializable]
    public class Wave
    {
        public int numberOfEnemies = 1;
        public float secondsBetweenEachSpawn;
        public float spawnJitter;
    }
}
