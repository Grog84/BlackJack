/*
 * The Turn manager is responsible of the different phases of the game.
 * The phases are timed and set in a queue using a Coroutine system, having a main coroutine
 * waiting for all the others to be over.
 * 
 * */

using System.Collections;
using UnityEngine;

using Players;

public enum TurnPhase { IDLE, PLAYER, DEALER };

public class TurnManager : MonoBehaviour {

    public TurnPhase state { get; private set; }

    bool dealerTurn;
    Dealer dealer;

    UIManager uiManager;

    private void Start()
    {
        dealerTurn = false;
        uiManager = FindObjectOfType<UIManager>();

        dealer = FindObjectOfType<Dealer>();
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

        dealer.waitingForCard = true;

        while (dealerTurn)
        {
            if (dealer.handValue > 21)
            {
                dealerTurn = false;
                dealer.SetBusted();
            }
            else
            {
                if (dealer.waitingForCard == false)
                {
                    dealer.waitingForCard = true;
                }
            }

            yield return null;    
        }

        dealer.waitingForCard = false;
    }

    IEnumerator PassTurnCO()
    {
        string winner = "";

        foreach (var pl in GameManager.instance.players)
        {
            if (dealer.handValue > 21)
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
                    if (pl.handValue > dealer.handValue)
                    {
                        winner = pl.playerVO.id;
                    }
                    else if (pl.handValue == dealer.handValue)
                    {
                        winner = "Draw";
                    }
                }
            }

            ResetPlayer(pl);
            yield return StartCoroutine(uiManager.Wins(winner));
        }

        dealer.ResetDealer();
        yield return new WaitForSeconds(1);

        GameManager.instance.ColectLooseCards();

        yield return new WaitForSeconds(2);

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
            dealer.SetStop();
        }
    }

}
