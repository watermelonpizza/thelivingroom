using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;
    public PlayerMovementSettings movementSettings;

    [HideInInspector]
    public bool movementSettingsFoldout = true;

    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);

        transform.position = transform.position + (movement * Time.deltaTime) * movementSettings.speed;
    }

    private void OnGUI()
    {
        GUI.TextArea(new Rect(0, 0, 200, 40), "Hoz: " + System.Math.Round(Input.GetAxis("Horizontal"), 3).ToString("#.###") + " ||| Vert: " + System.Math.Round(Input.GetAxis("Vertical"), 3).ToString("#.###"));
    }
}
