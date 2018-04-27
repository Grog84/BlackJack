using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dealer : MonoBehaviour {

    [HideInInspector]
    public int dealerHandValue;

    [HideInInspector]
    private bool _waitingForCard;

    public bool waitingForCard
    {
        get { return _waitingForCard; }
        set
        {
            _waitingForCard = value;
            if (_waitingForCard)
            {
                stateRenderer.color = oneMoreColor;
            }
            else
            {
                stateRenderer.color = idleColor;
            }
        }
    }

    public SpriteRenderer stateRenderer;

    public Color oneMoreColor;
    public Color stopColor;
    public Color bustedColor;
    public Color idleColor;

    private void Start()
    {
        dealerHandValue = 0;
        waitingForCard = false;
    }

    public void SetBusted()
    {
        stateRenderer.color = bustedColor;
    }

    public void SetStop()
    {
        stateRenderer.color = stopColor;
    }

    public void ResetDealer()
    {
        GetComponentInChildren<DealerArea>().ReleaseCards();
        dealerHandValue = 0;
    }


}
