using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkBackwardStateFSM : IEnemyState
{
    private Enemy enemy;
    private float walkBackwardTimer;
    private float walkBackwardDuration = 0.5f;

    public string getStateName()
    {
        return "walkBackward";
    }

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        Walk();
        enemy.MoveBackward();
        //if (Player.Instance.onGround && enemy.InFarRange)
        //{
        //enemy.ChangeState(new RangedAttackState());
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
        walkBackwardTimer += Time.deltaTime;

        if (walkBackwardTimer >= walkBackwardDuration || (Player.Instance.onGround && enemy.InFarRange))
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
