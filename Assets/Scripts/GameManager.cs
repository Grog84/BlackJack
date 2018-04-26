using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cards;

public class GameManager : MonoBehaviour {

    [Range(1, 3)]
    public int playersNumber;

    SpriteManager spriteManager;
    CardManager cardManager;
    TurnManager turnManager;
    UIManager uiManager;

    [HideInInspector] public Player[] players;

    public GameObject[] playersGO;

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
        uiManager       = GetComponentInChildren<UIManager>();
    }

    private void Start()
    {
        spriteManager.Init();
        cardManager.Init();
        uiManager.Init();

        players = new Player[playersNumber];
        for (int i = 0; i < playersNumber; i++)
        {
            playersGO[i].SetActive(true);
            players[i] = playersGO[i].GetComponent<Player>();
        }

    }

    public TurnPhase GetCurrentTurnPhase()
    {
        return turnManager.state;
    }

}
