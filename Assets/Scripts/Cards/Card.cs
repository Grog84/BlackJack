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

    }
}