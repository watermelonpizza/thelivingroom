using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource fullHealth;
    public AudioSource highHealth;
    public AudioSource midHealth;
    public AudioSource lowHealth;
   // public AudioSource criticalHealth;

    public AudioClip introTrack;
    public AudioClip fullHealthTrack;
    public AudioClip highHealthClip;
    public AudioClip midHealthClip;
    public AudioClip lowHealthClip;


    private GameStateManager _gameStateManager;
    //   public AudioClip criticalHealthClip;

    

    //public float _gameStateManager.currentFeels = 100f;

    void Start ()
    {
        var gameController = GameObject.FindGameObjectWithTag("GameController");
        _gameStateManager = gameController.GetComponent<GameStateManager>();

        fullHealth.clip = introTrack;
        fullHealth.Play();

        fullHealth.volume = 1.0f;
        highHealth.volume = 0.0f;
        midHealth.volume = 0.0f;
        lowHealth.volume = 0.0f;
     //   criticalHealth.volume = 0.0f;

	}
	
	void Update ()
    {
        //if (Input.GetButtonDown("Fire2"))
        //{
        //    _gameStateManager.currentFeels -= 20;
        //}

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

       //     criticalHealth.clip = criticalHealthClip;
       //     criticalHealth.Play();
       //     criticalHealth.loop = isActiveAndEnabled;
        }

        if (_gameStateManager.currentFeels <100 && _gameStateManager.currentFeels >=67)
        {
            HighHealth();
            // high health
        }

        if (_gameStateManager.currentFeels <=66 && _gameStateManager.currentFeels >= 34)
        {
            // mid health
            MidHealth();
        }

        if (_gameStateManager.currentFeels <=33 && _gameStateManager.currentFeels >=0)
        {
            //low health
            LowHealth();
        }

        //  if (_gameStateManager.currentFeels <25)
        //   {
            //critical health
        //       CriticalHealth();
        //    }
    }

    void HighHealth()
    {
        
        fullHealth.volume = 0.0f;
        highHealth.volume = 1.0f;
        midHealth.volume = 0.0f;
        lowHealth.volume = 0.0f;
       // criticalHealth.volume = 0.0f;
        
    }

    void MidHealth()
    {
        fullHealth.volume = 0.0f;
        highHealth.volume = 0.0f;
        midHealth.volume = 1.0f;
        lowHealth.volume = 0.0f;
      //  criticalHealth.volume = 0.0f;
    }

    void LowHealth()
    {
        fullHealth.volume = 0.0f;
        highHealth.volume = 0.0f;
        midHealth.volume = 0.0f;
        lowHealth.volume = 1.0f;
      //  criticalHealth.volume = 0.0f;
    }

  //  void CriticalHealth()
 //   {
 //       fullHealth.volume = 0.0f;
 //       highHealth.volume = 0.0f;
 //       midHealth.volume = 0.0f;
 //       lowHealth.volume = 0.0f;
      //  criticalHealth.volume = 1.0f;
//    }
}
