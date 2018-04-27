using System.Collections;
using System.Collections.Generic;
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

        internal Card GetNext()
        {
            Card nextCard = cardManager.cardPool[nextCardIdx];
            nextCardIdx = (nextCardIdx + 1) % poolLength;

            return nextCard;
        }

        public void ParkAllCards()
        {
            nextCardIdx = 0;
            foreach (var c in cardManager.cardPool)
            {
                ParkCard(c);
            }
        }

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

        public void ParkCard(Card card)
        {
            Debug.Log("Park");
            card.transform.position = parkingPosition;
            card.transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));
            card.locked = false;
        }

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

        public void CollectLooseCard(Card card)
        {
            card.GetComponent<CardAnimation>().AnchorToDeckPosition(GameManager.instance.deck.transform.position);
            GameManager.instance.deck.AddCard(card, false);
            StartCoroutine(DelayedParkAllCardsCO());
        }

        IEnumerator DelayedParkAllCardsCO()
        {
            yield return new WaitForSeconds(2);
            ParkAllCards();
        }

    }
}