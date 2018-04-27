using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cards;

public class GameManager : MonoBehaviour {

    [Range(1, 3)]
    public int playersNumber;

    SpriteManager spriteManager;
    CardManager cardManager;
    CardPoolManager cardPoolManager;
    TurnManager turnManager;
    UIManager uiManager;

    [HideInInspector] public Player[] players;
    [HideInInspector] public Dealer dealer;
    [HideInInspector] public Deck deck;

    public GameObject[] playersGO;

    [HideInInspector]
    public List<int> lockedCards = new List<int>();

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
        cardPoolManager = GetComponentInChildren<CardPoolManager>();
        turnManager     = GetComponentInChildren<TurnManager>();
        uiManager       = GetComponentInChildren<UIManager>();
    }

    private void Start()
    {
        spriteManager.Init();
        cardManager.Init();
        uiManager.Init();

        PlayerVO playerVO;

        players = new Player[playersNumber];
        for (int i = 0; i < playersNumber; i++)
        {
            playersGO[i].SetActive(true);
            players[i] = playersGO[i].GetComponent<Player>();
            playerVO = new PlayerVO { id = (i+1).ToString() };
            players[i].Init(playerVO);
        }

        dealer  = FindObjectOfType<Dealer>();
        deck    = FindObjectOfType<Deck>();

        turnManager.Init();

    }

    public TurnPhase GetCurrentTurnPhase()
    {
        return turnManager.state;
    }

    public void ResetCardPosition()
    {
        cardPoolManager.ParkAllCards();
    }

    public void AddLockedCard(int cardID)
    {
        lockedCards.Add(cardID);
    }

    public void ClearLockedCard()
    {
        lockedCards.Clear();
    }

}
