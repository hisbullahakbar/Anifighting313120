using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkStateFSM : IEnemyState
{
    private Enemy enemy;
    private float walkTimer;
    private float walkDuration = 0.5f;

    public string getStateName()
    {
        return "walk";
    }

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        Walk();
        enemy.Move();
        //if (Player.Instance.onGround && enemy.InNearRange)
        //{
        /*if (Player.Instance.attack)
        {
            enemy.ChangeState(new WalkBackwardState());
        }
        else
        {
            enemy.ChangeState(new HeavyAttackState());
        }*/
        //}
    }

    public void Exit()
    {
    }

    public void OnTriggerEnter(Collider2D other) //marked
    {
        if (other.tag == "Edge")
        {
            enemy.ChangeDirection();
        }
    }

    private void Walk()
    {
        walkTimer += Time.deltaTime;
        if (enemy.InNearRange)
        {
            walkTimer = walkDuration;
        }
        if (walkTimer >= walkDuration || (Player.Instance.onGround && enemy.InNearRange))
        {
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
