﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Mover : MonoBehaviour
{
    private bool _destinationRequiresSet = false;
    private bool _mementoPickedUp = false;
    private Vector3 _destination;
    private Memento _claimedMemento;
    private NavMeshAgent _navMeshAgent;
    private MementoManager _mementoManager;
    private SpawnPointManager _spawnPointManager;

    public void TargetMemento(Memento memento)
    {
        _claimedMemento = memento;
        _destination = memento.transform.position;
        _destinationRequiresSet = true;
    }

    public void Spook()
    {
        _mementoManager.ForfeitMemento(_claimedMemento);
    }

    private void Start()
    {
        var gameController = GameObject.FindGameObjectWithTag("GameController");
        _mementoManager = gameController.GetComponent<MementoManager>();
        _spawnPointManager = gameController.GetComponent<SpawnPointManager>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_destinationRequiresSet)
        {
            _navMeshAgent.SetDestination(_destination);
        }

        if (!_mementoPickedUp && _claimedMemento != null && Vector2.Distance(transform.position, _claimedMemento.transform.position) <= _navMeshAgent.stoppingDistance)
        {
            _claimedMemento.PickUp(this);
            _destination = _spawnPointManager.GetRandomSpawnPoint();
            _destinationRequiresSet = true;
            _mementoPickedUp = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger");
        Debug.Log(collision);
        if (collision.tag == "Player")
        {
            _claimedMemento.Drop();
            _mementoManager.ForfeitMemento(_claimedMemento);
            _claimedMemento = null;
            _mementoPickedUp = false;
            Destroy(gameObject);
        }

        if (collision.tag == "Exit" && _mementoPickedUp)
        {
            Debug.Log("removed");
            _mementoManager.DestroyMemento(_claimedMemento);
            Destroy(gameObject);
        }
    }
}