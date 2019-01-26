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

    // Use this for initialization
    void Start ()
    {
        var gameController = GameObject.FindGameObjectWithTag("GameController"); 
        _gameStateManager = gameController.GetComponent<GameStateManager>();

        feelsSprite = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (_gameStateManager.currentFeels ==100)
        {
            feelsSprite.sprite = maxFeels;
        }

        if (_gameStateManager.currentFeels <100 && _gameStateManager.currentFeels >= 75)
        {
            feelsSprite.sprite = highFeels;
        }

        if (_gameStateManager.currentFeels < 75 && _gameStateManager.currentFeels >= 50)
        {
            feelsSprite.sprite = mediumFeels;
        }

        if (_gameStateManager.currentFeels < 100 && _gameStateManager.currentFeels >= 75)
        {
            feelsSprite.sprite = sadFeels;
        }

    }
}
