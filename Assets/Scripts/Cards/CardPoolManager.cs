/*
 * The Pooling system has been implemented in case of performance issues on low budget mobiles
 * In the Card Manager it is possible to set the number of instanced cards
 * For a PC application a value of 52 (max nbr of cards) is recommanded
 * 
 * */

using System.Collections;
using UnityEngine;

namespace Cards
{
    public class CardPoolManager : MonoBehaviour
    {
        CardManager cardManager;

        public int nextCardIdx { get; private set; }
        int poolLength;

        Vector3 parkingPosition;

        private void Awake()
        {
            cardManager = GetComponent<CardManager>();
        }

        private void Start()
        {
            parkingPosition = new Vector3(-1.2f, 0, 0);

            nextCardIdx = 0;
            poolLength = cardManager.cardPoolExtension;
        }

        // Returns the next card in the pooling queue
        internal Card GetNext()
        {
            Card nextCard = cardManager.cardPool[nextCardIdx];
            nextCardIdx = (nextCardIdx + 1) % poolLength;

            return nextCard;
        }

        // Set all cards in the parking state and position
        public void ParkAllCards()
        {
            nextCardIdx = 0;
            foreach (var c in cardManager.cardPool)
            {
                ParkCard(c);
            }
        }

        // Parks only the cards that have been thrown away by the player
        public void ParkAllLooseCards()
        {
            nextCardIdx = 0;
            foreach (var c in cardManager.cardPool)
            {
                if (!c.locked)
                {
                    ParkCard(c);
                }
            }
        }

        // Parks a card
        public void ParkCard(Card card)
        {
            card.transform.position = parkingPosition;
            card.transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));
            card.locked = false;
        }

        // Puts back in the deck all loose cards
        public void CollectAllLooseCards()
        {
            foreach (var c in cardManager.cardPool)
            {
                if (!c.locked && c.transform.position != parkingPosition)
                {
                    Debug.Log(c.transform.position);
                    CollectLooseCard(c);
                }
            }
        }

        // Puts back in the deck a loose card
        public void CollectLooseCard(Card card)
        {
            card.GetComponent<CardAnimation>().AnchorToDeckPosition(GameManager.instance.deck.transform.position);
            GameManager.instance.deck.AddCard(card, false);
            StartCoroutine(DelayedParkAllCardsCO());
        }

        // Coroutine required for a proper timing
        IEnumerator DelayedParkAllCardsCO()
        {
            yield return new WaitForSeconds(2);
            ParkAllCards();
        }

    }
}