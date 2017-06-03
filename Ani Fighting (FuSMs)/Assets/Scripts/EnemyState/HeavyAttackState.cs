using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttackState : IEnemyState
{
    private Enemy enemy;

    private float heavyAttackTimer;
    private float heavyAttackCoolDown = 4;
    private bool canHeavyAttack = true;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        HeavyAttack();
    }

    public void Exit()
    {
    }

    public void OnTriggerEnter(Collider2D other)
    {
    }

    private void HeavyAttack()
    {
        if (canHeavyAttack)
        {
            canHeavyAttack = false;
            enemy.CharaAnimator.SetTrigger("heavyAttack");
        }
        heavyAttackTimer += Time.deltaTime;

        if (heavyAttackTimer >= heavyAttackCoolDown)
        {
            canHeavyAttack = true;
            heavyAttackTimer = 0;
            enemy.ChangeState(new IdleState());

            enemy.CharaAnimator.SetBool("crouch", false); //if before of this state chara is crouch attack (heavy)
        }
    }
}
