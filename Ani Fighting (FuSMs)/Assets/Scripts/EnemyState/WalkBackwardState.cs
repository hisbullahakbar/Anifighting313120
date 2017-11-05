using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkBackwardState : IEnemyState
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
 
        if (walkBackwardTimer >= walkBackwardDuration ||(Player.Instance.onGround &&  enemy.InFarRange))
        {
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
