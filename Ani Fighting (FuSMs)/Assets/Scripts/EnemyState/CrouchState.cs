using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : IEnemyState
{

    private Enemy enemy;

    private float crouchTimer;
    private float crouchDuration = 1;
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
      
        FuzzyStateMachines.Instance.initiateFuSMs();
        FuzzyStateMachines.Instance.runFuSMs();

        switch (((MovementType.enemy)FuzzyStateMachines.Instance.ChoosenRuleIndex).ToString())
        {
            //masih bug sedikit disini
            case "idle":
                enemy.ChangeState(new IdleState());
                break;
            case "walk":
                enemy.ChangeState(new WalkState());
                break;
            case "walkBackward":
                enemy.ChangeState(new WalkBackwardState());
                break;
            case "lightAttack":
                enemy.ChangeState(new LightAttackState());
                break;
            case "heavyAttack":
                enemy.ChangeState(new HeavyAttackState());
                break;
            case "rangedAttack":
                enemy.ChangeState(new RangedAttackState());
                break;
            case "jump":
                enemy.ChangeState(new JumpState());
                break;
            case "crouch":
                enemy.ChangeState(new CrouchState());
                break;
        }

        if (crouchTimer >= crouchDuration)
        {
            canCrouch = true;
            crouchTimer = 0;
            enemy.CharaAnimator.SetBool("crouch", false);
            //enemy.ChangeState(new IdleState());

            FuzzyStateMachines.Instance.initiateFuSMs();
            FuzzyStateMachines.Instance.runFuSMs();

            switch (((MovementType.enemy)FuzzyStateMachines.Instance.ChoosenRuleIndex).ToString())
            {
                //masih bug sedikit disini
                case "idle":
                    enemy.ChangeState(new IdleState());
                    break;
                case "walk":
                    enemy.ChangeState(new WalkState());
                    break;
                case "walkBackward":
                    enemy.ChangeState(new WalkBackwardState());
                    break;
                case "lightAttack":
                    enemy.ChangeState(new LightAttackState());
                    break;
                case "heavyAttack":
                    enemy.ChangeState(new HeavyAttackState());
                    break;
                case "rangedAttack":
                    enemy.ChangeState(new RangedAttackState());
                    break;
                case "jump":
                    enemy.ChangeState(new JumpState());
                    break;
                case "crouch":
                    enemy.ChangeState(new CrouchState());
                    break;
            }
        }
    }
}
