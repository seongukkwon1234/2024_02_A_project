using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    public Animator animator;
    public PlayerStateMachine stateMachine;

    private const string PARAM_IS_MOVING = "IsMoving";
    private const string PARAM_IS_RUNING = "IsRuning";
    private const string PARAM_IS_JUMPING = "IsJumping";
    private const string PARAM_IS_FALLING = "Isfalling";
    private const string PARAM_IS_ATTACK_TRIGGER = "Attack";

    public void TriggerAttack()
    {
        animator.SetTrigger(PARAM_IS_ATTACK_TRIGGER);
    }

    private void ResetAllBoolParameters()
    {
        animator.SetBool(PARAM_IS_MOVING,false);
        animator.SetBool(PARAM_IS_RUNING,false);
        animator.SetBool(PARAM_IS_JUMPING,false);
        animator.SetBool(PARAM_IS_FALLING,false);
    }

    public void Update()
    {
        UpdateAnimationState();
    }
    private void UpdateAnimationState()
    {
        if(stateMachine.currentState != null)
        {
            ResetAllBoolParameters();

            switch(stateMachine.currentState)
            {
                case IdleState:
                    break;

                case MoveingState:
                    animator.SetBool(PARAM_IS_MOVING, true);

                    if(Input.GetKey(KeyCode.LeftShift))
                    {
                        animator.SetBool(PARAM_IS_RUNING, true);
                    }
                    break;
                case JumpingState:
                    animator.SetBool(PARAM_IS_JUMPING, true);
                    break;
                case FallingState:
                    animator.SetBool(PARAM_IS_FALLING, true);
                    break;
            }
        }
    }
}
