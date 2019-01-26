using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public AudioSource firstTrack;
    public AudioSource layerOne;
    public AudioClip introTrack;
    public AudioClip loopTrack;
    public AudioClip layer1clip;
    float trackDuration;

    public bool layerOneActive = false;

    // Use this for initialization
    void Start ()
    {
        firstTrack.clip = introTrack;
        firstTrack.Play();
        layerOne.volume = 0.0f;
        firstTrack.volume = 1.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (!firstTrack.isPlaying)
        {
            firstTrack.clip = loopTrack;
            firstTrack.Play();
            firstTrack.loop = isActiveAndEnabled;
            layerOne.clip = layer1clip;
            layerOne.Play();
            layerOne.loop = isActiveAndEnabled;
        }

        if (Input.GetButton("Fire2"))
        {
            SwapLayers();
        }
    }

    void SwapLayers()
    {
        if (layerOneActive == false)
        {
            firstTrack.volume = 0.0f;
            layerOne.volume = 1.0f;
            layerOneActive = true;
        }

        else if (layerOneActive == true)
        {
            firstTrack.volume = 1.0f;
            layerOne.volume = 0.0f;
            layerOneActive = false;
        }
    }
}
