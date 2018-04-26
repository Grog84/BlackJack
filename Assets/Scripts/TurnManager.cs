using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnPhase { IDLE, PLAYER, DEALER };

public class TurnManager : MonoBehaviour {

    public TurnPhase state { get; private set; }

    [HideInInspector] public bool dealerTurn = true;

    UIManager uiManager;

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    public void Init()
    {
        state = TurnPhase.IDLE;
        StartCoroutine(TurnCO());    
    }

    IEnumerator TurnCO()
    {
        while (true)
        {
            yield return StartCoroutine(PlayerTurnCO());
            yield return StartCoroutine(DealerTurnCO());
            yield return StartCoroutine(PassTurnCO());
        }
    }

    IEnumerator PlayerTurnCO()
    {
        uiManager.PlayerTurnBegins();

        foreach (var pl in GameManager.instance.players)
        {
            pl.state = PlayerState.DECIDING;
        }

        while (!CheckPlayerTurnOver())
        {
            yield return new WaitForSeconds(1);
        }

    }

    IEnumerator DealerTurnCO()
    {
        uiManager.DealerTurnBegins();

        while (dealerTurn)
        {
            yield return null;    
        }
    }

    IEnumerator PassTurnCO()
    {
        yield return null;
    }

    bool CheckPlayerTurnOver()
    {
        bool isOver = true;
        for (int i = 0; i < GameManager.instance.playersNumber; i++)
        {
            isOver = isOver && (GameManager.instance.players[i].state == PlayerState.BUSTED ||
                GameManager.instance.players[i].state == PlayerState.STOP);
        }
        return isOver;
    }

}
