using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [System.Serializable]
    public abstract class DecisionMaker: ScriptableObject
    {
        [HideInInspector] public Player player;

        public abstract void MakeDecision();
    }
}
