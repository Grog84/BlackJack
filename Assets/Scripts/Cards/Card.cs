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

        private bool _grabbed;
        public Vector3 grabbingOffset { get; private set; }

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

        public bool grabbed
        {
            get
            {
                return _grabbed;
            }
            set
            {
                _grabbed = value;
                if (_grabbed)
                {
                    grabbingOffset = InputManager.instance.GetPosition() - transform.position;
                }
            }
        }

        void VOPointsConvertion()
        {
            int normalizedValue = _cardVO.idValue % 13;
            if (normalizedValue <= 9)
            {
                pointsValue = normalizedValue;
            }
            else if (normalizedValue >= 10 || normalizedValue <= 12)
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