using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneGhostResult : MonoBehaviour
{
    public Sprite winSprite;
    public Sprite loseSprite;

    private GameStateManager _gameStateManager;

    void Start()
    {
        var gameController = GameObject.FindGameObjectWithTag("GameController");
        _gameStateManager = gameController.GetComponent<GameStateManager>();

        var spriteRenderer = GetComponent<SpriteRenderer>();

        if (_gameStateManager.currentFeels > 0)
        {
            spriteRenderer.sprite = winSprite;
        }
        else
        {
            spriteRenderer.sprite = loseSprite;
        }
    }

    void Update()
    {
       
    }
}
