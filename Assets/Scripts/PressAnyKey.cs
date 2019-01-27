using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressAnyKey : MonoBehaviour
{
    private float delay = 0;
    public float delayTime = 3;
    public string sceneName;


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

            SceneManager.LoadScene(sceneName);
        }
    }
}
