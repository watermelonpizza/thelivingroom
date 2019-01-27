using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressAnyKey : MonoBehaviour
{
    private float delay = 0;
    public float delayTime = 3;
    public string sceneName;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (delay < delayTime)
        {
            delay += Time.deltaTime;
        }
        else if (Input.anyKeyDown)
        {
            var gameController = GameObject.FindGameObjectWithTag("GameController");

            if (gameController != null)
            {
                Destroy(gameController);
            }

            if (audioSource)
            {
                audioSource.Play();
            }

            SceneManager.LoadScene(sceneName);
        }
    }
}
