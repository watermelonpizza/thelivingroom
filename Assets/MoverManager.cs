using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpawnPointManager))]
public class MoverManager : MonoBehaviour
{
    public GameObject moverPrefab;

    private SpawnPointManager _spawnPointManager;
    private List<GameObject> _movers = new List<GameObject>();

    public Mover SpawnMover()
    {
        var mover = Instantiate(moverPrefab, _spawnPointManager.GetRandomSpawnPoint(), Quaternion.identity);
        _movers.Add(mover);

        return mover.GetComponent<Mover>();
    }

    private void Start()
    {
        _spawnPointManager = GetComponent<SpawnPointManager>();
    }
}
