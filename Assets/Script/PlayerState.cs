using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected PlayerController playerController;


    
    public PlayerState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        this.playerController = stateMachine.PlayerController;

    }

        
    public virtual void Enter() { }

    public virtual void Exit() { }

    public virtual void Update() { }

    public virtual void FixedUpdate() { }
   

    public class IdleState : PlayerState
    {
        public IdleState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        public override void Update()
        {
            CheckTransitions();
        }

    }

    public class MoveingState : PlayerState
    {
        public MoveingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        public override void Update()
        {
            CheckTransitions();
        }

        public override void FixedUpdate()
        {
            playerController.HandleMovement();
        }

    }
    public class JumpingState : PlayerState
    {
        public JumpingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        public override void Update()
        {
            CheckTransitions();
        }

        public override void FixedUpdate()
        {
            playerController.HandleMovement();
        }
    }

    public class FallingState : PlayerState
    {
        public FallingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        public override void Update()
        {
            CheckTransitions();
        }


    }
    protected void CheckTransitions()
    {
        if (playerController.isGrounded())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                stateMachine.TransitionToState(new JumpingState(stateMachine));
            }
            else if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("vertical")! = 0)
            {
                stateMachine.TrasnitionToState(new MoveingState(stateMachine));
            }
            else
            {
                stateMachine.TransitionToState(new IdleState(stateMachine));
            }
        }
        else
        {
            if (playerController.GetVerticalVelocity() > 0)
            {
                stateMachine.TransitionToState(new JumpingState(stateMachine));
            }
            else
            {
                stateMachine.TransitionToState(new FallingState(stateMachine));
            }
        }
    }


}
