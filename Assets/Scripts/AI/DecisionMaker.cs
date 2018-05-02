/*
 * The Decision maker is an abstract class. It defines the interface for the AI of the player.
 * The structure is intended for using a strategy pattern
 * 
 * */

using UnityEngine;

namespace Players.AI
{
    [System.Serializable]
    public abstract class DecisionMaker: ScriptableObject
    {
        [HideInInspector] public Player player;

        public abstract void MakeDecision();
    }
}
