using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [System.Serializable]
    public abstract class DecisionMaker: ScriptableObject
    {
        public abstract void MakeDecision();
    }
}
