using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    private AudioSource audioSource;
    public AudioClip introTrack;
    public AudioClip loopTrack;
    float trackDuration;

   // private bool isPlaying;

    // Use this for initialization
    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = introTrack;
      //  trackDuration = audioSource.clip.length;
        audioSource.Play();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (!audioSource.isPlaying)
        {
            audioSource.clip = loopTrack;
            audioSource.Play();
            audioSource.loop = isActiveAndEnabled;
        }
	}
}
