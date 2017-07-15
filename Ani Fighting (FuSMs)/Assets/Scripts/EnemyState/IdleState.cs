using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState
{
    private Enemy enemy;
    private float idleTimer;
    private float idleDuration = 0f; //5f

    public string getStateName()
    {
        return "idle";
    }

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        //Debug.Log("idle");
        Idle();
        //if (Player.Instance.onGround && Player.Instance.attack && enemy.InFarRange)
        //{
            //enemy.ChangeState(new JumpState());
        //}
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {

    }

    private void Idle()
    {
        enemy.CharaAnimator.SetFloat("speed", 0);

        idleTimer += Time.deltaTime;
        if (idleTimer >= idleDuration)
        {
            //if (Player.Instance.onGround && enemy.InNearRange)
            //{
                //enemy.ChangeState(new LightAttackState());
            //}
            //else
            //{
                //enemy.ChangeState(new WalkState());
            //}

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