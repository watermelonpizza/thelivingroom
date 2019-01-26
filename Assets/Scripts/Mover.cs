using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Mover : MonoBehaviour
{
    private Vector3 _destination = Vector3.negativeInfinity;
    private Memento _claimedMemento;
    private NavMeshAgent _navMeshAgent;
    private MementoManager _mementoManager;
    private MoverManager _moverManager;
    private SpawnPointManager _spawnPointManager;

    public void TargetMemento(Memento memento)
    {
        _claimedMemento = memento;
    }

    public void Spook()
    {
        if (_claimedMemento != null && _claimedMemento.mementoState == Memento.MementoState.PickedUp)
        {
            _claimedMemento.Drop();
            _mementoManager.ForfeitMemento(_claimedMemento);
            _claimedMemento = null;
            _moverManager.DestroyMover(this);
        }
    }

    private void Start()
    {
        var gameController = GameObject.FindGameObjectWithTag("GameController");
        _mementoManager = gameController.GetComponent<MementoManager>();
        _spawnPointManager = gameController.GetComponent<SpawnPointManager>();
        _moverManager = gameController.GetComponent<MoverManager>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_claimedMemento.mementoState != Memento.MementoState.PickedUp)
        {
            _destination = _claimedMemento.transform.position;
        }

        if (_destination != Vector3.negativeInfinity)
        {
            _navMeshAgent.SetDestination(_destination);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_claimedMemento.mementoState != Memento.MementoState.PickedUp
            && collision.gameObject == _claimedMemento.gameObject)
        {
        }


        if (collision.tag == "Exit" && _claimedMemento.mementoState == Memento.MementoState.PickedUp)
        {
            _mementoManager.DestroyMemento(_claimedMemento);
            _moverManager.DestroyMover(this);
        }
    }

    private IEnumerator PickUpObject()
    {
        //yield return WaitForSeconds(settings);
        _claimedMemento.PickUp(this);
        _destination = _spawnPointManager.GetRandomSpawnPoint();
        return null;
    }
}
