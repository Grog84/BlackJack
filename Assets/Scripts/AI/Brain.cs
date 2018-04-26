using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class Brain : MonoBehaviour
    {
        public DecisionMaker decisionMaker;
        public float tickDelay;

        void Start()
        {
            decisionMaker.player = GetComponent<Player>();
        }

        public void TickBrain()
        {
            decisionMaker.MakeDecision();
        }     

    }
}
