using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    public Spawn[] spawnPoints;

    public Vector3 GetRandomSpawnPoint()
    {
        var enabledSpawns = spawnPoints.Where(x => x.enabled).ToArray();
        return enabledSpawns[Random.Range(0, enabledSpawns.Length)].spawnPoint.transform.position;
    }

    [System.Serializable]
    public class Spawn
    {
        public GameObject spawnPoint;
        public bool enabled;
        public int enableOnWave;
    }
}
