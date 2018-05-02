/*
 * The component is different from its base just for the the type of the player it is referring 
 * 
 * */

namespace Players
{
    public class DealerArea : PlayerAreaBase
    {
        protected override void Awake()
        {
            base.Awake();
            player = GetComponentInParent<Dealer>();
        }

    }
}

