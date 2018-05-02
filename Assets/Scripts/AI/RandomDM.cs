/*
 * The Random decision maker is a simple AI system that consist in rolling for the chance of asking for one more card
 * It can be personalised opting for one out of three different personalities, each characterized by a different
 * chance of asking for more cards.
 * 
 * */

using UnityEngine;

namespace Players.AI
{
    public enum PlayerAttitude { SHY, NORMAL, BOLD }

    [CreateAssetMenu(menuName = "AI/DecisionMakers/Random")]
    [System.Serializable]
    public class RandomDM : DecisionMaker
    {
        public PlayerAttitude attitude;
        float successThreshold;

        bool initialized = false;

        public override void MakeDecision()
        {
            if (!initialized)
            {
                Init();
            }

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
                    successThreshold = 0.9f;
                    break;
                default:
                    break;
            }

            initialized = true;
        }

        public bool TakeRandomDecision()
        {
            if (player.handValue <= 10)
            {
                return true;
            }
            else if (player.handValue >= 20)
            {
                return false;
            }
            else if (player.handValue >= 18)
            {
                return Random.value < (successThreshold - 0.3);
            }
            else
            {
                return Random.value < successThreshold;
            }
        }

    }
}
