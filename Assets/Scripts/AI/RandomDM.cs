using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [CreateAssetMenu(menuName = "AI/DecisionMakers/Random")]
    [System.Serializable]
    public class RandomDM : DecisionMaker
    {
        public override void MakeDecision()
        {
            throw new System.NotImplementedException();
        }
    }
}
