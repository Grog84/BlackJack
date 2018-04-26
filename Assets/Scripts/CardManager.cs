using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour {

    private List<CardVO> _cardsVOList = new List<CardVO>();

    public void Init () {

        createCardsVOList();
    }

    private void createCardsVOList()
    {
        CardVO cardVO;
        List<string> dictKeys = new List<string>(SpriteManager.instance.spriteDict.Keys);

        for (int i = 0; i < SpriteManager.instance.spriteDict.Count; i++)
        {
            cardVO = new CardVO();
            cardVO.id = dictKeys[i];
            _cardsVOList.Add(cardVO);
        }
    }
}
