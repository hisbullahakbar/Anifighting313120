using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStateFSM : IEnemyState
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