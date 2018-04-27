using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class Deck : MonoBehaviour {

        private CardPoolManager cardPoolManager;
        private CardManager cardManager;

        private UIManager uiManager;

        private Stack<int> cards = new Stack<int>();

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

            cards = new Stack<int>(cardsList);

            transform.position = startingPosition;
            transform.localScale = startingScale;

            cardsLeft = cards.Count;
            uiManager.UpdateCardCounter(cardsLeft);
        }


        public void TakeCard()
        {
            if (cardsLeft > 0)
            {
                transform.position += Vector3.down * loweringAmount;
                transform.localScale = transform.localScale + Vector3.back * cardThickness;

                int cardIdx = -1;

                if (cards.Count > 0)
                {
                    cardIdx = cards.Pop();
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

        public void AddCard(Card card)
        {
            transform.position += Vector3.up * cardThickness;
            transform.position += Vector3.up * cardThickness;

            cards.Push(card.cardVO.idValue);
            cardPoolManager.ParkCard(card);

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

