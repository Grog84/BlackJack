using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class CardManager : MonoBehaviour {

        [Range(10, 52)]
        public int cardPoolExtension;

        public GameObject cardPrefab;

        [HideInInspector]
        public List<CardVO> cardsVOList = new List<CardVO>();

        [HideInInspector]
        public List<Card> cardPool = new List<Card>();

        public void Init () {

            CreateCardsVOList();
            CreateCardPrefabs();
        }

        private void CreateCardsVOList()
        {
            CardVO cardVO;
            List<string> dictKeys = new List<string>(SpriteManager.instance.spriteDict.Keys);

            for (int i = 0; i < SpriteManager.instance.spriteDict.Count; i++)
            {
                cardVO = new CardVO();
                cardVO.id = dictKeys[i];
                cardsVOList.Add(cardVO);
            }
        }

        private void CreateCardPrefabs()
        {
            for (int i = 0; i < cardPoolExtension; i++)
            {
                cardPool.Add(Instantiate<GameObject>(cardPrefab).GetComponent<Card>());
            }
        }
    }
}
