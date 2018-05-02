/*
 * Simple assets for the definition of the colors associated to the different player states
 * 
 * */

using UnityEngine;

namespace Players
{
    [CreateAssetMenu(menuName = "Players/StatesColor")]
    public class StatesColor : ScriptableObject
    {
        public Color oneMoreColor;
        public Color stopColor;
        public Color bustedColor;
        public Color decidingColor;
        public Color idleColor;

    }
}