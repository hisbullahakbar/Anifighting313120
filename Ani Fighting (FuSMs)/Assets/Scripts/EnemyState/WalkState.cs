using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : IEnemyState
{
    private Enemy enemy;
    private float walkTimer;
    private float walkDuration = 7f;

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
        Debug.Log("Walking");
        Walk();
        enemy.Move();
        //if (Player.Instance.onGround && enemy.InNearRange)
        //{
            if (Player.Instance.attack)
            {
                enemy.ChangeState(new WalkBackwardState());
            }
            else
            {
                enemy.ChangeState(new HeavyAttackState());
            }
        //}
    }

    public void Exit()
    {
    }

    public void OnTriggerEnter(Collider2D other)
    {
        if (other.tag == "Edge")
        {
            enemy.ChangeDirection();
        }
    }

    private void Walk()
    {
        walkTimer += Time.deltaTime;
        if (walkTimer >= walkDuration)
        {
            enemy.ChangeState(new IdleState());
        }
    }
}
