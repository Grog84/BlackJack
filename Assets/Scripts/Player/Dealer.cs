using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Players
{
    public class Dealer : PlayerBase
    {

        public override bool waitingForCard
        {
            get { return _waitingForCard; }
            set
            {
                _waitingForCard = value;
                if (_waitingForCard)
                {
                    stateRenderer.color = stateColor.oneMoreColor;
                }
                else
                {
                    stateRenderer.color = stateColor.idleColor;
                }
            }
        }

        void Start()
        {
            handValue = 0;
            waitingForCard = false;
        }

        public void SetBusted()
        {
            stateRenderer.color = stateColor.bustedColor;
        }

        public void SetStop()
        {
            stateRenderer.color = stateColor.stopColor;
        }

        public void ResetDealer()
        {
            GetComponentInChildren<DealerArea>().ReleaseCards();
            handValue = 0;
        }


    }
}


