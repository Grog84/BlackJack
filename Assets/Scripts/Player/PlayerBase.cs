/*
 * The class is defining the common interface between the dealer and the player controlled by the AI 
 * 
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Players
{
    public abstract class PlayerBase : MonoBehaviour
    {

        [HideInInspector] public int handValue;

        protected PlayerState _state;

        public SpriteRenderer stateRenderer;

        public StatesColor stateColor;

        protected bool _waitingForCard;

        public abstract bool waitingForCard { get; set; }


    }
}


