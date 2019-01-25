using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
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
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal") * movementSettings.speed, Input.GetAxis("Vertical") * movementSettings.speed);

        _animator.SetFloat("Horizontal", movement.x);
        _animator.SetFloat("Vertical", movement.y);
        _animator.SetFloat("Magnitude", movement.magnitude);

        //transform.position = transform.position + (movement * Time.deltaTime) * movementSettings.speed;

        _rigidbody2D.velocity = movement;
    }

    private void OnGUI()
    {
        GUI.TextArea(new Rect(0, 0, 200, 40), "Hoz: " + System.Math.Round(Input.GetAxis("Horizontal"), 3).ToString("#.###") + " ||| Vert: " + System.Math.Round(Input.GetAxis("Vertical"), 3).ToString("#.###"));
    }
}
