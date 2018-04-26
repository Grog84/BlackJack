using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cards;

public class PlayerArea : MonoBehaviour {

    Player player;

    List<Transform> cardPositions = new List<Transform>();

    bool animationRunning;

    int availablePositionsNbr;
    int nextPosition;

    private void Awake()
    {
        animationRunning = false;

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
            if (player.waitingForCard && !animationRunning)
            {
                animationRunning = true;

                other.GetComponent<CardAnimation>().AnchorToPosition(cardPositions[nextPosition].position);
                if (nextPosition < availablePositionsNbr)
                {
                    nextPosition++;
                }
                else { Debug.Log("Player Area - 'No available position left!'"); }

                player.playerHandValue += other.GetComponent<Card>().GetPoints();

                player.waitingForCard = false;

            }
        }
    }

}
