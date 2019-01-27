using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressAnyKey : MonoBehaviour {

    private float delay = 0;
    public float delayTime = 3;
    public string sceneName;
    AudioSource audioSource;
    
	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
        if (delay < delayTime)
        {
            delay += Time.deltaTime;
            Debug.Log(delay);
        }
        else if (Input.anyKeyDown)
        {
            if (audioSource)
            {
                audioSource.Play();
            }
                
            SceneManager.LoadScene(sceneName);
        }

	}
}
