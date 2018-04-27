using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnPhase { IDLE, PLAYER, DEALER };

public class TurnManager : MonoBehaviour {

    public TurnPhase state { get; private set; }

    [HideInInspector] public bool dealerTurn;

    UIManager uiManager;

    private void Start()
    {
        dealerTurn = false;
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
        dealerTurn = true;

        uiManager.DealerTurnBegins();

        GameManager.instance.dealer.waitingForCard = true;

        while (dealerTurn)
        {
            if (GameManager.instance.dealer.dealerHandValue > 21)
            {
                dealerTurn = false;
                GameManager.instance.dealer.SetBusted();
            }
            yield return null;    
        }

        GameManager.instance.dealer.waitingForCard = false;
    }

    IEnumerator PassTurnCO()
    {
        string winner = "";

        foreach (var pl in GameManager.instance.players)
        {
            if (GameManager.instance.dealer.dealerHandValue > 21)
            {
                if (pl.state == PlayerState.BUSTED)
                {
                    winner = "Draw";
                }
                else
                {
                    winner = pl.playerVO.id;
                }
            }
            else
            {
                winner = "Dealer";
                if (pl.state != PlayerState.BUSTED)
                {
                    if (pl.playerHandValue > GameManager.instance.dealer.dealerHandValue)
                    {
                        winner = pl.playerVO.id;
                    }
                }
            }

            Debug.Log("Turn reset player");
            ResetPlayer(pl);
            yield return StartCoroutine(uiManager.Wins(winner));
        }

        GameManager.instance.dealer.ResetDealer();
        yield return new WaitForSeconds(1);

        GameManager.instance.ColectLooseCards();

        yield return new WaitForSeconds(3);

        GameManager.instance.ResetCardPosition();
        GameManager.instance.ClearLockedCard();

        yield return null;
    }

    void ResetPlayer(Player player)
    {
        player.state = PlayerState.IDLE;
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

    public void StopDealerTurn()
    {
        if (dealerTurn)
        {
            dealerTurn = false;
            GameManager.instance.dealer.SetStop();
        }
    }

}
