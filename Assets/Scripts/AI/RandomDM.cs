﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public enum PlayerAttitude { SHY, NORMAL, BOLD }

    [CreateAssetMenu(menuName = "AI/DecisionMakers/Random")]
    [System.Serializable]
    public class RandomDM : DecisionMaker
    {
        public PlayerAttitude attitude;
        float successThreshold;

        public override void MakeDecision()
        {
            if (TakeRandomDecision())
            {
                player.state = PlayerState.ONEMORE;
            }
            else
            {
                player.state = PlayerState.STOP;
            }


        }

        public void Init()
        {
            switch (attitude)
            {
                case PlayerAttitude.SHY:
                    successThreshold = 0.3f;
                    break;
                case PlayerAttitude.NORMAL:
                    successThreshold = 0.6f;
                    break;
                case PlayerAttitude.BOLD:
                    successThreshold = 0.8f;
                    break;
                default:
                    break;
            }
        }

        public bool TakeRandomDecision()
        {
            if (player.playerHandValue <= 10)
            {
                return true;
            }
            else if (player.playerHandValue == 21)
            {
                return false;
            }
            else
            {
                return Random.value < successThreshold;
            }
        }

    }
}
