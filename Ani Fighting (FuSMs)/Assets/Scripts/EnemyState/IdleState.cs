using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState
{
    private Enemy enemy;
    private float idleTimer;
    private float idleDuration = 100f; //5f

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
        Debug.Log("idle");
        Idle();
        //if (Player.Instance.onGround && Player.Instance.attack && enemy.InFarRange)
        //{
            enemy.ChangeState(new JumpState());
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
                enemy.ChangeState(new LightAttackState());
            //}
            //else
            //{
                enemy.ChangeState(new WalkState());
            //}
        }
    }
}