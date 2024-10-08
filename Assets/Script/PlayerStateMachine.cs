using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerState;

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerState currentState;

    public PlayerController playerController;


   
    private void Awawke()
    {
        playerController = Getcompoenet<PlayerController>();
    }

    void Start()
    {
        TransitionToState(new IdleState(this));
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.Update();
        }
    }

    public void FixedUpdate()
    {
        if (currnetState != null)
        {
            currentState.FixedUpdate();
        }
    }

    public void TransitionToState(PlayerState newState)
    {
        currentState?.Exit();

        currentState = newState;

        currentState.Enter();

        Debug.Log($"Transitioned to State {newState.GetType()}");
    }
}
