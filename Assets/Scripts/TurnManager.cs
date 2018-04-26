using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnState { IDLE, PLAYER, DEALER };

public class TurnManager : MonoBehaviour {

    TurnState state;

    public void Init()
    {
        state = TurnState.IDLE;
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
        yield return null;
    }

    IEnumerator DealerTurnCO()
    {
        yield return null;
    }

    IEnumerator PassTurnCO()
    {
        yield return null;
    }

}
