﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cards;

// TODO This should have been derived from a generic Area object together with dealer area
public class PlayerArea : MonoBehaviour {

    Player player;

    List<Transform> cardPositions = new List<Transform>();

    bool waitingForRelease;

    int availablePositionsNbr;
    int nextPosition;

    CardAnimation targetCardAnim;
    Card targetCard;

    List<CardAnimation> anchoredCards = new List<CardAnimation>();

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        foreach (Transform tr in transform)
        {
            cardPositions.Add(tr);
        }
        availablePositionsNbr = cardPositions.Count;
        nextPosition = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Card")
        {
            if (player.waitingForCard)
            {
                waitingForRelease = true;
                targetCardAnim = other.GetComponent<CardAnimation>();
                targetCard = other.GetComponent<Card>();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Card")
        {
            waitingForRelease = false;
            targetCardAnim = null;
            targetCard = null;
        }
    }

    private void AnchorCard()
    {
        waitingForRelease = false;
        targetCard.GetComponent<Rigidbody>().isKinematic = true;

        targetCardAnim.AnchorToPosition(cardPositions[nextPosition].position);
        if (nextPosition < availablePositionsNbr)
        {
            nextPosition++;
        }
        else { Debug.Log("Player Area - 'No available position left!'"); }

        player.playerHandValue += targetCard.GetPoints();
        GameManager.instance.AddLockedCard(targetCard.cardVO.idValue);

        player.waitingForCard = false;

        anchoredCards.Add(targetCardAnim);

    }

    public void ReleaseCards()
    {
        for (int i = anchoredCards.Count - 1; i > -1; i--)
        {
            anchoredCards[i].AnchorToDeckPosition(GameManager.instance.deck.transform.position);
            anchoredCards.RemoveAt(i);
        }

        nextPosition = 0;
    }


    private void Update()
    {
        if (waitingForRelease)
        {
            if (!targetCard.grabbed && !targetCard.animating)
            {
                AnchorCard();
            }
        }
    }

}
