using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class Player : MonoBehaviour {

    [HideInInspector] public int playerHandValue;

    PlayerState _state;

    Brain brain;

    delegate void StateEnter();
    StateEnter stateEnter;

    delegate void StateAction();
    StateAction stateAction;

    //delegate void StateExit();
    //StateExit stateExit;

    bool waitingForCard;

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
        else
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
