using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(ParticleSystem))]
public class Memento : MonoBehaviour
{
    [Range(0, 4)]
    public float timeBeforeTeleport = 1;

    private Vector3 _originalPosition;
    private BoxCollider2D _boxCollider2D;
    private ParticleSystem _particleSystem;

    private bool _triggered = false;

    private void Start()
    {
        _originalPosition = transform.position;
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_triggered && transform.position != _originalPosition)
        {
            _triggered = true;

            StartCoroutine(PlayParticals());
        }
    }

    public void ResetPosition()
    {
        transform.position = _originalPosition;
    }

    private IEnumerator PlayParticals()
    {
        _particleSystem.Play();
        yield return new WaitForSeconds(timeBeforeTeleport);
        transform.position = _originalPosition;
        _particleSystem.Play();

        _triggered = false;
    }
}
