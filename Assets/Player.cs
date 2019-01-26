using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public PlayerMovementSettings movementSettings;

    private Animator _animator;
    private Rigidbody2D _rigidbody2D;

    [HideInInspector]
    public bool movementSettingsFoldout = true;

    private void Start()
    {
        //_animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal") * movementSettings.speed, Input.GetAxis("Vertical") * movementSettings.speed);

        //_animator.SetFloat("Horizontal", movement.x);
        //_animator.SetFloat("Vertical", movement.y);
        //_animator.SetFloat("Magnitude", movement.magnitude);

        _rigidbody2D.velocity = movement;

        if (Input.GetAxis("Jump") == 1)
        {

        }
    }
}
