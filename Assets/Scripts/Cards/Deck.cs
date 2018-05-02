/*
 * The deck consists in a list of cards identified by an integer value, that in turns referes to the cards value objects
 * When the deck is clicked by the user a card is pooled to the deck position using the Card pool manager.
 * The value and sprite of the card is then updated according to the corresponding top of the deck value
 * 
 * The card stack is represented by a linked list, since this data structure allows to easily add
 * cards at the bottom and at the top of the stack
 * 
 * */

using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class Deck : MonoBehaviour {

        private CardPoolManager cardPoolManager;
        private CardManager cardManager;

        private UIManager uiManager;

        private LinkedList<int> cards = new LinkedList<int>();

        float cardThickness;
        float loweringAmount;

        Vector3 startingPosition;
        Vector3 startingScale;

        int cardsLeft;

        private bool _clicked;

        public bool clicked
        {
            get
            {
                return _clicked;
            }
            set
            {
                _clicked = value;
                if (_clicked)
                {
                    TakeCard();
                }           
            }
        }

        private void Start()
        {
            startingPosition = transform.position;
            startingScale = transform.localScale;

            cardThickness = 0.002f;
            loweringAmount = Mathf.Abs(startingPosition.y / 52);

            cardPoolManager = FindObjectOfType<CardPoolManager>();
            cardManager = FindObjectOfType<CardManager>();
            uiManager = FindObjectOfType<UIManager>();

            cardsLeft = 52;
            uiManager.UpdateCardCounter(cardsLeft);

            ReShuffle();
        }

        // Shuffles a new deck from scratch. At the end the deck will be composed by all the 52 cards
        public void ReShuffle()
        {
            cards.Clear();

            cardPoolManager.CollectAllLooseCards();

            List<int> cardsList = new List<int>(Enumerable.Range(0, 52));
            if (GameManager.instance.lockedCards.Count > 0)
            {
                foreach (int id in GameManager.instance.lockedCards)
                {
                    cardsList.Remove(id);
                }

            }

            cardsList = cardsList.OrderBy(item => Random.value).ToList();

            cards = new LinkedList<int>(cardsList);

            transform.position = startingPosition;
            transform.localScale = startingScale;

            cardsLeft = cards.Count;
            uiManager.UpdateCardCounter(cardsLeft);
        }

        // Shuffles an already existing deck. At the end of te shuffling process
        // the deck will be composed by the same ammount of cards composing it at the beginning
        public void Shuffle()
        {          
            List<int> cardsList = new List<int>(cards);
            cardsList = cardsList.OrderBy(item => Random.value).ToList();

            cards = new LinkedList<int>(cardsList);
          
        }

        // Pools a card and assignes the correct value
        public void TakeCard()
        {
            if (cardsLeft > 0)
            {
                transform.position += Vector3.down * loweringAmount;
                transform.localScale = transform.localScale + Vector3.back * cardThickness;

                int cardIdx = -1;

                if (cards.Count > 0)
                {
                    cardIdx = cards.First();
                    cards.RemoveFirst();
                    Card pickedUpCard = cardPoolManager.GetNext();
                    pickedUpCard.cardVO = cardManager.cardsVOList[cardIdx];
                    pickedUpCard.transform.position = transform.position;
                    pickedUpCard.grabbed = true;
                }

                cardsLeft--;
                uiManager.UpdateCardCounter(cardsLeft);

                if (cardsLeft == 0)
                {
                    transform.position += Vector3.up * 5;
                }

            }
        }

        // When a card is falling at the top of the deck, it is added back to the stack
        public void AddCard(Card card, bool top = true)
        {
            transform.position += Vector3.up * cardThickness;
            transform.localScale = transform.localScale + Vector3.forward * cardThickness;

            if (top)
            {
                cards.AddFirst(card.cardVO.idValue);
                cardPoolManager.ParkCard(card);
            }
            else
            {
                cards.AddLast(card.cardVO.idValue);
            }


            cardsLeft++;
            uiManager.UpdateCardCounter(cardsLeft);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Card")
            {
                
                 AddCard(collision.gameObject.GetComponent<Card>());
                
            }
        }

    }
}

