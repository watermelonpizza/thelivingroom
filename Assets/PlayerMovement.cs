using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;

    [Range(1, 10)]
    public int playerSpeed = 1;
    
    void Update()
    {
        //animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
        transform.position = transform.position + (movement * Time.deltaTime) * playerSpeed;
    }

    private void OnGUI()
    {
        GUI.TextArea(Rect.zero, "aWDWA");
    }
}
