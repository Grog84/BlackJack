using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class Card : MonoBehaviour
    {
        private CardVO _cardVO;
        public SpriteRenderer frontRenderer;

        private bool _grabbed;
        private Vector3 selectionOffset;

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

        public bool grabbed
        {
            get
            {
                return _grabbed;
            }
            set
            {
                _grabbed = value;
                selectionOffset = InputManager.instance.GetPosition() - transform.position;
            }
        }


    }
}