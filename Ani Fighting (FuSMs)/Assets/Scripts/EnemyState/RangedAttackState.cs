using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackState : IEnemyState
{
    private Enemy enemy;

    private float castMagicTimer;
    private float castMagicCoolDown = 4;
    private bool canCastMagic = true;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        CastingMagic();
    }

    public void Exit()
    {
    }

    public void OnTriggerEnter(Collider2D other)
    {
    }

    private void CastingMagic()
    {
        if (canCastMagic)
        {
            canCastMagic = false;
            enemy.CharaAnimator.SetTrigger("rangedAttack");
        }
        castMagicTimer += Time.deltaTime;

        if (castMagicTimer >= castMagicCoolDown)
        {
            canCastMagic = true;
            castMagicTimer = 0;
            enemy.ChangeState(new IdleState());
        }
    }
}