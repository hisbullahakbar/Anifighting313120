using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchStateFSM : IEnemyState
{
    private Enemy enemy;

    private float crouchTimer;
    private float crouchDuration = 0.5f;
    private bool canCrouch = true;

    public string getStateName()
    {
        return "crouch";
    }

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        Crouch();
    }

    public void Exit()
    {
    }

    public void OnTriggerEnter(Collider2D other)
    {
    }

    private void Crouch()
    {
        if (canCrouch && enemy.charaRigidbody2D.velocity.y == 0)
        {
            canCrouch = false;
            enemy.CharaAnimator.SetTrigger("crouch");
            //enemy.CharaAnimator.SetTrigger("crouchAttack"); 
            //we must searching solution for jump attack, jump range attack, and crouch attack
            //we must make a decision for Classify these all into stateclass or it's just become additional movement...
        }
        crouchTimer += Time.deltaTime;
        //enemy.ChangeState(new HeavyAttackState());

        //FuzzyStateMachines.Instance.initiateFuSMs();
        //FuzzyStateMachines.Instance.runFuSMs();

        /*
        switch (((MovementType.enemy)FuzzyStateMachines.Instance.ChoosenRuleIndex).ToString())
        {
            case "idle":
                enemy.ChangeState (new IdleState ());
                enemy.CharaAnimator.SetBool ("crouch", false);
                break;
            case "walk":
                enemy.ChangeState(new WalkState());
                enemy.CharaAnimator.SetBool ("crouch", false);
                break;
            case "walkBackward":
                enemy.ChangeState(new WalkBackwardState());
                enemy.CharaAnimator.SetBool ("crouch", false);
                break;
            case "lightAttack":
                enemy.ChangeState(new LightAttackState());
                enemy.CharaAnimator.SetBool ("crouch", false);
                break;
            case "heavyAttack":
                enemy.ChangeState(new HeavyAttackState());
                enemy.CharaAnimator.SetBool ("crouch", false);
                break;
            case "rangedAttack":
                enemy.ChangeState(new RangedAttackState());
                enemy.CharaAnimator.SetBool ("crouch", false);
                break;
            case "jump":
                enemy.ChangeState(new JumpState());
                enemy.CharaAnimator.SetBool ("crouch", false);
                break;
            case "crouch":
                enemy.ChangeState(new CrouchState());
                break;
        }*/

        if (crouchTimer >= crouchDuration)
        {
            canCrouch = true;
            crouchTimer = 0;
            //enemy.CharaAnimator.SetBool("crouch", false);
            //enemy.ChangeState(new IdleState());

            FiniteStateMachines.Instance.initiateFSM();
            FiniteStateMachines.Instance.runFSM();

            switch (((MovementType.enemy)FiniteStateMachines.Instance.ChoosenRuleIndex).ToString())
            {
                case "idle":
                    enemy.ChangeState(new IdleStateFSM());
                    enemy.CharaAnimator.SetBool("crouch", false);
                    break;
                case "walk":
                    enemy.ChangeState(new WalkStateFSM());
                    enemy.CharaAnimator.SetBool("crouch", false);
                    break;
                case "walkBackward":
                    enemy.ChangeState(new WalkBackwardStateFSM());
                    enemy.CharaAnimator.SetBool("crouch", false);
                    break;
                case "lightAttack":
                    enemy.ChangeState(new LightAttackStateFSM());
                    enemy.CharaAnimator.SetBool("crouch", false);
                    break;
                case "heavyAttack":
                    enemy.ChangeState(new HeavyAttackStateFSM());
                    break;
                case "rangedAttack":
                    enemy.ChangeState(new RangedAttackStateFSM());
                    enemy.CharaAnimator.SetBool("crouch", false);
                    break;
                case "jump":
                    enemy.ChangeState(new JumpStateFSM());
                    enemy.CharaAnimator.SetBool("crouch", false);
                    break;
                case "crouch":
                    enemy.ChangeState(new CrouchStateFSM());
                    break;
            }
        }
    }
}
