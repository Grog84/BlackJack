using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class Player : MonoBehaviour {

    public PlayerVO playerVO { get; private set; }

    [HideInInspector] public int playerHandValue;

    PlayerState _state;

    Brain brain;

    delegate void StateEnter();
    StateEnter stateEnter;

    delegate void StateAction();
    StateAction stateAction;

    //delegate void StateExit();
    //StateExit stateExit;

    [HideInInspector] public bool waitingForCard;

    public SpriteRenderer stateRenderer;

    public Color oneMoreColor;
    public Color stopColor;
    public Color bustedColor;
    public Color decidingColor;
    public Color idleColor;

    public PlayerState state
    {
        get { return _state; }
        set
        {
            // if (stateExit != null) { stateExit(); }

            UpdateDelegates(value);

            _state = value;

            if (stateEnter != null) { stateEnter(); }
        }
    }


    private void Awake()
    { 
        brain = GetComponent<Brain>();
        playerHandValue = 0;
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
        stateRenderer.color = oneMoreColor;
        waitingForCard = true;
    }

    void EnterStop()
    {
        stateRenderer.color = stopColor;
    }

    void EnterBusted()
    {
        stateRenderer.color = bustedColor;
    }

    void EnterDeciding()
    {
        stateRenderer.color = decidingColor;
        brain.TickBrain();
    }

    void EnterIdle()
    {
        stateRenderer.color = idleColor;
        GetComponentInChildren<PlayerArea>().ReleaseCards();
        playerHandValue = 0;
    }

    //
    // State Actions
    //

    void OneMoreAction()
    {
        if (!waitingForCard && playerHandValue > 21)
        {
            state = PlayerState.BUSTED;
        }
        else if (!waitingForCard && playerHandValue <= 21)
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
