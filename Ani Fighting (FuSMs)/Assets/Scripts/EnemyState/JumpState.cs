using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : IEnemyState
{
    private Enemy enemy;

    private float jumpTimer;
    private float jumpCoolDown = 0;
    private bool canJump = true;

    public string getStateName()
    {
        return "jump";
    }

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        Jump();
    }

    public void Exit()
    {
    }

    public void OnTriggerEnter(Collider2D other)
    {
    }

    private void Jump()
    {
        if (canJump && enemy.charaRigidbody2D.velocity.y == 0)
        {
            canJump = false;
            enemy.CharaAnimator.SetTrigger("jump");
        }
        jumpTimer += Time.deltaTime;
        //enemy.ChangeState(new LightAttackState());

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

        if (jumpTimer >= jumpCoolDown)
        {
            if (enemy.onGround)
            {
                canJump = true;
                jumpTimer = 0;
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
}
