using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpawnPointManager))]
public class MoverManager : MonoBehaviour
{
    public GameObject moverPrefab;

    private SpawnPointManager _spawnPointManager;
    private List<Mover> _movers = new List<Mover>();

    public Mover SpawnMover()
    {
        var moverObject = Instantiate(moverPrefab, _spawnPointManager.GetRandomSpawnPoint(), Quaternion.identity);
        var mover = moverObject.GetComponent<Mover>();
        _movers.Add(mover);

        return mover;
    }

    public void DestroyMover(Mover mover)
    {
        _movers.Remove(mover);
        Destroy(mover.gameObject);
    }

    public bool AllMoversDestroyed()
    {
        return _movers.Count == 0;
    }

    private void Start()
    {
        _spawnPointManager = GetComponent<SpawnPointManager>();
    }
}
