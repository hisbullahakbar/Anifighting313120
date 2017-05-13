using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : IEnemyState
{
    private Enemy enemy;
    private float walkTimer;
    private float walkDuration = 10f;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        Debug.Log("Walking");
        Walk();
        enemy.Move();
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
