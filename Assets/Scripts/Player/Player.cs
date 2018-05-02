/*
 * The player is intended as the one playing against the dealer and controlled by the AI.
 * It is implemented as a state machine. It consist in two delegates, one pointing to the actions that needs to be performed
 * when a state is changed, the other pointing to the action that needs to be performed when the state is active.
 * 
 * When the state is changed the two delegates are updated.
 * 
 * */

using Players.AI;

namespace Players
{
    public class Player : PlayerBase
    {

        public PlayerVO playerVO { get; private set; }

        Brain brain;

        delegate void StateEnter();
        StateEnter stateEnter;

        delegate void StateAction();
        StateAction stateAction;

        public PlayerState state
        {
            get { return _state; }
            set
            {

                UpdateDelegates(value);

                _state = value;

                if (stateEnter != null) { stateEnter(); }
            }
        }

        public override bool waitingForCard
        {
            get { return _waitingForCard; }
            set
            {
                _waitingForCard = value;
            }
        }

        void Awake()
        {
            brain = GetComponent<Brain>();
            handValue = 0;
            waitingForCard = false;
        }

        public void Init(PlayerVO plVO)
        {
            playerVO = plVO;
            state = PlayerState.IDLE;
            brain.decisionMaker.player = this;
        }

        //
        // State Enter Actions
        //

        void EnterOneMore()
        {
            stateRenderer.color = stateColor.oneMoreColor;
            _waitingForCard = true;
        }

        void EnterStop()
        {
            stateRenderer.color = stateColor.stopColor;
        }

        void EnterBusted()
        {
            stateRenderer.color = stateColor.bustedColor;
        }

        void EnterDeciding()
        {
            stateRenderer.color = stateColor.decidingColor;
            brain.TickBrain();
        }

        void EnterIdle()
        {
            stateRenderer.color = stateColor.idleColor;
            GetComponentInChildren<PlayerArea>().ReleaseCards();
            handValue = 0;
        }

        //
        // State Actions
        //

        void OneMoreAction()
        {
            if (!_waitingForCard && handValue > 21)
            {
                state = PlayerState.BUSTED;
            }
            else if (!_waitingForCard && handValue <= 21)
            {
                state = PlayerState.DECIDING;
            }
        }

        void UpdateDelegates(PlayerState state)
        {
            switch (state)
            {
                case PlayerState.ONEMORE:
                    stateEnter = EnterOneMore;
                    stateAction = OneMoreAction;
                    break;
                case PlayerState.STOP:
                    stateEnter = EnterStop;
                    stateAction = null;
                    break;
                case PlayerState.BUSTED:
                    stateEnter = EnterBusted;
                    stateAction = null;
                    break;
                case PlayerState.DECIDING:
                    stateEnter = EnterDeciding;
                    stateAction = null;
                    break;
                case PlayerState.IDLE:
                    stateEnter = EnterIdle;
                    stateAction = null;
                    break;
                default:
                    stateEnter = null;
                    break;
            }
        }

        private void Update()
        {
            if (stateAction != null)
            {
                stateAction();
            }
        }

        
    }

}

