/*
 * The Brain is the main component in charge of taking decisions
 * It consist in a single method that can be referred in order to ask for a decision.
 * 
 * */

using UnityEngine;

namespace Players.AI
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
