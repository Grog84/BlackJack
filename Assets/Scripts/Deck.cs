using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {

    private Stack<int> cards = new Stack<int>();

    float cardThickness;

    float startingPosition;
    float startingHeight;

    private void Start()
    {
        cardThickness = 0.002f;

        startingPosition = transform.position.y;
        startingHeight = transform.position.y;
    }

    public void ReShuffle()
    {
        cards.Clear();

        List<int> cardsList = new List<int>(Enumerable.Range(0, 51));
        cardsList.OrderBy(item => Random.value);

        cards = new Stack<int>(cardsList);
    }


    public int TakeCard()
    {
        transform.position += Vector3.down * cardThickness;
        transform.position += Vector3.down * cardThickness;

        if (cards.Count > 0)
        {
            return cards.Pop();
        }
        else
        {
            return -1;
        }
    }

    public void AddCard(int cardID)
    {
        transform.position += Vector3.up * cardThickness;
        transform.position += Vector3.up * cardThickness;

        cards.Push(cardID);
    }
}
