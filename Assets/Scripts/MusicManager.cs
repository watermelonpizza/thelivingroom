using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public AudioSource fullHealth;
    public AudioSource highHealth;
    public AudioSource midHealth;
    public AudioSource lowHealth;
    public AudioSource criticalHealth;

    public AudioClip introTrack;
    public AudioClip fullHealthTrack;
    public AudioClip highHealthClip;
    public AudioClip midHealthClip;
    public AudioClip lowHealthClip;
    public AudioClip criticalHealthClip;

    public float feels = 100f;

    void Start ()
    {
        fullHealth.clip = introTrack;
        fullHealth.Play();

        fullHealth.volume = 1.0f;
        highHealth.volume = 0.0f;
        midHealth.volume = 0.0f;
        lowHealth.volume = 0.0f;
        criticalHealth.volume = 0.0f;

        if (feels != 100) feels = 100f;
	}
	
	void Update ()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            feels -= 20;
        }

        if (!fullHealth.isPlaying)
        {
            fullHealth.clip = fullHealthTrack;
            fullHealth.Play();
            fullHealth.loop = isActiveAndEnabled;

            highHealth.clip = highHealthClip;
            highHealth.Play();
            highHealth.loop = isActiveAndEnabled;

            midHealth.clip = midHealthClip;
            midHealth.Play();
            midHealth.loop = isActiveAndEnabled;

            lowHealth.clip = lowHealthClip;
            lowHealth.Play();
            lowHealth.loop = isActiveAndEnabled;

            criticalHealth.clip = criticalHealthClip;
            criticalHealth.Play();
            criticalHealth.loop = isActiveAndEnabled;
        }

        if (feels <100 && feels >=76)
        {
            HighHealth();
            // high health
        }

        if (feels <=75 && feels >= 51)
        {
            // mid health
            MidHealth();        }

        if (feels <=50 && feels >=26)
        {
            //low health
            LowHealth();
        }

        if (feels <25)
        {
            //critical health
            CriticalHealth();
        }
    }

    void HighHealth()
    {
        
        fullHealth.volume = 0.0f;
        highHealth.volume = 1.0f;
        midHealth.volume = 0.0f;
        lowHealth.volume = 0.0f;
         criticalHealth.volume = 0.0f;
        
    }

    void MidHealth()
    {
        fullHealth.volume = 0.0f;
        highHealth.volume = 0.0f;
        midHealth.volume = 1.0f;
        lowHealth.volume = 0.0f;
        criticalHealth.volume = 0.0f;
    }

    void LowHealth()
    {
        fullHealth.volume = 0.0f;
        highHealth.volume = 0.0f;
        midHealth.volume = 0.0f;
        lowHealth.volume = 1.0f;
        criticalHealth.volume = 0.0f;
    }

    void CriticalHealth()
    {
        fullHealth.volume = 0.0f;
        highHealth.volume = 0.0f;
        midHealth.volume = 0.0f;
        lowHealth.volume = 0.0f;
        criticalHealth.volume = 1.0f;
    }
}
