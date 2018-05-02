/*
 * Component in charge of controlling the UI messages
 * 
 * */

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject[] labels;
    public GameObject[] messages;
    public Text cardCounter;

    public void Init()
    {
        for (int i = 1; i < GameManager.instance.playersNumber + 1; i++)
        {
            labels[i].SetActive(true);
        }
    }

    public void PlayerTurnBegins()
    {
        StartCoroutine(MessageCO(0));
    }

    public void DealerTurnBegins()
    {
        StartCoroutine(MessageCO(1));
    }

    public IEnumerator Wins(string winner)
    {
        if (winner == "Draw")
        {
            messages[2].GetComponent<Text>().text = "Draw!";
        }
        else
        {
            messages[2].GetComponent<Text>().text = winner + " Wins!";
        }
        yield return StartCoroutine(MessageCO(2));
    }

    IEnumerator MessageCO(int messageIdx)
    {
        float timer = 0;
        Vector3 startingPosition = Vector3.zero;
        Vector3 midPosition = Vector3.right * 2000;
        Vector3 endPosition = Vector3.right * 4000;

        while (timer < 3)
        {
            if (timer < 1)
            {
                messages[messageIdx].transform.localPosition = Vector3.Lerp(startingPosition, midPosition, timer);
            }
            else if (timer > 2)
            {
                messages[messageIdx].transform.localPosition = Vector3.Lerp(midPosition, endPosition, (timer - 2));
            }

            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        messages[messageIdx].transform.localPosition = startingPosition;

    }

    public void UpdateCardCounter(int nbr)
    {
        cardCounter.text = "Remaining \n Cards: \n " + nbr;
    }

}
