using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Mover : MonoBehaviour
{
    public MoverSettings settings;

    private bool _spooked;
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
            _spooked = true;
            _claimedMemento.Drop();
            _mementoManager.ForfeitMemento(_claimedMemento);
            _claimedMemento = null;

            _destination = _spawnPointManager.GetRandomSpawnPoint();
            _navMeshAgent.speed = settings.scaredSpeed;
        }
    }

    private void Start()
    {
        var gameController = GameObject.FindGameObjectWithTag("GameController");
        _mementoManager = gameController.GetComponent<MementoManager>();
        _spawnPointManager = gameController.GetComponent<SpawnPointManager>();
        _moverManager = gameController.GetComponent<MoverManager>();
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _navMeshAgent.speed = settings.walkingSpeed;
    }

    private void Update()
    {
        if (_claimedMemento != null && _claimedMemento.mementoState != Memento.MementoState.PickedUp)
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
        if (_claimedMemento != null)
        {
            if (_claimedMemento.mementoState != Memento.MementoState.PickedUp
            && collision.gameObject == _claimedMemento.gameObject)
            {
                StartCoroutine(PickUpObject());
            }

            if (collision.tag == "Exit" && _claimedMemento.mementoState == Memento.MementoState.PickedUp)
            {
                _mementoManager.DestroyMemento(_claimedMemento);
                _moverManager.DestroyMover(this);
            }
        }

        if (collision.tag == "Exit" && _spooked)
        {
            _moverManager.DestroyMover(this);
        }
    }

    private IEnumerator PickUpObject()
    {
        yield return new WaitForSeconds(settings.timeToPickupItem);

        _navMeshAgent.speed = settings.carrySpeed;
        _claimedMemento.PickUp(this);
        _destination = _spawnPointManager.GetRandomSpawnPoint();
    }
}
