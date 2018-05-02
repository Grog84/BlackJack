/*
 * The component is controlling the player area.
 * It is keeping track of the player hand.
 * It is in charge of controlling the scripted cards animations and is also in charge
 * of updating the player current points.
 * 
 * */

using System.Collections.Generic;
using UnityEngine;

using Cards;

namespace Players
{
    public class PlayerAreaBase : MonoBehaviour
    {

        protected PlayerBase player;

        List<Transform> cardPositions = new List<Transform>();

        protected bool waitingForRelease;

        protected int availablePositionsNbr;
        protected int nextPosition;

        protected CardAnimation targetCardAnim;
        protected Card targetCard;

        protected List<CardAnimation> anchoredCards = new List<CardAnimation>();

        protected virtual void Awake()
        {
            GetCardsPositions();
        }

        protected void GetCardsPositions()
        {
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

            player.handValue += targetCard.GetPoints();
            GameManager.instance.AddLockedCard(targetCard.cardVO.idValue);

            player.waitingForCard = false;

            anchoredCards.Add(targetCardAnim);

        }

        public void ReleaseCards()
        {
            for (int i = anchoredCards.Count - 1; i > -1; i--)
            {
                anchoredCards[i].AnchorToDeckPosition(GameManager.instance.deck.transform.position);
                GameManager.instance.deck.AddCard(anchoredCards[i].GetComponent<Card>(), false);
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
}


