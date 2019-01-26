using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeelsIndicator : MonoBehaviour {

    private GameStateManager _gameStateManager;
    SpriteRenderer feelsSprite;
    public Sprite maxFeels;
    public Sprite highFeels;
    public Sprite mediumFeels;
    public Sprite sadFeels;

    Animator anim;

    // Use this for initialization
    void Start()
    {
        var gameController = GameObject.FindGameObjectWithTag("GameController");
        _gameStateManager = gameController.GetComponent<GameStateManager>();
        anim = GetComponent<Animator>();

        // feelsSprite = GetComponent<SpriteRenderer>();
    }


    


	
	// Update is called once per frame
	void Update ()
    {

        anim.SetInteger ("FeelsValue", _gameStateManager.currentFeels);          

    }
}
