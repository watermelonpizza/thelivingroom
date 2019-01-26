using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameStateManager))]
public class MementoManager : MonoBehaviour
{
    private GameObject[] _mementos;
    private readonly object _claimLock = new object();
    private List<GameObject> _claimableMementos = new List<GameObject>();

    private GameStateManager _gameStateManager;

    public bool TryClaimMemento(out Memento memento)
    {
        memento = null;

        lock (_claimLock)
        {
            if (_claimableMementos.Count > 0)
            {
                var randomIndex = Random.Range(0, _claimableMementos.Count - 1);
                memento = _claimableMementos[randomIndex].GetComponent<Memento>();
                _claimableMementos.RemoveAt(randomIndex);

                return true;
            }
        }

        return false;
    }

    public void ForfeitMemento(Memento memento)
    {
        lock (_claimLock)
        {
            _claimableMementos.Add(memento.gameObject);
        }
    }

    public void DestroyMemento(Memento claimedMemento)
    {
        _gameStateManager.currentFeels -= claimedMemento.theFeels;
        _claimableMementos.Remove(claimedMemento.gameObject);
        Destroy(claimedMemento.gameObject);
    }

    private void Start()
    {
        _mementos = GameObject.FindGameObjectsWithTag("Memento");
        _claimableMementos.AddRange(_mementos);
        _gameStateManager = GetComponent<GameStateManager>();
    }
}
