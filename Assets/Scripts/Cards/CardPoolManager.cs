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
            parkingPosition = new Vector3(-1, 0, 0);

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
                c.transform.position = parkingPosition;
            }
        }

        public void ParkCard(Card card)
        {

            card.transform.position = parkingPosition;
            
        }


    }
}