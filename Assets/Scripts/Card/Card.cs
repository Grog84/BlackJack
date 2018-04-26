using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {

    private CardVO _cardVO;
    public SpriteRenderer frontRenderer;

    public CardVO cardVO
    {
        get
        {
            return _cardVO;
        }
        set
        {
            _cardVO = value;
            frontRenderer.sprite = SpriteManager.instance.GetSprite(_cardVO.id);
        }
    }
}
