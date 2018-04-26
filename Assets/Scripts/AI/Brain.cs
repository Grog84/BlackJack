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
            StartCoroutine(BrainCO());
        }

        public IEnumerator BrainCO()
        {
            while (true)
            {
                yield return new WaitForSeconds(tickDelay);
                
                TickBrain();
            }
        }

        void TickBrain()
        {
            decisionMaker.MakeDecision();
        }     

    }
}
