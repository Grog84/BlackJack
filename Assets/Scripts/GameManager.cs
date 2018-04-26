using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cards;

public class GameManager : MonoBehaviour {

    SpriteManager spriteManager;
    CardManager cardManager;
    TurnManager turnManager;

    public static GameManager instance
    {
        get; private set;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != instance)
            {
                Destroy(this.gameObject);
            }
        }

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
