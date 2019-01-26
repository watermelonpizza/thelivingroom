using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

//[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public PlayerSettings settings;

    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _lastMovement = Vector2.down;
    private float _lastFire = 0;

    [HideInInspector]
    public bool movementSettingsFoldout = true;

    private void Start()
    {
        //_animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal") * settings.speed, Input.GetAxis("Vertical") * settings.speed);

        //_animator.SetFloat("Horizontal", movement.x);
        //_animator.SetFloat("Vertical", movement.y);
        //_animator.SetFloat("Magnitude", movement.magnitude);

        _rigidbody2D.velocity = movement;

        if (Input.GetAxis("Jump") > 0 && CanFire())
        {
            _lastFire = Time.timeSinceLevelLoad;
            Debug.DrawRay(transform.position, _lastMovement.normalized * settings.range, Color.red, .5f);
            RaycastHit2D[] results = Physics2D.RaycastAll(transform.position, _lastMovement.normalized, settings.range);
            if (results.Length > 0)
            {
                foreach (var result in results.Where(x => x.collider.tag == "Mover"))
                {
                    result.collider.GetComponentInParent<Mover>().Spook();
                }
            }
        }

        if (movement != Vector2.zero)
        {
            _lastMovement = movement;
        }
    }

    private bool CanFire()
    {
        return Time.timeSinceLevelLoad - _lastFire > settings.cooldown;
    }

    private void OnGUI()
    {
        GUI.TextArea(new Rect(0, 0, 200, 40), "Hoz: " + System.Math.Round(Input.GetAxis("Horizontal"), 3).ToString("#.###") + " ||| Vert: " + System.Math.Round(Input.GetAxis("Vertical"), 3).ToString("#.###") + " ||| Cooldown: " + CanFire());
    }
}
