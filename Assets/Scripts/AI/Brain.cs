using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class Brain : MonoBehaviour
    {
        public DecisionMaker decisionMaker;
        public float tickDelay;

        public void TickBrain()
        {
            decisionMaker.MakeDecision();
        }     

    }
}
