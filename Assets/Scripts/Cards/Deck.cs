using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class Deck : MonoBehaviour {

        private CardPoolManager cardPoolManager;
        private CardManager cardManager;

        private Stack<int> cards = new Stack<int>();

        float cardThickness;

        float startingPosition;
        float startingHeight;

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
            cardThickness = 0.002f;

            startingPosition = transform.position.y;
            startingHeight = transform.position.y;

            cardPoolManager = FindObjectOfType<CardPoolManager>();
        }

        public void ReShuffle()
        {
            cards.Clear();

            List<int> cardsList = new List<int>(Enumerable.Range(0, 51));
            cardsList.OrderBy(item => Random.value);

            cards = new Stack<int>(cardsList);
        }


        public void TakeCard()
        {
            transform.position += Vector3.down * cardThickness;
            transform.position += Vector3.down * cardThickness;

            int cardIdx = -1;

            if (cards.Count > 0)
            {
                cardIdx = cards.Pop();
                Card pickedUpCard = cardPoolManager.GetNext();
                pickedUpCard.cardVO = cardManager.cardsVOList[cardIdx];
                pickedUpCard.transform.position = transform.position;
                pickedUpCard.grabbed = true;
            }

        }

        public void AddCard(Card card)
        {
            transform.position += Vector3.up * cardThickness;
            transform.position += Vector3.up * cardThickness;

            cards.Push(card.cardVO.idValue);
            cardPoolManager.ParkCard(card);
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

