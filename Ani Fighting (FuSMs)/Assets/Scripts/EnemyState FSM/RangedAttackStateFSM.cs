using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackStateFSM : IEnemyState
{
    private Enemy enemy;

    private float castMagicTimer;
    private float castMagicCoolDown = 2;
    private bool canCastMagic = true;

    public string getStateName()
    {
        return "rangedAttack";
    }

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
