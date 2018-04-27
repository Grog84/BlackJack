using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class Card : MonoBehaviour
    {
        int pointsValue;

        private CardVO _cardVO;
        public SpriteRenderer frontRenderer;

        public bool locked;
        public bool grabbed;

        public bool animating;

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
                VOPointsConvertion();
            }
        }

        private void Start()
        {
            locked = false;
        }

        void VOPointsConvertion()
        {
            int normalizedValue = _cardVO.idValue % 13;
            if (normalizedValue <= 8)
            {
                pointsValue = normalizedValue + 2;
            }
            else if (normalizedValue >= 9 && normalizedValue <= 11)
            {
                pointsValue = 10;
            }
            else
            {
                pointsValue = 11;
            }
        }

        public int GetPoints()
        {
            return pointsValue;
        }

    }
}