using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpStateFSM : IEnemyState
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

        FiniteStateMachines.Instance.initiateFSM();
        FiniteStateMachines.Instance.runFSM();

        switch (((MovementType.enemy)FiniteStateMachines.Instance.ChoosenRuleIndex).ToString())
        {
            //masih bug sedikit disini
            case "idle":
                enemy.ChangeState(new IdleStateFSM());
                break;
            case "walk":
                enemy.ChangeState(new WalkStateFSM());
                break;
            case "walkBackward":
                enemy.ChangeState(new WalkBackwardStateFSM());
                break;
            case "lightAttack":
                enemy.ChangeState(new LightAttackStateFSM());
                break;
            case "heavyAttack":
                enemy.ChangeState(new HeavyAttackStateFSM());
                break;
            case "rangedAttack":
                enemy.ChangeState(new RangedAttackStateFSM());
                break;
            case "jump":
                enemy.ChangeState(new JumpStateFSM());
                break;
            case "crouch":
                enemy.ChangeState(new CrouchStateFSM());
                break;
        }

        if (jumpTimer >= jumpCoolDown)
        {
            if (enemy.onGround)
            {
                canJump = true;
                jumpTimer = 0;
                //enemy.ChangeState(new IdleState());

                FiniteStateMachines.Instance.initiateFSM();
                FiniteStateMachines.Instance.runFSM();

                switch (((MovementType.enemy)FiniteStateMachines.Instance.ChoosenRuleIndex).ToString())
                {
                    //masih bug sedikit disini
                    case "idle":
                        enemy.ChangeState(new IdleStateFSM());
                        break;
                    case "walk":
                        enemy.ChangeState(new WalkStateFSM());
                        break;
                    case "walkBackward":
                        enemy.ChangeState(new WalkBackwardStateFSM());
                        break;
                    case "lightAttack":
                        enemy.ChangeState(new LightAttackStateFSM());
                        break;
                    case "heavyAttack":
                        enemy.ChangeState(new HeavyAttackStateFSM());
                        break;
                    case "rangedAttack":
                        enemy.ChangeState(new RangedAttackStateFSM());
                        break;
                    case "jump":
                        enemy.ChangeState(new JumpStateFSM());
                        break;
                    case "crouch":
                        enemy.ChangeState(new CrouchStateFSM());
                        break;
                }
            }
        }
    }
}
