using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour {


    private List<CardVO> _thumbnailVOList = new List<CardVO>();

    // Use this for initialization
    void Start () {

        createCardsVOList();
    }

    private void createCardsVOList()
    {
        CardVO cardVO;
        for (int i = 0; i < 52; i++)
        {
            cardVO = new CardVO();
            
        }
    }
}
