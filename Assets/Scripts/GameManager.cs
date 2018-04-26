using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    SpriteManager spriteManager;
    CardManager cardManager;
    TurnManager turnManager;


    private void Awake()
    {
        spriteManager   = GetComponentInChildren<SpriteManager>();
        cardManager     = GetComponentInChildren<CardManager>();
        turnManager     = GetComponentInChildren<TurnManager>();
    }

    private void Start()
    {
        spriteManager.Init();
        cardManager.Init();
    }

}
